using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletSpawner;

    private Equipment equip;
    private PlayerController controller;

    private Gun myCurrentGun;
    private Gun myNextGun;
    private Gun[] myEquipped = new Gun[2];

    public Text ammoUI; // a ref to text onscreen that shows ammo amounts
    public Text interactUI; // a ref to popup when standing over weapon
    public Text currWeapUI;
    public ParticleSystem muzzleFlash; //a ref to the muzzleflash of the weapon

    public int myPlayernum;

    // Use this for initialization
    void Start()
    {
        equip = GetComponent<Equipment>();
        controller = GetComponent<PlayerController>();
        myPlayernum = controller.myPlayerNum;
    }

    // Update is called once per frame
    void Update()
    {
        myEquipped[0] = equip.equipped[0];
        myEquipped[1] = equip.equipped[1];

        myCurrentGun = equip.currentGun;
        myNextGun = equip.nextGun;

        if (myCurrentGun == myEquipped[0])
        {
            myNextGun = myEquipped[1];
        }
        else if (myCurrentGun == myEquipped[1])
        {
            myNextGun = myEquipped[0];
        }

        if (myNextGun != equip.empty)
        {
            ammoUI.text = " ( " + myCurrentGun.currentClip.ToString() + " | " + myCurrentGun.currentAmmo.ToString() + " )   " + myNextGun.currentClip.ToString() + " | " + myNextGun.currentAmmo.ToString();
        } else
        {
            ammoUI.text = " ( " + myCurrentGun.currentClip.ToString() + " | " + myCurrentGun.currentAmmo.ToString() + " ) ";
        }



        currWeapUI.text = "EQUIPPED: " + myCurrentGun.name + " || " + myNextGun.name;

        if (myCurrentGun == equip.empty && myNextGun != equip.empty)
        {
            ammoUI.text = " (Empty) " + " ( " + myNextGun.currentClip.ToString()  +  " | " + myNextGun.currentAmmo.ToString() + " ) ";
        }

        

        //Debug.Log("we have out " + myCurrentGun.name + " and " + myNextGun.name + " secondary");
    }

    public void Fire()
    {
        if (myCurrentGun.name == "M7 Carbine")
        {
            if (Input.GetButton(myPlayernum + "Shoot"))
            {
                Invoke("Fire", .125f);
            }
        }

        if (myCurrentGun.currentClip > 0)
        {
            
            GameObject newbullet = Instantiate(bulletPrefab);
            newbullet.transform.position = bulletSpawner.transform.position;
            newbullet.transform.up = bulletSpawner.transform.forward;
            Vector3 bulletDir = bulletSpawner.transform.forward;
            newbullet.GetComponent<Rigidbody>().AddForce(myCurrentGun.bulletVelocity * bulletDir);
            myCurrentGun.currentClip -= 1;
            muzzleFlash.Play();
        }
    }


    public void Reload()
    {
        if (myCurrentGun.currentAmmo >= (myCurrentGun.maxClip - myCurrentGun.currentClip)) //if my current ammo is bigger than the amount of ammo needed to refill my mag
        {
            myCurrentGun.currentAmmo -= (myCurrentGun.maxClip - myCurrentGun.currentClip); //subtract that amount from my pocket ammo
            myCurrentGun.currentClip += (myCurrentGun.maxClip - myCurrentGun.currentClip); //fill my clip back to max
        }
        else if (myCurrentGun.currentAmmo < (myCurrentGun.maxClip - myCurrentGun.currentClip)) //if my pocket ammo is not enough
        {
            myCurrentGun.currentClip += myCurrentGun.currentAmmo; // put whatever is left in my pcoket into my clip
            myCurrentGun.currentAmmo = 0; //make my pocket ammo = 0
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "pickup") //if you are touching a pickup item...
        {
            if (other.GetComponent<Pickup>().gunName != equip.equipped[0].name) //if the weapon you are touching is not the same type of gun you are holding...
            {
                if (other.GetComponent<Pickup>().gunName != equip.equipped[1].name) //if the weapon we are touching is not the same type of gun you have on back...
                {
                    //Debug.Log("Hold R1 to pickup " + other.GetComponent<Pickup>().gunType.name + "|" + other.GetComponent<Pickup>().clipAmmo.ToString() + "|" + other.GetComponent<Pickup>().pocketAmmo.ToString()); //show the gun name and ammo
                    interactUI.text = "Tap R1 to pickup " + other.GetComponent<Pickup>().gunType.name + " " + "(" + other.GetComponent<Pickup>().clipAmmo.ToString() + "|" + other.GetComponent<Pickup>().pocketAmmo.ToString() + ")";
                    if (Input.GetButtonDown(myPlayernum + "Pickup"))
                    {
                        Debug.Log("Picked up " + other.GetComponent<Pickup>().gunType.name);
                        equip.PickupGun(other.GetComponent<Pickup>().gunType, other.GetComponent<Pickup>().clipAmmo, other.GetComponent<Pickup>().pocketAmmo);
                        other.GetComponent<Pickup>().RemoveFromMap();
                        interactUI.text = " ";
                    }
                } 

            }
            /*if (other.GetComponent<Pickup>().gunType != equip.equipped[equip.gunIndex])
            {
                interactUI.text = "Hold R1 to pickup a " + other.GetComponent<Pickup>().gunType.name + " ( " + other.GetComponent<Pickup>().clipAmmo + " | " + other.GetComponent<Pickup>().pocketAmmo + " ) ";
                if (Input.GetButtonDown(myPlayernum + "Pickup"))
                {
                    if (other.GetComponent<Pickup>().gunType != equip.equipped[equip.gunIndex])
                    {
                        //Debug.Log("We should have taken this gun");
                        equip.PickupGun(other.GetComponent<Pickup>().gunType, other.GetComponent<Pickup>().clipAmmo, other.GetComponent<Pickup>().pocketAmmo);
                        other.GetComponent<Pickup>().RemoveFromMap();
                        interactUI.text = "";
                    }

                }
            } else if (equip.equipped[equip.gunIndex] == other.GetComponent<Pickup>().gunType)
            {
                Debug.Log("pickup ammo");
                //equip.equipped[equip.gunIndex].currentAmmo += other.GetComponent<Pickup>().pocketAmmo;
                //other.GetComponent<Pickup>().RemoveFromMap();
            }

            //Debug.Log(other.GetComponent<Pickup>().gunType.name); */



        }




    }

    private void OnTriggerEnter(Collider other) //right when u enter the sphere trigger of a pickup...
    {
        if (other.GetComponent<Collider>().tag == "pickup") //if it is a pickup...
        {
            if (other.GetComponent<Pickup>().gunType.name == equip.equipped[0].name) //if the pickup is the same as ur gun 0...
            {
                // Debug.Log("picked up " + other.GetComponent<Pickup>().pocketAmmo.ToString() + " rounds for " + other.GetComponent<Pickup>().gunType.name); //tell us we picked up ammo for that gun.
                interactUI.text = "picked up " + other.GetComponent<Pickup>().pocketAmmo.ToString() + " rounds for " + other.GetComponent<Pickup>().gunType.name;
            }

            if (other.GetComponent<Pickup>().gunType.name == equip.equipped[1].name) //if the pickup is the same as ur gun 1...
            {
                //Debug.Log("picked up " + other.GetComponent<Pickup>().pocketAmmo.ToString() + " rounds for " + other.GetComponent<Pickup>().gunType.name); //tell us we picked up ammo for that gun.
                interactUI.text = "picked up " + other.GetComponent<Pickup>().pocketAmmo.ToString() + " rounds for " + other.GetComponent<Pickup>().gunType.name;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.GetComponent<Collider>().tag == "pickup") //if it was a pickup...
        {
            if (other.GetComponent<Pickup>().gunName != equip.equipped[0].name) //if the pickup was not our gun 0
            {
                if (other.GetComponent<Pickup>().gunName != equip.equipped[1].name) //if the pickup was not our gun 1
                {
                    interactUI.text = " "; //then remove onscreen text when we un-touch this pickup
                }
            }
        } 
        
    }

}
