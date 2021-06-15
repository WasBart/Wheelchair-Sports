using UnityEngine;
using System.Collections;
using Leap.Unity;
// This script defines conditions that are necessary for the Leap player to grab a shared object
// TODO: values of these four boolean variables can be changed either directly here or through other components
// AuthorityManager of a shared object should be notifyed from this script

public class LeapGrab : MonoBehaviour {

    public AuthorityManager am;
    public PinchDetector pdRight;
    public PinchDetector pdLeft;

    // conditions for the object control here
    public bool leftHandTouching = false;
    public bool rightHandTouching = false;
    public bool leftPinch = false;
    public bool rightPinch = false;
    
   

    // Use this for initialization
    void Start () {
        if(GameObject.FindGameObjectWithTag("leap") == null)
        {
            GetComponent<LeapGrab>().enabled = false;
        }
        am = this.GetComponent<AuthorityManager>();
       
    }
	
	// Update is called once per frame
	void Update () {

        if(pdRight != null)
        {
            rightPinch = pdRight.IsPinching;
        }

        if(pdLeft != null)
        {
            leftPinch = pdLeft.IsPinching;
        }

        if (leftHandTouching && rightHandTouching && leftPinch && rightPinch)
        {
            // notify AuthorityManager that grab conditions are fulfilled
            am.grabbedByPlayer = true;
        }
        else
        {
            // grab conditions are not fulfilled
            am.grabbedByPlayer = false;
        }
    }
}
