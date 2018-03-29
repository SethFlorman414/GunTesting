using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	
	public float range = 100f;

    public Animator reload;
    public Animator aimDownSight;
	

	public int bulletsLeft = 200; //Total bullets we have
	public float bulletSpeed = 10f;
	public int bulletsPerMag = 6; //Bullets per each magazine
	public int currentBullets; //current bullets in our magazine
	public int shootDelay = 3;

	public float fireTimer; //Time counter for the delay

    public ParticleSystem muzzleFlash;


	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("currentBullets", currentBullets);
		PlayerPrefs.SetInt ("bulletsLeft", bulletsLeft);
	}
	
	// Update is called once per frame
	void Update () {

       
        AimDownSight();
		
		currentBullets = PlayerPrefs.GetInt ("currentBullets");
		bulletsLeft = PlayerPrefs.GetInt ("bulletsLeft");

		if (Input.GetButton ("Fire1")) {
			if (currentBullets > 0)
				Fire (); //Execute the fire function if wem press/hold the left mouse button
			
		}

		if (Input.GetKey(KeyCode.R)) {
			if(currentBullets < bulletsPerMag && bulletsLeft > 0)
                reload.SetBool("Reload", true);
                Reload ();

        }
        else
        {

            reload.SetBool("Reload", false);

        }

		if (fireTimer < bulletSpeed) {
			fireTimer += Time.deltaTime; //Add into time counter
		}
	}



    //Picking up bullets for the gun
	void OnCollisionEnter (Collision collision){
		if(collision.gameObject.tag == "BulletCase"){
			PlayerPrefs.SetInt ("Bullets", PlayerPrefs.GetInt("Bullets") + 10);
			Destroy (collision.gameObject);
		}
	}

	private void Fire(){

        muzzleFlash.Play();

		if (fireTimer > shootDelay) {
			
			Vector3 shootDirection = Camera.main.transform.forward;

			shootDirection.Normalize ();
			Camera gunCam = Camera.main;//.allCameras[1];
			Debug.Log (gunCam.name);
			Vector3 offset = gunCam.transform.forward;
			offset += 0.2f * gunCam.transform.forward;
			offset += 0.2f * gunCam.transform.right;
			offset -= 0.1f * gunCam.transform.up;
			Vector3 spawnPosition = gunCam.transform.position + offset;
			//spawnPosition.x += shootDirection.x * 0.2f;
			
			


			currentBullets--;
			fireTimer = 0.0f;
			PlayerPrefs.SetInt ("currentBullets", PlayerPrefs.GetInt ("currentBullets") - 1);

		}

	}

			public void Reload(){
				
		if (bulletsLeft > 0) 
		{
			int bulletsToLoad = bulletsPerMag - currentBullets;

			if (bulletsLeft < bulletsToLoad) 
			{
				bulletsToLoad = bulletsLeft;
			}

			bulletsLeft -= bulletsToLoad;
			currentBullets += bulletsToLoad;

			PlayerPrefs.SetInt ("currentBullets", currentBullets);
			PlayerPrefs.SetInt ("bulletsLeft", bulletsLeft);

		}
	}

    public void AimDownSight()
    {

        if (Input.GetMouseButton(1))
        {

            aimDownSight.SetBool("Aiming", true);

        }
        else
        {

            aimDownSight.SetBool("Aiming", false);

        }


    }


		
		
}
