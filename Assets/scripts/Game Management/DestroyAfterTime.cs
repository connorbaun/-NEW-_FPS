using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {
    private float timer = 0;

    public float timeBeforeDestruction = 1;
    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= timeBeforeDestruction)
        {
            Destroy(gameObject);
        }
	}

    public void OnCollisionEnter(Collision collision)
    {
        Reflect(transform.forward, collision.contacts[0].normal);
    }

    public static Vector3 Reflect(Vector3 vector, Vector3 normal)
    {
        return vector - 2 * Vector3.Dot(vector, normal) * normal;
    }
}
