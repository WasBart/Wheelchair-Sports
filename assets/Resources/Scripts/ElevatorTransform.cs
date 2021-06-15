using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ElevatorTransform : MonoBehaviour {
    bool moving = false;
    ElevatorButton active;
    NetworkIdentity nI;
    static float distance = 7.0f;
    private Vector3 start;
    private Vector3 dest;
    private GameObject wheelChair;
    private GameObject leap;
    Vector3 prev;
    Vector3 cur;
    
	// Use this for initialization
	void Start () {
        nI = transform.root.GetComponent<NetworkIdentity>();
	}
	
	// Update is called once per frame
	void Update () {
        if (nI.isServer)
        {
            if (moving && Mathf.Abs(this.transform.position.y - dest.y) > 0.1f) 
            {
                this.transform.root.transform.Translate(transform.up * active.mode * Time.deltaTime);
            }
            else
            {
                moving = false;
            }

        }
        else
        {
            cur = this.transform.position;
            if (this.transform.position.y - prev.y < 0.1)
            {
                moving = false;
            }
            prev = cur;
        }
       
	}

    
    
    public void StartMovement(ElevatorButton active)
    {
        Debug.Log("in start mom");
        moving = true;
        if (nI.isServer)
        {
            Debug.Log("Starting Movement on eletrans");
           
            this.active = active;
            start = this.transform.position;
            dest = this.transform.position + new Vector3(0, distance *active.mode, 0);
           
        }
    }

    

    public void endMovement()
    {
        moving = false;
        if (nI.isServer)
        {
            Debug.Log("ending Movement on eletrans");
           
            this.transform.root.transform.Translate(Vector3.zero);
            active.EndMovement();
            if(leap != null)
            {
                leap.transform.parent = null;
            }
            if(wheelChair != null)
            {
                wheelChair.transform.parent = null;
            }
        }
    }

 
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision on elevator");
        if (nI.isServer)
        {
            if (collision.transform.tag == "stage")
            {
                //CmdEndMovement();
                endMovement();
            }
        }
        Debug.Log(moving);
        Debug.Log(collision.transform.tag);
        if((collision.transform.tag == "leftWheel" && moving && wheelChair == null))
        {
            collision.transform.root.parent = this.transform;
            wheelChair = collision.gameObject;
        }
        if ((collision.transform.tag == "leap" && moving && leap == null))
        {
            collision.transform.parent = this.transform;
            leap = collision.gameObject;
        }
    }

}
