/*****************************************************************************
// File Name :         GameController.cs
// Author :            Craig D. Hughes
// Creation Date :     May 6, 2022
//
// Brief Description : This script handles transitions and end conditions
//                     while the player is in a maze
//                  
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameController : MonoBehaviour
{
    [Tooltip("The name of the scene that will appear after this one")]
    public string NextSceneName;
    [Tooltip("The object on the canvas that appears when the player wins")]
    public Text WinText;
    //
    private Color lerpedColor = Color.white;


    // Start is called before the first frame update
    void Start()
    {
        //turn off the win text at the start of the game
        WinText.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        //tickdown
        // this line changes the color of the you win text
        lerpedColor = Color.Lerp(Color.blue, Color.magenta, Mathf.PingPong(Time.time, .5f));
        //this line sets the text color to the color above
        WinText.color = lerpedColor; 
    }

    /// <summary>
    /// 
    /// </summary>
    public void ActivateWin()
    {
        //show the win text
        WinText.enabled = true;
        //invoke the next scene so the player has time to see the you win text
        Invoke("ChangeToNextScene",1f);

    }

    /// <summary>
    /// Change the scene based on the next scene variable
    /// </summary>
    public void ChangeToNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneName);
    }
}
