using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {
    ButtonInteraction[] buttons;
    private ButtonInteraction active;
    private int activeMode = -1;
    // Use this for initialization
    public int GetActiveMode
    {
        get { return activeMode; }
    }

    void Start () {
        
        GameObject[] temp = GameObject.FindGameObjectsWithTag("button");
         if(temp != null)
        {
            buttons = new ButtonInteraction[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                
                buttons[i] = temp[i].GetComponent<ButtonInteraction>();
                buttons[i].setButtonManager = this;
            }
        }
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeActiveButton(ButtonInteraction newActive)
    {
        //print("old: " + active.gameObject == null ?"nothing" : active.gameObject.name + " new: " +newActive.gameObject.name);
        if (active != null)
        {
           active.resetPosition();
        }
        Debug.Log("changeActiveButton" + newActive.mode);
        active = newActive;
        activeMode = active.mode;
       
    }
}
