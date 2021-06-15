using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GoalWin : MonoBehaviour {
    CourtManager cm;

    // Use this for initialization
    void Start () {
        cm = GameObject.FindWithTag("floor").GetComponent<CourtManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision");
        if(this.tag == "net" && collision.transform.tag == "volleyball")
        {
            //cm.ClearGoal();
            //cm.points += 1;

        }
        else if(this.tag == "basket" && collision.transform.tag == "basketball")
        {
            //cm.ClearGoal();
            //cm.points += 1;
        }
        else if(this.tag == "target" && collision.transform.tag == "football")
        {
            //cm.ClearGoal();
            //cm.points += 1;
        }
    }
}
