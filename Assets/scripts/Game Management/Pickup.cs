using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public int clipAmmo;
    public int pocketAmmo;
    public string gunName = null;
    [SerializeField]
    public Gun gunType;
    public GameObject model;
    public GameObject magnumModel;
    public GameObject smgModel;
    public GameObject brModel;
    public bool spawnedOnMap;
    public bool isPistol;
    public bool isSmg;
    public bool isBr;

    public Equipment allEquip;

    // Use this for initialization
    void Start ()
    {
        if (spawnedOnMap == true)
        {
            allEquip = FindObjectOfType<Equipment>();
            if (isPistol)
            {
                gunType = allEquip.pistol;
                clipAmmo = 8;
                pocketAmmo = 24;
            }
            if (isSmg)
            {
                gunType = allEquip.smg;
                clipAmmo = 60;
                pocketAmmo = 120;
            }
            if (isBr)
            {
                gunType = allEquip.br;
                clipAmmo = 36;
                pocketAmmo = 75;
            }

        }
	}



    public void RemoveFromMap()
    {
        Destroy(gameObject);
    }
}
