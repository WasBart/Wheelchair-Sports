using UnityEngine;
using UnityEngine.Networking;
using Leap.Unity;
using System.Collections;

// TODO: define the behaviour of a shared object when it is manipulated by a client

public class OnGrabbedBehaviour : MonoBehaviour {

   
    public bool grabbed;
    GameObject leftController;
    GameObject rightController;
    Rigidbody box;
    private Vector3 previousPos;
    private Vector3 currentPos;
    private  bool Leap;
    private PinchDetector pd;
    private NetworkIdentity nI;
    private AuthorityManager aM;
    bool thrown = false;
   

    // Use this for initialization
    void Start () {
        if(GameObject.FindGameObjectWithTag("leap") != null){
            Leap = true;
            pd = GameObject.FindGameObjectWithTag("right").GetComponent<PinchDetector>();
        }
     
        box = this.GetComponent<Rigidbody>();


    }
	
	// Update is called once per frame
	void Update () {

        
        if(box == null)
        {
            box = this.GetComponent<Rigidbody>();
        }
        if(nI == null)
        {
            nI = this.GetComponent<NetworkIdentity>();
          
        }
        if(aM == null)
        {
            aM = this.GetComponent<AuthorityManager>();
        }
        if (aM.GetActor() == null)
        {
            if (GameObject.FindGameObjectWithTag("vive") != null)
            {
                aM.AssignActor(GameObject.FindGameObjectWithTag("vive").GetComponentInChildren<Actor>());
            }
            else if (GameObject.FindGameObjectWithTag("leap") != null)
            {
                aM.AssignActor(GameObject.FindGameObjectWithTag("leap").GetComponentInChildren<Actor>());
            }
        }

        if (nI.hasAuthority && !grabbed)
        {
            OnGrabbed();
        }
        else if(!nI.hasAuthority && grabbed)
        {
            
            OnReleased();
        }

     


        // GO´s behaviour when it is in a grabbed state (owned by a client) should be defined here
        if (grabbed)
        {
            previousPos = currentPos;
            currentPos = this.transform.position;

            if (GameObject.FindGameObjectWithTag("leap") != null)
            {
                if (pd.IsPinching && !aM.wasGrabbed && !thrown)
                {
                    box.isKinematic = true;
                    this.transform.parent = pd.transform.GetChild(0);
                    aM.AssignActor(GameObject.FindGameObjectWithTag("leap").GetComponentInChildren<Actor>());
                    aM.grabbedByPlayer = true;
                   
                }
                else if (!pd.IsPinching && !thrown && nI.hasAuthority)
                {
                    this.transform.parent = null;
                    Debug.Log("ball is thrown");
                    Rigidbody rb = this.GetComponent<Rigidbody>();

                    Vector3 speed = (currentPos - previousPos) / Time.deltaTime;
                    speed *= 100;
                    Debug.Log(speed.x + " " + speed.y + " " + speed.z);


                    Debug.Log("release Authority");
                    thrown = true;
           
                    //<BallCollision>().CmdForce(speed);
                    rb.isKinematic = false;
                    rb.AddForce(speed);
                    grabbed = false;
                    
                }
                else if (!nI.hasAuthority)
                {
                    this.transform.parent = null;
                    grabbed = false;
                }
            }
            else
            {
                if(!thrown) {
                box.isKinematic = true;
                box.transform.parent = rightController.transform;
                    if (!this.GetComponent<ViveGrab>().rightTriggerDown)
                    {
                        Rigidbody rb = this.GetComponent<Rigidbody>();

                        Vector3 speed = (currentPos - previousPos) / Time.deltaTime;
                        speed *= 100;
                        Debug.Log(speed.x + " " + speed.y + " " + speed.z);


                        Debug.Log("throw");
                        thrown = true;

                        rb.isKinematic = false;
                        rb.AddForce(speed);
                        grabbed = false;
                        this.transform.parent = null;
                    }

                }
            }

        }
     
	}


    // called first time when the GO gets grabbed by a player
    public void OnGrabbed()
    {
        
        Debug.Log("ongrabbed called");
        grabbed = true;
        if (GameObject.FindGameObjectWithTag("leap") != null && GameObject.FindGameObjectWithTag("leap").GetComponent<LocalPlayerController>().actor.isLocalPlayer)
        {
            leftController = GameObject.FindGameObjectWithTag("leftLeap");
            rightController = GameObject.FindGameObjectWithTag("rightLeap");
        }
        else if (GameObject.FindGameObjectWithTag("vive") != null && GameObject.FindGameObjectWithTag("vive").GetComponent<LocalPlayerController>().actor.isLocalPlayer)
        {
            leftController = this.GetComponent<ViveGrab>().GetLeftVive();
            rightController = this.GetComponent<ViveGrab>().GetRightVive();
        }
        

    }

    // called when the GO gets released by a player
    public void OnReleased()
    {
        Debug.Log("onRelease");
        grabbed = false;
        //box.isKinematic = false;

    }
}
