using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float movingSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Movement();
	}

    // Use w/a/s/d to control 
    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // moving forward
            transform.localPosition += movingSpeed * Time.deltaTime * transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            // moving back
            transform.localPosition += movingSpeed * Time.deltaTime * -transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // moving left
            transform.localPosition += movingSpeed * Time.deltaTime * -transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            // moving right
            transform.localPosition += movingSpeed * Time.deltaTime * transform.right;
        }
    }
}
