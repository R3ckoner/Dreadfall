using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15;

    public int magAmmo;
    private int ReserveAmmount = -1;
    public int totalAmmo = 50;
    public float reloadTime = 1f;
    // messed up some of these variables, will fix 

    private bool isReloading = true;
    
    public Camera fpsCam;

    public bool weapon = false;

    private float nextTimeToFire = 0f;
    
   // public Recoil recoil;

   public AudioSource gunShot;
    public AudioSource reloadNoise;


    

    
    
     void Start() {
        {
            ReserveAmmount = magAmmo;

            
        }
    }

    void OnEnable() {

        isReloading = false;

        
    }
    // sets amounts equal to the corresponding text object, sets conditions for reload
    void Update()
    {

        if (isReloading)
            return;
        if (ReserveAmmount <= 0 || (Input.GetKeyDown("r")))
        {
            StartCoroutine(Reload());
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {

            Shoot();
        }
        
    }
     
 

    

    // performs reload
    IEnumerator Reload ()
    {
        if (totalAmmo > magAmmo){
        isReloading = true;

        //reloadNoise = GetComponent<AudioSource>();
        reloadNoise.Play(0);
        

        yield return new WaitForSeconds(reloadTime);

        ReserveAmmount = magAmmo;
        isReloading = false;

        totalAmmo = totalAmmo - ReserveAmmount;

        
        }
    }

    // fires the weapon and damages enemies
    void Shoot()
    {
        
        if (ReserveAmmount > 0){

        gunShot = GetComponent<AudioSource>();
        gunShot.Play(0);

       // recoil.Fire();
        
       

        ReserveAmmount --;
        
        
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

          // EnemyHealth health = hit.transform.GetComponent<EnemyHealth>();

         //  if (health != null)

           // health.TakeDamage(damage);

        }
        

      //  Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }

        
    }
}