using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CourtManager : NetworkBehaviour {
    private ArrayList goalPositions;
    private ArrayList goalTypes;
    public GameObject basket;
    public GameObject net;
    public GameObject target;
    public GameObject goal;
    private bool goalActive;
    public int points = 0;

	// Use this for initialization
	void Start () {
        goalPositions = new ArrayList();
        goalTypes = new ArrayList();
        goalTypes.Add("Prefabs/Basket");
        goalTypes.Add("Prefabs/Net");
        goalTypes.Add("Prefabs/Target");
        goalActive = false;

        // Delete after testing
        InitCourt(1);
       //SpawnGoal();
        
	}
	
	// Update is called once per frame
	void Update () {
        /*if(points < 12) {
		    if(points == 0)
            {
                InitCourt(1);
            }
            else if(points == 4)
            {
                DestroyCourt();
                InitCourt(2);
            }
            else if(points == 8)
            {
                DestroyCourt();
                InitCourt(3);
            }
            SpawnGoal();
            ClearGoal();
        } */
        if(!goalActive) {
            //SpawnGoal();
        }
    }

    void InitCourt(int level) {
        goalPositions.Add(GameObject.FindGameObjectWithTag("ngoal" + level));
        goalPositions.Add(GameObject.FindGameObjectWithTag("egoal" + level));
        goalPositions.Add(GameObject.FindGameObjectWithTag("sgoal" + level));
        goalPositions.Add(GameObject.FindGameObjectWithTag("wgoal" + level));
    }

    void DestroyCourt()
    {
        goalPositions = new ArrayList();
    }

    void SpawnGoal() {
        GameObject goalPos = (GameObject) goalPositions[Random.Range(0,4)];
        int goalType = Random.Range(0,3);
        goal = new GameObject();
        switch (goalType)
        {
            case 0:
                goal = Instantiate(basket, goalPos.transform.position, Quaternion.identity);
                break;
            case 1:
                goal = Instantiate(net, goalPos.transform.position, Quaternion.identity);
                break;
            case 2:
                goal = Instantiate(target, goalPos.transform.position, Quaternion.identity);
                break;
        }
        //Debug.Log("goalPos = " + goalPos);
        //Debug.Log("goalType = " + goalType);
        if(goal == null) {
            Debug.Log("Goal is empty");
        }
        if (NetworkServer.active)
        {
            Debug.Log("Hübsches Grünzeug");
            NetworkServer.Spawn(goal);
            goal.transform.parent = goalPos.transform;
        }
        Debug.Log(NetworkServer.active);
        goalActive = true;
    }

    public void ClearGoal() {
        if(goal != null)
        {
            Destroy(goal);
        }
        goalActive = false;
    }
}
