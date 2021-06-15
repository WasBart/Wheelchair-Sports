using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// TODO: this script CAN be used to detect the events of a left networked hand touching a shared object
// fill in the implementation and communicate touching events to either LeapGrab and ViveGrab by setting the rightHandTouching variable
// ALTERNATIVELY, implement the verification of the grabbing conditions in a way  your prefer
// TO REMEMBER: only the localPlayer (networked hands belonging to the localPlayer) should be able to "touch" shared objects

public class TouchLeft : MonoBehaviour {

    public bool vive;
    public bool leap;

    private ViveGrab viveGrab;
    private LeapGrab leapGrab;
 

    private void Start()
    {
        if(GameObject.FindGameObjectWithTag("leap") != null)
        {
            leap = true;
            vive = false;
            leapGrab = this.GetComponent<LeapGrab>();
          
         
        }
        else if (GameObject.FindGameObjectWithTag("vive") != null)
        {
            vive = true;
            leap = false;
            viveGrab = this.GetComponent<ViveGrab>();
        }

       
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (leap && other.tag == "leftLeap" &&  other.transform.root.GetComponent<LocalPlayerController>().actor.isLocalPlayer)
        {
          
            leapGrab.leftHandTouching = true;
           
        }
        else if (vive && other.tag == "leftVive" && other.transform.root.GetComponent<LocalPlayerController>().actor.isLocalPlayer) {
            viveGrab.leftHandTouching = true;
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (leap && other.tag == "leftLeap" && other.transform.root.GetComponent<LocalPlayerController>().actor.isLocalPlayer)
        {
            leapGrab.leftHandTouching = false;
           
        }
        else if (vive && other.tag == "leftVive" && other.transform.root.GetComponent<LocalPlayerController>().actor.isLocalPlayer)
        {
            viveGrab.leftHandTouching = false;
        }

    }
}
