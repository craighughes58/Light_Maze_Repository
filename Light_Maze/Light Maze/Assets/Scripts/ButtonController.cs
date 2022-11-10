/*****************************************************************************
// File Name :         ButtonController.cs
// Author :            Craig D. Hughes
// Creation Date :     May 7, 2022
//
// Brief Description : This script changes the interactibility of buttons
//                     on the level select screen
//
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [Tooltip("This represents how many buttons there are")]
    public int ButtonNum;

    /// <summary>
    /// calls the method that sets the buttons to on or off
    /// </summary>
    void Start()
    {
        SetButtonsActive();
    }

    /// <summary>
    /// This method goes through each button and checks in the player prefs if the button
    /// should be active or not
    /// </summary>
    private void SetButtonsActive()
    {
        //this represents what number is the most up to date button
        int last = 1;
        for (int i = 1; i <=ButtonNum; i++) //go through each button
        {
            if(PlayerPrefs.GetInt("Level" + i) == 1) //if the level is completed, turn it onn
            {
                GameObject.Find("LvlBtn" + i).GetComponent<Button>().interactable = true;
                last = i + 1;
            }
            else //otherwise turn it off
            {
                GameObject.Find("LvlBtn" + i).GetComponent<Button>().interactable = false;
            }
        }
        if (last < 11)//set the next button to true so that the player can progress if they haven't completed all levels
        {
            GameObject.Find("LvlBtn" + (last)).GetComponent<Button>().interactable = true;
        }
    }
}
