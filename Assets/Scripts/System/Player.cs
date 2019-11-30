using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody))]

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

    [SerializeField] bool isFlying = false;
    [SerializeField] bool isOnGround;
    [SerializeField] float movingSpeed;
    [SerializeField] float turningSpeed;
    [SerializeField] float jumpingForce;
    [SerializeField] float flyingSpeed;

    private Rigidbody rb;
    
	// Use this for initialization
	void Start () {
        // lock cursor position
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
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

            if (Physics.Raycast(ray, out rch))
            {
                Transform c = Instantiate(Dirt);
                Debug.Log("r" + rch.point);
                if (rch.collider.gameObject.transform.tag == "Cube")
                {
                    float x, y, z;
                    x = Mathf.Floor(rch.point.x) + c.transform.localScale.x / 2;

                    //Debug.Log("rx"+ camera.transform.localRotation.eulerAngles.x);
                    if (camera.transform.localRotation.eulerAngles.x > 0 && camera.transform.localRotation.eulerAngles.x < 180)
                    {
                        y = Mathf.Floor(rch.point.y) + c.transform.localScale.y / 2;
                        //Debug.Log("1"+"y:" + y);
                    }
                    else
                    {
                        y = Mathf.Floor(rch.point.y) - c.transform.localScale.y / 2;
                        //Debug.Log("2" + "y:" + y);
                    }

                    z = Mathf.Floor(rch.point.z) + c.transform.localScale.z / 2;

                    c.transform.position = new Vector3(x, y, z);
                    Debug.Log("x" + x + "y:" + y + "z" + z);
                }
            }
            
        }
    }

    // Detect collision
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Cube")
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isOnGround = false;
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

        // Use Ctrl to run
        if (Input.GetKey(KeyCode.LeftControl))
        {
            movingSpeed = 10;
        }
        else
        {
            if (!isFlying)
            {
                movingSpeed = 5;
            }
        }

        // Use space to jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isFlying)
            { 
                if (isOnGround)
                {
                    rb.AddForce(Vector3.up * jumpingForce);
                }
            }
        }

        // Use space to fly 
        if (Input.GetKey(KeyCode.Space))
        {
            if (isFlying)
            {
                transform.localPosition += Vector3.up * flyingSpeed * Time.deltaTime;
            }
        }

        // Use shift to land while flying
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isFlying)
            {
                transform.localPosition += Vector3.down * flyingSpeed * Time.deltaTime;
            }
        }

        // Use f to fly
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlying = !isFlying;
            rb.useGravity = !rb.useGravity;
        }

        // change Speed when flying
        if (isFlying)
        {
            movingSpeed = 10;
        }
    }
}
