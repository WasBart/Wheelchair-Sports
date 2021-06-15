using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class InitRightPinch : MonoBehaviour {
    private CreateBall cb;



    private void OnEnable()
    {
    }
        
    // Use this for initialization
    void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
       if(cb == null)
            {
                cb = gameObject.transform.root.GetComponentInChildren<CreateBall>();
                if(cb!= null)
                {
                    cb.setPinchDetector = GetComponent<PinchDetector>();
                }
                
            }
    }
	

    
}
