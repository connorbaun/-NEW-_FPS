using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {
    //this is the currently equipped weapons. maybe an array with 2 spaces?
    
    public List<Gun> theGuns = new List<Gun>(); //a new list of all weapons in the game. we will create instances of weapons below

    public Gun pistol = new Gun("Magnum", 8, 60, 8, 60, 20, 50, 30000); //new weapon
    public Gun smg = new Gun("M7 Carbine",  60, 300, 60, 240, 8, 8, 20000); //new weapon
    public Gun br = new Gun("BHR55 Battle Rifle",   36, 10, 36, 360, 4, 7, 30000); //new weapon
    public Gun empty = new Gun("------", 0, 0, 0, 0, 0, 0, 0);

    public Gun currentGun; //the gun that is onscreen right this instant
    public Gun nextGun; //the gun that we will cycle to if the cycle button is pressed

    public Gun[] equipped = new Gun[2]; //equipment refers to the guns we have equipped
    public int gunIndex = 0; //keeps track of which gun we are currently on; 0 or 1?
    public Pickup pickupPrefab; //ref to the primitive pickup item which will be dropped out of our face upon picking up a new weapon
    public GameObject pickupSpawner;


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

    public void PickupGun(Gun type, int clip, int ammo)
    {        
            if (currentGun == equipped[0])
            {
                if (equipped[0].currentClip > 0 || equipped[0].currentAmmo > 0)
                {
                    CloneWeapon(100);
                }

                equipped[0] = type;
                equipped[0].currentClip = clip;
                equipped[0].currentAmmo = ammo;
                
            }
            else if (currentGun == equipped[1])
            {
                if (equipped[1].currentClip > 0 && equipped[1].currentAmmo > 0)
                {
                    CloneWeapon(100);
                }


                equipped[1] = type;
                equipped[1].currentClip = type.currentClip;
                equipped[1].currentAmmo = type.currentAmmo;
            }
        

        
    }

    public void CloneWeapon(float force)
    {
        if (currentGun == equipped[0])
        {
            if (equipped[0] != empty)
            {
                Pickup oldGun = Instantiate(pickupPrefab);
                oldGun.transform.position = pickupSpawner.transform.position;
                oldGun.GetComponent<Pickup>().gunType = equipped[0];
                oldGun.GetComponent<Pickup>().clipAmmo = equipped[0].currentClip;
                oldGun.GetComponent<Pickup>().pocketAmmo = equipped[0].currentAmmo;
                oldGun.GetComponent<Pickup>().gunName = equipped[0].name;
                if (equipped[0] == smg)
                {
                    oldGun.GetComponent<Pickup>().smgModel.SetActive(true);
                } else if (equipped[0] == pistol)
                {
                    oldGun.GetComponent<Pickup>().magnumModel.SetActive(true);
                } else if (equipped[0] == br)
                {
                    oldGun.GetComponent<Pickup>().brModel.SetActive(true);
                }
                oldGun.GetComponent<Rigidbody>().AddForce(pickupSpawner.transform.forward * force);
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

        }

    }

    public void ThrowCurrentWeapon()
    {
        if (gunIndex == 0)
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
        }

    }

    public void DropBR()
    {
        Pickup oldGun = Instantiate(pickupPrefab);
        oldGun.transform.position = pickupSpawner.transform.position;
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

    private void SetStartingEquipment()
    {
        equipped[0] = br; //sets our currentGun
        equipped[1] = smg; //sets our nextGun

        currentGun = equipped[gunIndex]; //makes sure current gun starts as 0
    }
}
