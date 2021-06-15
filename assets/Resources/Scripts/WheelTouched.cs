using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTouched : MonoBehaviour {
    WheelchairMovement movementScript;

	// Use this for initialization
	void Start () {
        movementScript = this.transform.parent.GetComponent<WheelchairMovement>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "leftVive" && this.tag == "leftWheel")
        {
            movementScript.SetLeftWheelTouched(true);
        }
        else if(collision.tag == "rightVive" && this.tag == "rightWheel")
        {
            movementScript.SetRightWheelTouched(true);
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "leftVive" && this.tag == "leftWheel")
        {
            movementScript.SetLeftWheelTouched(false);
        }
        else if (collision.tag == "rightVive" && this.tag == "rightWheel")
        {
            movementScript.SetRightWheelTouched(false);
        }

    }
}
