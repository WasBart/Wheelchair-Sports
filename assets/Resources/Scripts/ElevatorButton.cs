using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ElevatorButton : MonoBehaviour{

    private bool active = false;
    private ButtonManager buttonManager;
    private Vector3 startingPos;
    public int mode = -1;
    public ElevatorTransform eleTrans;
    private NetworkIdentity nI;

    public bool isActive
    {
        set { active = value; }
        get { return active; }
    }

    void Start()
    {
        startingPos = this.gameObject.transform.position;
        nI = transform.root.GetComponent<NetworkIdentity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (nI.isServer)
        {
            if (other.tag == "leftLeap" || other.tag == "rightLeap" || other.tag == "leftVive" || other.tag == "rightVive")
            {
                Debug.Log("trigger is entering");

                StartMovement();
                //StartMovement();

            }
        }
        else
        {
            Debug.Log("startMom called");
            eleTrans.StartMovement(this);
        }

      

    }

    

    
     public void StartMovement()
     {
        if (!active)
        {
            Debug.Log("Starting Movement in button");
            active = true;
            this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.01f);
            eleTrans.StartMovement(this);
        }
    }

     
     public void EndMovement()
     {
        if (active)
        {
            Debug.Log("Ending Movement in button");
            active = false;
            this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.01f);
        }
     }

   

}
