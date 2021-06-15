using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftViveInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        /*GameObject wheelChair = GameObject.FindWithTag("chair");
        wheelChair.GetComponent<WheelchairMovement>().SetLeftController(this.transform.gameObject, this.GetComponent<SteamVR_TrackedController>());*/

        GameObject[] sharedObjects = GameObject.FindGameObjectsWithTag("shared");
        for (int i = 0; i < sharedObjects.Length; i++)
        {
            sharedObjects[i].GetComponent<ViveGrab>().EnableController(this.transform.gameObject, this.GetComponent<SteamVR_TrackedController>(), 0);
        }
    }
}
