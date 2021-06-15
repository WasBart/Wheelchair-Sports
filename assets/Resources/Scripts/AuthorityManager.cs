using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;


// TODO: this script should manage authority for a shared object
public class AuthorityManager : NetworkBehaviour {

    
    NetworkIdentity netID; // NetworkIdentity component attached to this game object

    // these variables should be set up on a client
    //**************************************************************************************************
    Actor localActor; // Actor that is steering this player 

    private bool grabbed = false; // if this is true client authority for the object should be requested
    public bool grabbedByPlayer // private "grabbed" field can be accessed from other scripts through grabbedByPlayer
    {
        get { return grabbed; }
        set { grabbed = value; }
    }

    OnGrabbedBehaviour onb; // component defining the behaviour of this GO when it is grabbed by a player
                            // this component can implement different functionality for different GO´s


    public bool wasGrabbed = false;
    //***************************************************************************************************

    // these variables should be set up on the server

    // TODO: implement a mechanism for storing consequent authority requests from different clients
    // e.g. manage a situation where a client requests authority over an object that is currently being manipulated by another client
    LinkedList<NetworkConnection> requests = new LinkedList<NetworkConnection>();
    NetworkConnection connectedTo = null;
    bool removeAuthoriy;
    //*****************************************************************************************************

    // TODO: avoid sending two or more consecutive RemoveClientAuthority or AssignClientAUthority commands for the same client and shared object
    // a mechanism preventing such situations can be implemented either on the client or on the server

    // Use this for initialization
    void Start () {
    
     netID = this.GetComponent<NetworkIdentity>();

        if (isClient)
        {
            onb = this.GetComponent<OnGrabbedBehaviour>();
        }  
    }

    // Update is called once per frame
    void Update()
    {
       

        if (isClient)
        {
           
            if (grabbed)
            {
               
                if (localActor != null && !wasGrabbed)
                {
                    Debug.Log("request object authority in client");
                    localActor.RequestObjectAuthority(netID);
                    wasGrabbed = true;
                   
                   
                }

            }
            else
            {
                if (localActor != null && wasGrabbed)
                {
                    Debug.Log("return object authority in client");
                    localActor.ReturnObjectAuthority(netID);
                    wasGrabbed = false;
                    
                 
                }
            }

        }
        if (isServer)
        {

           //Other objects want the box
            if (requests.Count > 0)
            {
                RemoveClientAuthority(connectedTo);
                connectedTo = (NetworkConnection)requests.First.Value;
                requests.RemoveFirst();
                netID.AssignClientAuthority(connectedTo);
                netID.GetComponent<Rigidbody>().isKinematic = true;
                RpcisKinematic(true);
            }
        }
    }

    // assign localActor here
    public void AssignActor(Actor actor)
    {
        this.localActor = actor;   
    }

    public Actor GetActor()
    {
        return this.localActor;
    }

    // should only be called on server (by an Actor)
    // assign the authority over this game object to a client with NetworkConnection conn
    public void AssignClientAuthority(NetworkConnection conn)
    {
        requests.AddLast(conn);
   
    }

    // should only be called on server (by an Actor)
    // remove the authority over this game object from a client with NetworkConnection conn
    public void RemoveClientAuthority(NetworkConnection conn)
    {
        //Player letting go of object:
        if(conn == connectedTo)
        {
             
            netID.RemoveClientAuthority(connectedTo);
            netID.GetComponent<Rigidbody>().isKinematic = false;
            RpcisKinematic(false);
            connectedTo = null;
           
        }
        else
        {
            if (requests.Contains(conn))
            {
                requests.Remove(conn);
            }
        }


    }
    [ClientRpc]
    void RpcisKinematic(bool value)
    {
        Debug.Log("rpc called with :" + value);
        netID.GetComponent<Rigidbody>().isKinematic = value;
    }

}


