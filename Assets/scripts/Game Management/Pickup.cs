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
                clipAmmo = allEquip.pistol.maxClip;
                pocketAmmo = allEquip.pistol.maxAmmo;
            }
            if (isSmg)
            {
                gunType = allEquip.smg;
                clipAmmo = allEquip.smg.maxClip;
                pocketAmmo = allEquip.smg.maxAmmo;
            }
            if (isBr)
            {
                gunType = allEquip.br;
                clipAmmo = allEquip.br.maxClip;
                pocketAmmo = allEquip.br.maxAmmo;
            }

        }
	}

    public void Update()
    {

    }



    public void RemoveFromMap()
    {
        Destroy(gameObject);
    }
}
