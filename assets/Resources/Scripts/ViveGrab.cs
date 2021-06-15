using UnityEngine;
using System.Collections;

// This script defines conditions that are necessary for the Vive player to grab a shared object
// TODO: values of these four boolean variables can be changed either directly here or through other components
// AuthorityManager of a shared object should be notifyed from this script

public class ViveGrab : MonoBehaviour {

    AuthorityManager am; // to communicate the fulfillment of grabbing conditions
    GameObject leftVive;
    SteamVR_TrackedController leftTracker;
    GameObject rightVive;
    SteamVR_TrackedController rightTracker;

    // conditions for the object control here
    public bool leftHandTouching = false;
    public bool rightHandTouching = false;
    public bool leftTriggerDown = false;
    public bool rightTriggerDown = false;
    

    // Use this for initialization
    void Start () {
        if (GameObject.FindGameObjectWithTag("vive") == null)
        {
            GetComponent<ViveGrab>().enabled = false;
        }
        Debug.Log("start");
        am = this.GetComponent<AuthorityManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if(leftVive == null)
        {
            leftVive = GameObject.FindGameObjectWithTag("left");
        }
        if (rightVive == null)
        {
            rightVive = GameObject.FindGameObjectWithTag("right");
        }

        if (leftVive != null && leftTracker == null)
        {
            leftTracker = leftVive.GetComponent<SteamVR_TrackedController>();
        }
        if(rightVive != null && rightTracker == null)
        {
            rightTracker = rightVive.GetComponent<SteamVR_TrackedController>();
        }

        if (leftTracker != null) {
            leftTriggerDown = leftTracker.triggerPressed;
        }
        if (rightTracker != null) {
            rightTriggerDown = rightTracker.triggerPressed;
        }

        /*Debug.Log("leftTrigger = " + leftTriggerDown);
        Debug.Log("rightTrigger = " + rightTriggerDown); 
        Debug.Log("leftHandTouching = " + leftHandTouching);
        Debug.Log("rightHandTouching = " + rightHandTouching);*/

        if ((leftHandTouching && leftTriggerDown) || (rightHandTouching && rightTriggerDown))
        {
            // notify AuthorityManager that grab conditions are fulfilled
            am.grabbedByPlayer = true;
        }
        else
        {
            // grab conditions are not fulfilled
            //am.grabbedByPlayer = false;
        }

    }

    public void EnableController(GameObject controller, SteamVR_TrackedController tracker, int mode)
    {
        if (mode == 0)
        {
            this.leftVive = controller;
            this.leftTracker = tracker;
        }
        else if (mode == 1)
        {
            this.rightVive = controller;
            this.rightTracker = tracker;
        }
    }

    public GameObject GetLeftVive()
    {
        return this.leftVive;
    }

    public GameObject GetRightVive()
    {
        return this.rightVive;
    }
}
