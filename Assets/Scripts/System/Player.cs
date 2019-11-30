using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]

public class Player : MonoBehaviour {

    public Transform Brick;
    public Transform Dirt;
    public Transform Glass;
    public Transform GoldOre;
    public Transform Grass;
    public Transform Lamp;
    public Transform Log;
    public Transform Stone;

    public GameObject camera;

    [SerializeField] float movingSpeed;
    [SerializeField] float turningSpeed;



	// Use this for initialization
	void Start () {
        // lock cursor position
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camera = GameObject.Find("Main Camera");
    }
	
	// Update is called once per frame
	void Update () {
        mouseClick();
        Movement();
	}

    // Left click to delete / Right click to create
    void mouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //  left click
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;

            if(Physics.Raycast(ray,out rch))
            {
                if(rch.transform.tag == "Cube")
                {
                    Destroy(rch.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            // right click
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;

            Transform c = Instantiate(Dirt);

            if (Physics.Raycast(ray, out rch))
            {
                if (rch.collider.gameObject.transform.tag == "Cube")
                {
                    float x = Mathf.Floor(rch.point.x) + c.transform.localScale.x / 2;
                    float y = Mathf.Floor(rch.point.y) + c.transform.localScale.y / 2;
                    float z = Mathf.Floor(rch.point.z) + c.transform.localScale.z / 2;
                    c.transform.position = new Vector3(x, y, z);
                }
            }
            
        }
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
            //transform.Rotate(Vector3.up, -turningSpeed * Time.deltaTime);
            //camera.transform.RotateAround(transform.position,Vector3.up, -turningSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            // moving right
            transform.localPosition += movingSpeed * Time.deltaTime * transform.right;
            //transform.Rotate(Vector3.up, turningSpeed * Time.deltaTime);
            //camera.transform.RotateAround(transform.position, Vector3.up, turningSpeed * Time.deltaTime);
        }
    }
}
