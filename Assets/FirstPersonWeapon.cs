using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonWeapon : MonoBehaviour {
    public List<GameObject> fpWeapons = new List<GameObject>();
    public Equipment equip;
    public Gun onScreenGun;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < fpWeapons.Count - 1; i++) //right away, scan through all gameobjects in this list and...
        {
            fpWeapons[i].SetActive(false); //disable them. this way, no weapons are onscreen until the equipment script tells us which one should be there
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < fpWeapons.Count; i++)
        {
            fpWeapons[i].SetActive(false);
        }

        if (equip.currentGun == equip.empty)
        {
            fpWeapons[0].SetActive(true);
        }
        if (equip.currentGun == equip.pistol)
        {
            fpWeapons[1].SetActive(true);
        }
        if (equip.currentGun == equip.smg)
        {
            fpWeapons[2].SetActive(true);
        }
        if (equip.currentGun == equip.br)
        {
            fpWeapons[3].SetActive(true);
        }
	}
}
