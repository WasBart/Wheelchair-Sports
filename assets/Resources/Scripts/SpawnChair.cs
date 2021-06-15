using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnChair : NetworkBehaviour {
    GameObject chair;

	// Use this for initialization
	void Start () {
        chair = Resources.Load("Prefabs/wheelchair", typeof(GameObject)) as GameObject;
        CmdSpawn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [Command]
    void CmdSpawn()
    {
        var go = (GameObject)Instantiate(
           chair,
           new Vector3(0, 0, 0),
           Quaternion.identity);

        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}
