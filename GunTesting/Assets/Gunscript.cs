
using UnityEngine;

public class Gunscript : MonoBehaviour {

    // Use this for initialization

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;

    private float nextTimeToFire = 0f;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {

            nextTimeToFire = Time.time + 1f / fireRate;

            Shoot();

        }


        

	}


    void Shoot()
    {

       muzzleFlash.Play();


        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Debug.Log(hit.transform.name);
           Target target =  hit.transform.GetComponent<Target>();
            if(target != null)
            {

                target.TakeDamage(damage);

            }

        }


    }

}
