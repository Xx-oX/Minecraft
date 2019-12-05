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
    public int cubeType;

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        camera = GameObject.Find("Main Camera");
    }
	
	// Update is called once per frame
	void Update () {
        mouseClick();
        Movement();
        numChangeCubeType();
        cursorLockstate();
    }

    // Use C to change cursor.lockstate
    void cursorLockstate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    // Use button to choose cube type
    public void changeCubeType(int a)
    {
        //a:1~8
        cubeType = a;
    }

    // Use numpad to choose cube type
    void numChangeCubeType()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            cubeType = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            cubeType = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            cubeType = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            cubeType = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            cubeType = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            cubeType = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            cubeType = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            cubeType = 8;
        }
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
                if(rch.transform.tag == "Cube" && Cursor.lockState == CursorLockMode.Locked)
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
                Transform c;
                switch (cubeType)
                {
                    case 0:
                        c = Instantiate(Dirt);
                        break;
                    case 1:
                        c = Instantiate(Brick);
                        break;
                    case 2:
                        c = Instantiate(Dirt);
                        break;
                    case 3:
                        c = Instantiate(Glass);
                        break;
                    case 4:
                        c = Instantiate(GoldOre);
                        break;
                    case 5:
                        c = Instantiate(Grass);
                        break;
                    case 6:
                        c = Instantiate(Lamp);
                        break;
                    case 7:
                        c = Instantiate(Log);
                        break;
                    case 8:
                        c = Instantiate(Stone);
                        break;
                    default:
                        c = Instantiate(Stone);
                        break;
                }
                
                //Debug.Log("r" + rch.point);
                if (rch.collider.gameObject.transform.tag == "Cube")
                {
                    //Debug.Log("n"+rch.normal);
                    c.transform.position = rch.transform.position + rch.normal;
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
