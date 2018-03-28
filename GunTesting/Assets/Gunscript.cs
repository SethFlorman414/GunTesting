
using UnityEngine;

public class Gunscript : MonoBehaviour
{

    // Use this for initialization

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    
    public int bulletsLeft;
    public int magazine;

    public Animator reload;

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;

    private float nextTimeToFire = 0f;

    void Start()
    {

        PlayerPrefs.SetInt("Mag", magazine);
        PlayerPrefs.SetInt("BulletCount", bulletsLeft);

    }

    // Update is called once per frame
    void Update()
    {

        bulletsLeft = PlayerPrefs.GetInt("BulletCount");
        magazine = PlayerPrefs.GetInt("Mag");

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && magazine >= 0)
        {
            
            nextTimeToFire = Time.time + 1f / fireRate;

            Shoot();

        }

        if (Input.GetKey(KeyCode.R))
        {

            Reload();

        }

        if (magazine <= 0)
        {

            reload.SetBool("Reload", true);
            Reload();

        }

    }


    void Shoot()
    {

        muzzleFlash.Play();


        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

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

        if (Input.GetKey(KeyCode.R))
        {

            reload.SetBool("Reload", true);

        }
        else
        {

            reload.SetBool("Reload", false);

        }

    }


    
}
