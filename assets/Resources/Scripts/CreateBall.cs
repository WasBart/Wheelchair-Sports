using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using UnityEngine;
using UnityEngine.Networking;

public class CreateBall : NetworkBehaviour{

    string[] balls = { "Basketball", "Volleyball", "Football" };
    private int active = -1;
    private PinchDetector pd;
    public bool inZone = false;
    public GameObject ball;
    private ButtonManager bM;
    private bool ballAtLeap = false;
   

    public int activeBall {
        set{active = value;}
        get { return active; }
    }

    public bool setInZone
    {
        set { inZone = value; }
    }

    public PinchDetector setPinchDetector
    {
        set { pd = value; }
    }
    // Use this for initialization
    void Start () {
        bM = GameObject.FindGameObjectWithTag("terminal").GetComponent<ButtonManager>();
        
	}

    public void deleteBall()
    {
        //GameObject.Destroy(ball);
        ball = null;
        CmdDeleteBall();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("ball is istantiated");
            CmdSpawn(1, this.transform.position);
            ballAtLeap = true;
        }

        if (pd != null)
        {
            
               
            
            if (inZone && pd.IsPinching)
            {

                if (bM != null)
                {
                    active = bM.GetActiveMode;
                }

                if (active > -1 && ball == null)
                {

                    GameObject tempBall = Resources.Load("Prefabs/" + balls[active]) as GameObject;

                    /*if (ball == null)
                    {
                        Debug.Log("ball is istantiated");
                        ball = GameObject.Instantiate(tempBall, pd.transform.GetChild(0).position, pd.transform.GetChild(0).rotation);
                        ball.name = balls[active];
                        ballAtLeap = true;

                    }*/

                    if (ball == null)
                    {
                        Debug.Log("ball is istantiated");
                        CmdSpawn(active, pd.transform.GetChild(0).position + new Vector3(0.1f,0.1f,0.1f));
                        ballAtLeap = true;
                        


                    }
                }
            }

        }
    }

    [Command]
    void CmdSpawn(int active, Vector3 position)
    {
        if (ball == null)
        {
            GameObject tempBall = Resources.Load("Prefabs/" + balls[active]) as GameObject;

            ball = GameObject.Instantiate(tempBall, position, Quaternion.identity);
            ball.name = balls[active];
           

            NetworkServer.SpawnWithClientAuthority(ball, connectionToClient);
        }
    }

    [Command]
    void CmdDeleteBall()
    {
        Destroy(ball);
        ball = null;
    }
}





