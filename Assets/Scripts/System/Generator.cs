using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]

public class Generator : MonoBehaviour {

    public Transform Brick;
    public Transform Dirt;
    public Transform Glass;
    public Transform GoldOre;
    public Transform Grass;
    public Transform Lamp;
    public Transform Log;
    public Transform Stone;

    [SerializeField] int width;
    [SerializeField] int height;

    // Use this for initialization
    void Start () {
        makePlatform(width,height);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Generate a platform
    void makePlatform(int width,int height)
    {
        for(int i = 0; i < width; ++i)
        {
            for(int j = 0; j < width; ++j)
            {
                Transform c = Instantiate(Grass);
                c.parent = transform;
                c.localPosition = new Vector3(-width/2+i,height-c.transform.localScale.y/2,-width/2+j);
            }
        }
    }
}
