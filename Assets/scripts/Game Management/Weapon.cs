using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

public class Gun
{
    public string name = "missing";

    public GameObject model;

    public int currentClip = 0;
    public int currentAmmo = 0;

    public int maxClip = 0;
    public int maxAmmo = 0;

    public int damage = 0;
    public int headshotDamage = 0;

    public float bulletVelocity = 0;



        public Gun(string nm, int currClip, int currAmm,  int clip, int ammo, int dmg, int head, float velocity)
        {
            name = nm;
            currentClip = currClip;
            currentAmmo = currAmm;
            maxClip = clip;
            maxAmmo = ammo;
            damage = dmg;
            headshotDamage = head;
            bulletVelocity = velocity;
        }
}
