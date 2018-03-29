
using UnityEngine;

public class Gunscript : MonoBehaviour
{

    // Use this for initialization

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public int bulletsPerMag;
    public int bulletsLeft;
    public int magazine;

    public Animator reload;
    public Animator aimDownSight;

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;

    private float nextTimeToFire = 0f;

    void Start()
    {

        PlayerPrefs.SetInt("Magazine", magazine);

        PlayerPrefs.SetInt("BulletCount", bulletsLeft);

    }

    // Update is called once per frame
    void Update()
    {
        AimDownSight();
        Reload();

        bulletsLeft = PlayerPrefs.GetInt("BulletCount");
        magazine = PlayerPrefs.GetInt("Magazine");

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && magazine >= 0)
        {
            
            nextTimeToFire = Time.time + 1f / fireRate;
           

            Shoot();

        }

        

        if (magazine <= 0)
        {

            reload.SetBool("Reload", true);
            

        }

    }


    void Shoot()
    {

        muzzleFlash.Play();


        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            magazine--;
            PlayerPrefs.SetInt("Magazine", PlayerPrefs.GetInt("Magazine") - 1);

            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {

                target.TakeDamage(damage);

            }

             

        }
        


    }


    void Reload()
    {


        if (bulletsLeft > 0)
        {
            int bulletsToLoad = bulletsPerMag - magazine;

            if (bulletsLeft < bulletsToLoad)
            {
                bulletsToLoad = bulletsLeft;
            }

            bulletsLeft -= bulletsToLoad;
            magazine += bulletsToLoad;

            PlayerPrefs.SetInt("Magazine", magazine);
            PlayerPrefs.SetInt("bulletsLeft", bulletsLeft);

        }

        if (Input.GetKey(KeyCode.R))
        {

            reload.SetBool("Reload", true);

        }
        else
        {

            reload.SetBool("Reload", false);

        }




        

    }



    public void AimDownSight()
    {

        if(Input.GetMouseButton(1))
        {
            

            aimDownSight.SetBool("Aiming", true);

        }
        else
        {

            aimDownSight.SetBool("Aiming", false);

        }
    }

    
}
