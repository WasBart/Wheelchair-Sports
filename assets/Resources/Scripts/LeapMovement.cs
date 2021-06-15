using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapMovement : MonoBehaviour {
    public bool rotateLeft = false;
    public bool rotateRight = false;
    public bool moveForward = false;

    private float rotationSpeed = 40.0f;
    private float movingSpeed = 2.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (rotateLeft)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * -rotationSpeed, Space.World);
        }

        else if (rotateRight)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed, Space.World);
        }

        if (moveForward)
        {
            Vector3 fakeForward = transform.forward;
            fakeForward.y = 0.0f;
            transform.Translate(fakeForward * movingSpeed * Time.deltaTime, Space.World);
        }
    }

    
    public void RotateLeft()
    {
       
        rotateLeft = true;
      
    }

    public void RotateRight()
    {
      
        rotateRight = true;
      
    }

    public void StopRotation()
    {
        if (rotateLeft)
        {
            rotateLeft = false;
        }
        else
        {
            rotateRight = false;
        }
    }

    public void MoveForward()
    {
       
        moveForward = true;
    }

    public void stopMoving()
    {
      
        moveForward = false;
    }
}
