using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallCollision : NetworkBehaviour{
    private CreateBall cb;
    private bool setCb;
    private bool ballHold;
    private GameObject leap;
    private Vector3 speed = Vector3.zero;
    private bool speedSet = false;

	// Use this for initialization
	void Start () {
        leap = GameObject.FindGameObjectWithTag("leap");
        if (leap != null)
        {
            cb = leap.GetComponentInChildren<CreateBall>();
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!setCb && cb != null)
        {
            cb.ball = this.gameObject;
        }
        if(speedSet && GetComponent<NetworkIdentity>().clientAuthorityOwner == null)
        {
            GetComponent<Rigidbody>().AddForce(speed);
            speed = Vector3.zero;
        }
        


    }

    void setParent(Transform transform)
    {
        this.transform.root.parent = transform;
    }
    
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("collision with" + collision.gameObject.tag);
       
        if (isServer)
        {
            if (collision.transform.tag == "floor")
            {
                Destroy(this.gameObject);
            }
        }
        else {
            if (collision.transform.tag == "floor")
            {
                CmdDestroy();
            }
        }

       
    }

    [Command]
    public void CmdForce(Vector3 speed)
    {
        if (GetComponent<NetworkIdentity>().clientAuthorityOwner == null)
        {
            //speed = new Vector3(Mathf.Clamp(speed.x, speed.x, 100), Mathf.Clamp(speed.y, speed.y, 100), Mathf.Clamp(speed.z, speed.z, 100));
            GetComponent<Rigidbody>().AddForce(speed);
        }
        else
        {
            this.speed = speed;
            speedSet = true;
        }
    }

    [Command]
    public void CmdDestroy()
    {
        Destroy(this.gameObject);
    }
}
