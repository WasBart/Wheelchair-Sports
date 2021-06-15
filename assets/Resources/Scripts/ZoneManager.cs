using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "leap")
        {
            Debug.Log("Player entered");
            other.GetComponentInChildren<CreateBall>().setInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "leap")
        {
            Debug.Log("Player exit");
            other.GetComponentInChildren<CreateBall>().setInZone = false;
        }
    }
}
