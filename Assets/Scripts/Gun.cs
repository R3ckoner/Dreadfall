using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public int magAmmo;
    private int reserveAmount;
    public int totalAmmo = 50;
    public float reloadTime = 1f;

    private bool isReloading = false;
    private bool isFiring = false;

    public Camera fpsCam;
    public AudioSource gunShot;
    public AudioSource reloadNoise;

    public float recoilForce = 0.1f;
    public float recoilRecoverySpeed = 1f;

    private Vector3 initialPosition;

    private float nextTimeToFire = 0f;

    private void Start()
    {
        reserveAmount = magAmmo;
        initialPosition = transform.localPosition;
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

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            StartCoroutine(Shoot());
        }

        // Smoothly recover from recoil
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * recoilRecoverySpeed);
    }

    private IEnumerator Reload()
    {
        if (totalAmmo >= magAmmo)
        {
            isReloading = true;
            reloadNoise.Play();

            yield return new WaitForSeconds(reloadTime);

            reserveAmount = magAmmo;
            isReloading = false;

            totalAmmo -= magAmmo - reserveAmount;
        }
    }

    private IEnumerator Shoot()
    {
        if (reserveAmount > 0)
        {
            isFiring = true;
            gunShot.Play();
            reserveAmount--;
            nextTimeToFire = Time.time + 1f / fireRate;

            // Apply recoil effect
            transform.localPosition -= transform.forward * recoilForce;

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                // Apply damage or effects to the hit object here
            }

            yield return new WaitForSeconds(0.05f); // Adjust the duration of the recoil

            isFiring = false;
        }
    }
}
