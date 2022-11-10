/*****************************************************************************
// File Name :         MenuController.cs
// Author :            Craig D. Hughes
// Creation Date :     May 6, 2022
//
// Brief Description : This script controls scene transitions and button
//                     controls like quitting the game
//                  
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Tooltip("The sound buttons make when they are pressed")]
    public AudioClip ButtonNoise;
    // Start is called before the first frame update
    void Start()
    {
        //if the player has no color then it sets the color to red
        if(PlayerPrefs.GetString("CurrentColor").Equals(""))
        {
            PlayerPrefs.SetString("CurrentColor", "Red");
            PlayerPrefs.SetInt("HasRed", 1);
            PlayerPrefs.SetString("BikeColor", "Red");
        }
    }
    private void Update()
    {
        //DEBUG CODE
        //if the player presses 2 it restarts the player prefs
        if(Input.GetKeyDown(KeyCode.T))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    /// <summary>
    /// change the scene to the scene given by name to this method
    /// </summary>
    /// <param name="name">the name of the scene that must be loaded</param>
    public void MoveScene(string name)
    {
        //the noise that plays when changing scene
        AudioSource.PlayClipAtPoint(ButtonNoise, Camera.main.transform.position);
        //change the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    /// <summary>
    /// exit the game
    /// called from a button
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
