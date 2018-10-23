using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {
    //this is the currently equipped weapons. maybe an array with 2 spaces?
    //we also "create" each version of weapon we want in this script.
    
    public List<Gun> theGuns = new List<Gun>(); //a new list of all weapons in the game. we will create instances of weapons below

    public Gun pistol = new Gun("Magnum", 12, 60, 12, 60, 20, 50, 60000); //new weapon
    public Gun smg = new Gun("M7 Carbine",  60, 300, 60, 240, 8, 8, 60000); //new weapon
    public Gun br = new Gun("BHR55 Battle Rifle",   36, 10, 36, 360, 4, 7, 100000); //new weapon
    public Gun empty = new Gun("     ", 0, 0, 0, 0, 0, 0, 0);

    public Gun currentGun; //the gun that is onscreen right this instant
    public Gun nextGun; //the gun that we will cycle to if the cycle button is pressed

    public Gun[] equipped = new Gun[2]; //equipment refers to the guns we have equipped
    public int gunIndex = 0; //keeps track of which gun we are currently on; 0 or 1?
    public Pickup pickupPrefab; //ref to the primitive pickup item which will be dropped out of our face upon picking up a new weapon
    public GameObject bulletSpawner;


    // Use this for initialization
    void Start ()
    {
        theGuns.Add(pistol); //adds the newly-created gun to the list of all guns in the game
        theGuns.Add(smg); //adds the newly-created gun to the list of all guns in the game
        theGuns.Add(br); //adds the newly-created gun to the list of all guns in the game
        theGuns.Add(empty);

        SetStartingEquipment(); //allows us to set starting equipment (which weapons will we spawn with?)
        
    }

    // Update is called once per frame
    void Update ()
    {
        ManageEquipment(); //every frame, makes sure that we are tracking which weapon is current and which is next

        //Debug.Log(currentGun.name);
        Debug.Log(currentGun.name);
        //currentGun = equipped[gunIndex];
    }

    public void CycleEquipment()
    {
            if (gunIndex == 0)
            {
                                                  
                    gunIndex = 1;                   
                  
            }
            else if (gunIndex == 1)
            {                
                    
                    gunIndex = 0;
                               
            } 
    }

    public void PickupGun(Gun type, int clip, int ammo) //collect the attributes of the gun on the floor for our new gun to equip
    {       
            if (gunIndex == 0)//(currentGun == equipped[0]) // if we are currently on gun 0...
            {
                if (equipped[gunIndex].currentClip > 0 || equipped[gunIndex].currentAmmo > 0)//currentGun.currentClip > 0 || currentGun.currentAmmo > 0) //if we have bullets in this gun
                {
                    CloneWeapon(100); //then actually drop that gun
                }
                    equipped[gunIndex] = type; //make our current gun of type thats on the floor
                    equipped[gunIndex].currentClip = clip; //make our current guns ammo to be equal to the floor gun
                    equipped[gunIndex].currentAmmo = ammo; //make our current guns pocket to be equal to the floor gun 
            }
            else if (gunIndex == 1)//currentGun == equipped[1]) //if we are currently on gun 1...
            {
                if (equipped[gunIndex].currentClip > 0 || equipped[gunIndex].currentAmmo > 0)//currentGun.currentClip > 0 && currentGun.currentAmmo > 0) //if we have bullets in this gun
                {
                    CloneWeapon(100); //then actually drop that gun
                }

                    equipped[gunIndex] = type; //make our current gun type thats on the floor                   
                    equipped[gunIndex].currentClip = type.currentClip; //make our current guns ammo to be equal to the floor gun
                    equipped[gunIndex].currentAmmo = type.currentAmmo; //make our current pocket to be equal to floor guns

            }
    }

    public void CloneWeapon(float force) //if we are throwing the gun, make sure we have that force applied. if its 0, apply no force 
    {
        /*if (currentGun == equipped[0]) //if we currently have out gun 0
        {
            if (equipped[0] != empty) //if its not an empty slot
            {
                Pickup oldGun = Instantiate(pickupPrefab); //create a new gun to drop on the floor
                oldGun.transform.position = pickupSpawner.transform.position; //spawn the gun where we are looking
                oldGun.GetComponent<Pickup>().gunType = equipped[0]; //make it the type of gun we just held
                oldGun.GetComponent<Pickup>().clipAmmo = equipped[0].currentClip; //make it have the ammo we had
                oldGun.GetComponent<Pickup>().pocketAmmo = equipped[0].currentAmmo; //make it have the same ammo we had
                oldGun.GetComponent<Pickup>().gunName = equipped[0].name; //make it have the same name of the gun we just had
                if (equipped[0] == smg) //if we had an SMG
                {
                    oldGun.GetComponent<Pickup>().smgModel.SetActive(true); //activate the smg model
                } else if (equipped[0] == pistol) //if we had a pistol
                {
                    oldGun.GetComponent<Pickup>().magnumModel.SetActive(true); //activate the pistol model
                } else if (equipped[0] == br) //if we had a br
                {
                    oldGun.GetComponent<Pickup>().brModel.SetActive(true); //activate the br model
                }
                oldGun.GetComponent<Rigidbody>().AddForce(pickupSpawner.transform.forward * force); //toss the weapon with the force i collected (if it was thrown)
            }

        }
        else if (currentGun == equipped[1])
        {
            if (equipped[1] != empty)
            {
                Pickup oldGun = Instantiate(pickupPrefab);
                oldGun.transform.position = pickupSpawner.transform.position;
                oldGun.GetComponent<Pickup>().gunType = equipped[1];
                oldGun.GetComponent<Pickup>().clipAmmo = equipped[1].currentClip;
                oldGun.GetComponent<Pickup>().pocketAmmo = equipped[1].currentAmmo;
                oldGun.GetComponent<Pickup>().gunName = equipped[1].name;
                if (equipped[1] == smg)
                {
                    oldGun.GetComponent<Pickup>().smgModel.SetActive(true);
                }
                else if (equipped[1] == pistol)
                {
                    oldGun.GetComponent<Pickup>().magnumModel.SetActive(true);
                }
                else if (equipped[1] == br)
                {
                    oldGun.GetComponent<Pickup>().brModel.SetActive(true);
                }
                oldGun.GetComponent<Rigidbody>().AddForce(pickupSpawner.transform.forward * force);

            }

        } */

        if (equipped[gunIndex] != empty) //if we are currently holding a gun in current hand
        {
            Pickup oldGun = Instantiate(pickupPrefab); //instantiate a generic cloned weapon prefab object
            oldGun.transform.position = bulletSpawner.transform.position ; //at the place we looking at (where bullets spawn out of)            
            oldGun.GetComponent<Pickup>().gunType = equipped[gunIndex]; //transfer the gun type to the clone
            oldGun.GetComponent<Pickup>().clipAmmo = equipped[gunIndex].currentClip; //transfer ammo amt to clone
            oldGun.GetComponent<Pickup>().pocketAmmo = equipped[gunIndex].currentAmmo; //transfer ammo amt to clone
            oldGun.GetComponent<Pickup>().gunName = equipped[gunIndex].name; //transfer name to clone

            //make sure the model of the gun is correct when it spawns(all models are deactivated at spawn)

            if (equipped[gunIndex] == smg) //if it was an smg
            {
                oldGun.GetComponent<Pickup>().smgModel.SetActive(true); //activate the smg model on clone
            }
            else if (equipped[gunIndex] == pistol) //if it was a pistol
            {
                oldGun.GetComponent<Pickup>().magnumModel.SetActive(true); //activate the pistol model on clone
            }
            else if (equipped[gunIndex] == br) //if it was a br
            {
                oldGun.GetComponent<Pickup>().brModel.SetActive(true); //activate the br model on clone
            }
            oldGun.GetComponent<Rigidbody>().AddForce(bulletSpawner.transform.forward * force); //whatever force we sent over, apply it (if it was tossed or not)
        }

    }

    public void ThrowCurrentWeapon()
    {
        /*if (gunIndex == 0)
        {
            if (equipped[0] != empty)
            {
                CloneWeapon(700);
                equipped[0] = empty;
            }
            
        } else if (gunIndex == 1)
        {
            if (equipped[1] != empty)
            {
                CloneWeapon(700);
                equipped[1] = empty;
            }
        } */

        if (equipped[gunIndex] != empty) //if we actually have a weapon in our current hand 
        {
            CloneWeapon(700); //spawn a clone of weapon with some amount of force (in this case we use 700)
            equipped[gunIndex] = empty; //whatever hand we had the gun in, make it so it is now empty since
        }
    }

    public void DropBR()
    {
        Pickup oldGun = Instantiate(pickupPrefab);
        oldGun.transform.position = bulletSpawner.transform.position;
        oldGun.GetComponent<Pickup>().gunType = br;
        oldGun.GetComponent<Pickup>().clipAmmo = br.maxClip;
        oldGun.GetComponent<Pickup>().pocketAmmo = br.maxAmmo;

    }

    private void ManageEquipment()
    {
        if (gunIndex == 0)
        {
            currentGun = equipped[0];
            nextGun = equipped[1];
        }
        else if (gunIndex == 1)
        {
            currentGun = equipped[1];
            nextGun = equipped[0];
        }

    }

    public void SetStartingEquipment()
    {
        equipped[0] = empty; //sets our currentGun
        equipped[1] = empty; //sets our nextGun

        currentGun = equipped[gunIndex]; //makes sure current gun starts as 0
    }
}
