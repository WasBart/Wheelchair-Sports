using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour {

    // Use this for initiWalization
    private bool active = false;
    private ButtonManager buttonManager;
    private Vector3 startingPos;
    public int mode = -1;

 

    public bool isActive
    {
        set { active = value; }
        get { return active; }
    }

    public ButtonManager setButtonManager
    {
        set { this.buttonManager = value; }
    }

    void Start () {
        startingPos = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "leftLeap" || other.tag == "rightLeap")
        {
            Debug.Log("trigger is entering");
            active = true;
            buttonManager.ChangeActiveButton(this.GetComponent<ButtonInteraction>());
            setPosition();
        }
        
    }

    public void setPosition()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
    }

    public void resetPosition()
    {
        active = false;
        transform.position = startingPos;
    }
}
