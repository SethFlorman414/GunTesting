using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {

    // Use this for initialization\

    public Animator aim;


	void Start () {

        aim.GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {



        if (Input.GetButton("Fire2"))
        {

            aim.SetBool("Aiming", true);

        }
        else
        {

            aim.SetBool("Aiming", false);

        }


    }
}
