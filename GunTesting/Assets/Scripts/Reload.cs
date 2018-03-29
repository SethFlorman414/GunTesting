using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour {

    // Use this for initialization

    public Animator anim;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.R))
        {

            anim.SetBool("Reload", true);

        }
        else
        {

            anim.SetBool("Reload", false);

        }
		
	}
}
