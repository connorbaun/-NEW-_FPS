using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSignIn : MonoBehaviour {
    public int playerNumber = 0;
    public int listPos = 0;
    public int maxPlayers = 4;
    public GameObject playerObj;
    public List<GameObject> playerList = new List<GameObject>();

    public GameObject init;


	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("Fire1") && playerList.Count < maxPlayers) 
        {
            playerList.Add(playerObj);
        } else if (Input.GetButtonDown("Fire1") && playerList.Count >= maxPlayers)
        {
            playerList.Clear();
            playerList.Add(playerObj);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                playerNumber++;
                GameObject newPlayer = playerObj;
                playerObj.GetComponent<PlayerController>().myPlayerNum = playerNumber;
            }
            init.SetActive(true);
        }

        
        
	}
}
