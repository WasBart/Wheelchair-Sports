using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class LeftPinchInit : MonoBehaviour
{

    List<GameObject> sharedObjects;
    private PinchDetector pinchDetector;

    bool pdSet = false;



    // Update is called once per frame
    void Update()
    {
        if (!pdSet)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("shared");
            sharedObjects = new List<GameObject>(gameObjects);
            pinchDetector = this.gameObject.GetComponent<PinchDetector>();
     
            if (sharedObjects.Count > 0)
            {
                for (int i = 0; i < sharedObjects.Count; i++)
                {

                    sharedObjects[i].GetComponent<LeapGrab>().pdLeft = pinchDetector;
                }
                pdSet = true;
            }
        }
    }

    void OnEnable()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("shared");
        sharedObjects = new List<GameObject>(gameObjects);
        pinchDetector = this.gameObject.GetComponent<PinchDetector>();
        if (sharedObjects.Count > 0)
        {
            for (int i = 0; i < sharedObjects.Count; i++)
            {

                sharedObjects[i].GetComponent<LeapGrab>().pdLeft = pinchDetector;
            }
            pdSet = true;
        }
    }
}
