using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]

public class CameraController : MonoBehaviour {

    public Transform target;

    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float xSensitive = 50;
    [SerializeField] float ySensitive = 40;
    [SerializeField] float mSensitive = 10;
    [SerializeField] float miny = -20;
    [SerializeField] float maxy = 80;
    [SerializeField] float distance;
    [SerializeField] float rollSensitive = 75;
    [SerializeField] float minDistance = 5;
    [SerializeField] float maxDistance = 15;
    [SerializeField] bool viewMode;//false for first person, true for third person
    [SerializeField] bool canMove;// false when cursor unlocked (code in Player.cs)

    private Quaternion rotationEuler;
    private Quaternion playerRotation;
    private Vector3 cameraPosition;

	// Use this for initialization
	void Start () {
        viewMode = false;
        Rigidbody rigidbody;
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody)
            rigidbody.freezeRotation = true;
        canMove = true;
    }
	
	// Update is called once per frame
	void Update () {

        mouseControl();
        if (Input.GetKeyDown(KeyCode.V))
        {
            // switch first/third person view
            viewMode = !viewMode;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            // cursor state change
            canMove = !canMove;
        }
	}

    // Use mouse to control camera
    void mouseControl()
    {
        if (canMove)
        {
            if (viewMode)
            {
                //third person
                x += Input.GetAxis("Mouse X") * xSensitive * Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySensitive * Time.deltaTime;

                // Keep x in 360 degree
                if (x > 360)
                {
                    x -= 360;
                }
                else if (x < 0)
                {
                    x += 360;
                }

                // Read mouse roll distance
                distance -= Input.GetAxis("Mouse ScrollWheel") * rollSensitive * Time.deltaTime;
                // Limit distance
                distance = Mathf.Clamp(distance, minDistance, maxDistance);

                // Compute camera position/rotation
                rotationEuler = Quaternion.Euler(y, x, 0);
                playerRotation = Quaternion.Euler(0, x, 0);
                cameraPosition = rotationEuler * new Vector3(0, 0, -distance) + target.position + new Vector3(0, 2, 0);

                // Apply
                transform.rotation = rotationEuler;
                target.transform.rotation = playerRotation;
                transform.position = cameraPosition;
            }
            else
            {
                //first person
                x = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mSensitive;
                y += Input.GetAxis("Mouse Y") * mSensitive;
                //limit angle
                y = Mathf.Clamp(y, miny, maxy);

                transform.localEulerAngles = new Vector3(-y, x, 0);
                transform.position = target.position + new Vector3(0,0.5f,0);
                target.transform.localEulerAngles = new Vector3(0, x, 0);
            }

        }
    }        
}
