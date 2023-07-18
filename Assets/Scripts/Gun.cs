using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public TextMeshProUGUI magText;
    public TextMeshProUGUI reserveText;
    public int magAmmo;
    private int reserveAmount;
    public int totalAmmo = 50;
    public float reloadTime = 1f;

    private bool isReloading = false;
    private bool isFiring = false;
    public bool isFullAuto = false; // Toggle for full-auto mode
    public bool showReserveText = true; // Toggle for reserve text visibility

    public Camera fpsCam;
    public AudioSource gunShot;
    public AudioSource reloadNoise;
    public GameObject muzzleFlash;
    public Transform barrelEnd;
    public float flashDuration = 0.1f;

    public float recoilForce = 0.1f;
    public float recoilRecoverySpeed = 10f; // Adjust this value for smoother recovery

    private Vector3 initialPosition;
    private Vector3 currentPosition; // Track the current position during recoil recovery

    private float nextTimeToFire = 0f;

    private void Start()
    {
        reserveAmount = magAmmo;
        initialPosition = transform.localPosition;
        currentPosition = initialPosition;
        UpdateAmmoText();
    }

    private void OnEnable()
    {
        isReloading = false;
    }

    private void Update()
    {
        if (isReloading)
            return;

        if (reserveAmount <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (isFullAuto)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                StartCoroutine(Shoot());
                nextTimeToFire = Time.time + 1f / fireRate;
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                StartCoroutine(Shoot());
            }
        }

        // Smoothly recover from recoil
        currentPosition = Vector3.Lerp(currentPosition, initialPosition, Time.deltaTime * recoilRecoverySpeed);
        transform.localPosition = currentPosition;
    }

    private IEnumerator Reload()
    {
        if (totalAmmo >= magAmmo)
        {
            isReloading = true;
            reloadNoise.Play();

            yield return new WaitForSeconds(reloadTime);

            int bulletsToReload = magAmmo - reserveAmount;
            reserveAmount += bulletsToReload;
            totalAmmo -= bulletsToReload;

            isReloading = false;

            UpdateAmmoText();
        }
        else if (totalAmmo > 0)
        {
            isReloading = true;
            reloadNoise.Play();

            yield return new WaitForSeconds(reloadTime);

            int bulletsToReload = totalAmmo;
            reserveAmount += bulletsToReload;
            totalAmmo -= bulletsToReload;

            isReloading = false;

            UpdateAmmoText();
        }
    }

    private IEnumerator Shoot()
    {
        if (reserveAmount > 0)
        {
            isFiring = true;

            if (!isFullAuto)
            {
                // Single shot in semi-auto mode
                // Instantiate muzzle flash
                muzzleFlash.SetActive(true);
                yield return new WaitForSeconds(flashDuration);
                muzzleFlash.SetActive(false);

                // Play gun shot audio
                gunShot.Play();

                reserveAmount--;
                nextTimeToFire = Time.time + 1f / fireRate;

                // Update ammo UI text components
                magText.text = reserveAmount.ToString();
                UpdateReserveText();

                // Apply recoil effect
                currentPosition -= transform.forward * recoilForce;

                RaycastHit hit;
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                {
                    Debug.Log(hit.transform.name);
                    // Apply damage or effects to the hit object here
                }

                yield return new WaitForSeconds(1f / fireRate);
            }
            else
            {
                int shotsFired = 0;

                while (Input.GetButton("Fire1") && reserveAmount > 0 && shotsFired < 1)
                {
                    // Instantiate muzzle flash
                    muzzleFlash.SetActive(true);
                    yield return new WaitForSeconds(flashDuration);
                    muzzleFlash.SetActive(false);

                    // Play gun shot audio
                    gunShot.Play();

                    reserveAmount--;
                    nextTimeToFire = Time.time + 1f / fireRate;
                    shotsFired++;

                    // Update ammo UI text components
                    magText.text = reserveAmount.ToString();
                    UpdateReserveText();

                    // Apply recoil effect
                    currentPosition -= transform.forward * recoilForce;

                    RaycastHit hit;
                    if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                    {
                        Debug.Log(hit.transform.name);
                        // Apply damage or effects to the hit object here
                    }

                    yield return new WaitForSeconds(1f / fireRate);
                }
            }

            isFiring = false;
        }

        // Check if reserve ammo reached 0 and trigger reload
        if (reserveAmount == 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void UpdateAmmoText()
    {
        magText.text = reserveAmount.ToString();
        UpdateReserveText();
    }

    private void UpdateReserveText()
    {
        reserveText.gameObject.SetActive(showReserveText);
        reserveText.text = totalAmmo.ToString();
    }
}
