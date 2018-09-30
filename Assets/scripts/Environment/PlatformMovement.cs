using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

    private Vector3 targetPos; //a ref to the location this object starts out (world space)

    public Transform  posA; //a ref to the first location the platform will move towards
    public Transform posB; //a ref to the second location the platform will move towards

    public float speed = 5.0f;

    private float startTime;
    private float journeyLength;

	// Use this for initialization
	void Start ()
    {
        transform.position = posA.position;
        targetPos = posB.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
               
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (transform.position == posB.position)
        {
            targetPos = posA.position;
        }

        if (transform.position == posA.position)
        {
            targetPos = posB.position;
        }

    }
}
