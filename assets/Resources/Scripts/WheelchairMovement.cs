using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairMovement : MonoBehaviour {
    Rigidbody rightWheel;
    Rigidbody leftWheel;
    bool rightWheelTouched;
    private bool leftWheelTouched;
    bool rightTrigger;
    public bool leftTrigger;
    GameObject rightController;
    SteamVR_TrackedController rightTracker;
    GameObject leftController;
    SteamVR_TrackedController leftTracker;
    Vector3 rightTouchPos;
    Vector3 lastRightPos;
    Vector3 leftTouchPos;
    Vector3 lastLeftPos;
    GameObject playerController;
    GameObject middle;

    // Use this for initialization
    void Start () {
        Debug.Log("start");
        rightWheel = GameObject.FindWithTag("rightWheel").GetComponent<Rigidbody>();
        leftWheel = GameObject.FindWithTag("leftWheel").GetComponent<Rigidbody>();
        rightWheelTouched = false;
        leftWheelTouched = false;
        rightController = GameObject.FindWithTag("right");
        rightTracker = rightController.GetComponent<SteamVR_TrackedController>();
        leftController = GameObject.FindWithTag("left");
        leftTracker = leftController.GetComponent<SteamVR_TrackedController>();
        playerController = GameObject.FindWithTag("vive");
        playerController.transform.position = this.transform.position + new Vector3(0.0f, 5.0f, 0.0f);
        Vector3 chairRotation = this.transform.rotation.eulerAngles;
        Vector3 playerRotation = playerController.transform.rotation.eulerAngles;
        playerController.transform.rotation = Quaternion.Euler(playerRotation.x, chairRotation.y, playerRotation.z);
        leftTouchPos = new Vector3(0, 0, 0);
        rightTouchPos = new Vector3(0, 0, 0);
        lastLeftPos = Vector3.zero;
        lastRightPos = Vector3.zero;
        middle = GameObject.FindWithTag("middle");
        rightWheel = this.transform.GetChild(2).GetComponent<Rigidbody>();
        leftWheel = this.transform.GetChild(0).GetComponent<Rigidbody>();
        middle = this.transform.GetChild(1).gameObject;
    }
  


    // Update is called once per frame
    void Update() {

        if (Input.GetKey(KeyCode.A))
        {
            leftWheel.AddForce(middle.transform.forward  * 2);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rightWheel.AddForce(middle.transform.forward * 2);
        }
        if (rightTracker != null)
        {
            rightTrigger = rightTracker.triggerPressed;
        }
        if(leftTracker != null)
        {
            leftTrigger = leftTracker.triggerPressed;
        }

        /*Debug.Log("leftTrigger = " + leftTrigger);
        Debug.Log("rightTrigger = " + rightTrigger);
        Debug.Log("leftWheelTouched = " + leftWheelTouched);
        Debug.Log("rightWheelTouched = " + rightWheelTouched);*/
        ;
        if (rightWheelTouched && rightTrigger)
        {
            //rightWheel.AddForce(transform.forward * 7.0f);
            Vector3 curPos = rightController.transform.localPosition;
            if(rightTouchPos.x == 0 && rightTouchPos.y == 0 && rightTouchPos.z == 0)
            {
                rightTouchPos = curPos;
            } 
            else
            {
                if (curPos != rightTouchPos)
                {
                    Vector3 diff = curPos - rightTouchPos;
                    float diffLen = diff.magnitude;
                  
                    rightWheel.AddForce(middle.transform.forward * Mathf.Sign(diff.z) * diffLen * 30.0f);
                }
            }
        }
        else
        {
            rightTouchPos = Vector3.zero;
        }

        if (leftWheelTouched && leftTrigger)
        {
            //leftWheel.AddForce(transform.forward * 7.0f);
            Vector3 curPos = leftController.transform.localPosition;
            if (leftTouchPos.x == 0 && leftTouchPos.y == 0 && leftTouchPos.z == 0)
            {
                leftTouchPos = curPos;
            }
            else
            {
                if (curPos != leftTouchPos)
                {
                    Vector3 diff = curPos - leftTouchPos;
                    float diffLen = diff.magnitude;
                    leftWheel.AddForce(middle.transform.forward * Mathf.Sign(diff.z) * diffLen * 30.0f);
                }
            }
        }
        else
        {
            leftTouchPos = Vector3.zero;
        }
        //this.transform.position = middle.transform.position;
        Vector3 chairRotation = middle.transform.rotation.eulerAngles;
        Vector3 newPlayerPosition = Vector3.Scale(middle.transform.position, new Vector3(chairRotation.x, 0.0f, chairRotation.z));
        // playerController.transform.position = middle.transform.position - Vector3.Scale(new Vector3(0.0f, 0.8f, 0.6f), new Vector3(0.0f, chairRotation.y, 0.0f));
        playerController.transform.position = middle.transform.position - middle.transform.rotation * new Vector3(0.1f, 0.7f, -0.3f);
        //playerController.transform.position = newPlayerPosition - new Vector3(0.0f,0.0f,0.0f);
        Vector3 playerRotation = playerController.transform.rotation.eulerAngles;
        //Debug.Log(chairRotation.y + " " + playerRotation.y);
        playerController.transform.rotation = Quaternion.Euler(playerRotation.x, chairRotation.y, playerRotation.z);
        //Debug.Log(middle.transform.forward);
    }

    public void SetRightWheelTouched(bool rightWheelTouched)
    {
        this.rightWheelTouched = rightWheelTouched;
    }

    public void SetLeftWheelTouched(bool leftWheelTouched)
    {
        this.leftWheelTouched = leftWheelTouched;
    }

    public void SetRightController(GameObject rightController, SteamVR_TrackedController rightTracker)
    {
        this.rightController = rightController;
        this.rightTracker = rightTracker;
    }

    public void SetLeftController(GameObject leftController, SteamVR_TrackedController leftTracker)
    {
        this.leftController = leftController;
        this.leftTracker = leftTracker;
    }
}
