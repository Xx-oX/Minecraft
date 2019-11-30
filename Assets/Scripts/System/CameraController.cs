using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]

public class CameraController : MonoBehaviour {

    public Transform target;

    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float xSensitive = 10;
    [SerializeField] float ySensitive = 10;
    [SerializeField] float distance;
    [SerializeField] float rollSensitive = 10;
    [SerializeField] float minDistance = 1;
    [SerializeField] float maxDistance = 5;

    private Quaternion rotationEuler;
    private Vector3 cameraPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        mouseControl();
	}

    // Use mouse to control camera
    void mouseControl()
    {
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
        cameraPosition = rotationEuler * new Vector3(0, 0, -distance) + target.position;

        // Apply
        transform.rotation = rotationEuler;
        transform.position = cameraPosition;

    }
}
