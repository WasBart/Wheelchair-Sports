using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnableSteamVR : MonoBehaviour {

	// Use this for initialization
	void Start () {
        XRSettings.LoadDeviceByName("OpenVR");
        XRSettings.enabled = true;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
