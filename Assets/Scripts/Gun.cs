using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("Gun Properties")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float recoilForce = 0.1f;
    public float recoilRecoverySpeed = 10f;
    public float timeToFireDelay = 0.2f;

    [Header("Ammo UI")]
    public TextMeshProUGUI magText;
    public TextMeshProUGUI reserveText;

    [Header("Ammo Configuration")]
    public int magAmmo;
    private int reserveAmount;
    public int totalAmmo = 50;

    [Header("Reload Configuration")]
    public float reloadTime = 1f;
    public float reloadDelay = 0.5f;
    public float reloadTiltAmount = -10f;
    public float reloadTiltSpeed = 30f;

    [Header("Fire Configuration")]
    public bool isFullAuto = false;
    public bool showReserveText = true;
    public bool isShotgun = false;
    private int shotsFired = 0;

    [Header("Camera and Sound")]
    public Camera fpsCam;
    public AudioSource gunShot;
    public AudioSource reloadNoise;

    [Header("Visual Effects")]
    public GameObject muzzleFlashPrefab;
    public Transform barrelEnd;
    public float flashDuration = 0.1f;

    private Vector3 initialPosition;
    private Vector3 currentPosition;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;
    private bool isFiring = false;

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

        if ((isFullAuto && Input.GetButton("Fire1") && Time.time >= nextTimeToFire) ||
            (!isFullAuto && Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire))
        {
            StartCoroutine(Shoot());
            nextTimeToFire = Time.time + 1f / fireRate;
        }

        // Smoothly recover from recoil
        currentPosition = Vector3.Lerp(currentPosition, initialPosition, Time.deltaTime * recoilRecoverySpeed);
        transform.localPosition = currentPosition;
    }

    private IEnumerator Reload()
    {
        if (totalAmmo >= magAmmo)
        {
            yield return PerformReload(magAmmo - reserveAmount);
        }
        else if (totalAmmo > 0)
        {
            yield return PerformReload(totalAmmo);
        }
    }

    private IEnumerator PerformReload(int bulletsToReload)
    {
        isReloading = true;
        PlayReloadAnimation();

        yield return new WaitForSeconds(reloadTime);

        reserveAmount += bulletsToReload;
        totalAmmo -= bulletsToReload;

        yield return new WaitForSeconds(reloadDelay);

        isReloading = false;
        UpdateAmmoText();
    }

    private IEnumerator PlayReloadAnimation()
    {
        float tiltDuration = 0.5f;
        float tiltSpeed = reloadTiltSpeed;

        Vector3 initialRotation = transform.localEulerAngles;
        Vector3 targetRotation = initialRotation + new Vector3(reloadTiltAmount, 0f, 0f);

        float elapsed = 0f;
        while (elapsed < tiltDuration)
        {
            transform.localEulerAngles = Vector3.Lerp(initialRotation, targetRotation, elapsed / tiltDuration);
            elapsed += Time.deltaTime * tiltSpeed;
            yield return null;
        }

        transform.localEulerAngles = targetRotation;

        // Gradually tilt back up during the reload time
        float tiltBackDuration = reloadTime - tiltDuration;
        float tiltBackSpeed = reloadTiltSpeed;

        elapsed = 0f;
        while (elapsed < tiltBackDuration)
        {
            transform.localEulerAngles = Vector3.Lerp(targetRotation, initialRotation, elapsed / tiltBackDuration);
            elapsed += Time.deltaTime * tiltBackSpeed;
            yield return null;
        }

        transform.localEulerAngles = initialRotation;
    }

    private IEnumerator Shoot()
    {
        if (reserveAmount > 0)
        {
            isFiring = true;

            PlayMuzzleFlash();

            gunShot.Play();
            reserveAmount--;
            UpdateAmmoText();
            currentPosition -= transform.forward * recoilForce;

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                // Handle hit
                EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }

            yield return new WaitForSeconds(1f / fireRate);

            currentPosition = Vector3.Lerp(currentPosition, initialPosition, Time.deltaTime * recoilRecoverySpeed);
            transform.localPosition = currentPosition;

            yield return new WaitForSeconds(timeToFireDelay);
            isFiring = false;
        }
    }

    private void PlayMuzzleFlash()
    {
        // Activate the muzzle flash GameObject
        muzzleFlashPrefab.SetActive(true);

        // Deactivate the muzzle flash after the specified duration
        StartCoroutine(DeactivateMuzzleFlash());
    }

    private IEnumerator DeactivateMuzzleFlash()
    {
        yield return new WaitForSeconds(flashDuration);

        // Deactivate the muzzle flash GameObject
        muzzleFlashPrefab.SetActive(false);
    }

    private void UpdateAmmoText()
    {
        magText.text = reserveAmount.ToString();
        reserveText.text = totalAmmo.ToString();
    }
}
