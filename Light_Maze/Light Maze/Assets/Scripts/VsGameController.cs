/*****************************************************************************
// File Name :         VsGameController.cs
// Author :            Craig D. Hughes
// Creation Date :     July 21, 2022
//
// Brief Description : 
//                     
//                     
//                     
//
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VsGameController : MonoBehaviour
{
    [Tooltip("The reference to the text that displays the timer at the start of the game")]
    public Text TimerText;
    [Tooltip("The text that displays who won at the start of the game")]
    public Text WinText;
    [Tooltip("How much time the timer has at the start of the game")]
    public float TimerStartTime;
    //The actual timer that keeps track of the start game
    private float Timer;
    //the list of players in the game 
    private List<GameObject> Players= new List<GameObject>();
    //represents if the game has begun or not after the timer
    private bool begin = false;
    //represents if the game has begun or not after the timer
    private static bool started = false;
    [Tooltip("The name of the current scene")]
    public string CurrentScene;
    //the score of the first player
    private Text p1ScoreText;
    //the score of the second player
    private Text p2ScoreText;
    [Tooltip("the sound that's played after a player wins a round")]
    public AudioClip Win;
    [Tooltip("the sound that plays after the player wins the entire game")]
    public AudioClip MegaWin;
    
    /// <summary>
    /// set the starting values for the variables and then find the references for the objects
    /// </summary>
    void Start()
    {
        //object references
        p1ScoreText = GameObject.Find("P1Score").GetComponent<Text>();
        p2ScoreText = GameObject.Find("P2Score").GetComponent<Text>();
        //starting variable values
        if (!started)
        {
            PlayerPrefs.SetInt("P1Wins", 0);
            PlayerPrefs.SetInt("P2Wins", 0);
        }
        started = true;
        Players.Add(GameObject.Find("PlayerVS 1"));
        Players.Add(GameObject.Find("PlayerVS 2"));
        Timer = TimerStartTime;
        WinText.enabled = false;
        p1ScoreText.text = PlayerPrefs.GetInt("P1Wins").ToString();
        p2ScoreText.text = PlayerPrefs.GetInt("P2Wins").ToString();

    }

    /// <summary>
    /// during uodate the game will tick down the starting timer or 
    /// check if the player is trying to exit
    /// </summary>
    void Update()
    {
        TickDownStart();
        CheckExit();
    }

    /// <summary>
    /// if the player is hitting escape then the game exits the current scene
    /// </summary>
    private void CheckExit()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //resets the player prefs
            PlayerPrefs.SetInt("P1Wins", 0);
            PlayerPrefs.SetInt("P2Wins",0);
            //change scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("VS Select");
        }
    }

    /// <summary>
    /// called during update
    /// if the game hasn't began then tick down the timer
    /// if the timer reaches zero then start the game and contact the players to start their script
    /// </summary>
    private void TickDownStart()
    {
        if(!begin)
        {
            //tick down timer
            Timer -= Time.deltaTime;
            TimerText.text = (int)Timer + "";
            //start the game if it's at zero
            if (Timer <= 1f)
            {
                foreach (GameObject G in Players)
                {
                    G.GetComponent<VersusPlayer>().StartMove();
                }
                begin = true;
                TimerText.enabled = false; //turn off ui
            }
        }
    }

    /// <summary>
    /// THis method handles wins at the end of each and checks if someone beat the game
    /// </summary>
    /// <param name="Winner"></param>
    public void ActivateEnd(int Winner)
    {
        WinText.enabled = true;
        /*        if(Winner == 1)
                {
                    WinText.color = Color.red;
                }
                else
                {
                    WinText.color = Color.blue;
                }*/
        WinText.color = Color.magenta;
        WinText.text = "Player " + Winner + " wins!";
        foreach (GameObject G in Players)
        {
            G.GetComponent<VersusPlayer>().HasWonActivate();
        }

        if(Winner == 1)//player 1 wins
        {
            PlayerPrefs.SetInt("P1Wins", PlayerPrefs.GetInt("P1Wins") + 1);
            AudioSource.PlayClipAtPoint(Win, Camera.main.transform.position);
        }
        else//player 2 wins
        {
            PlayerPrefs.SetInt("P2Wins", PlayerPrefs.GetInt("P2Wins") + 1);
            AudioSource.PlayClipAtPoint(Win, Camera.main.transform.position);
        }
        if (PlayerPrefs.GetInt("P2Wins") >= 3 || PlayerPrefs.GetInt("P1Wins") >= 3)//if either player has past three points
        {
            if (PlayerPrefs.GetInt("P1Wins") >= 3 && !(PlayerPrefs.GetInt("P2Wins") >= 3))//player 1 wins
            {
                WinText.text = "PLAYER 1 IS THE WINNER!";
                AudioSource.PlayClipAtPoint(MegaWin, Camera.main.transform.position);
            }
            else if (PlayerPrefs.GetInt("P2Wins") >= 3 && !(PlayerPrefs.GetInt("P1Wins") >= 3)) //player 2 wins
            {
                WinText.text = "PLAYER 2 IS THE WINNER!";
                AudioSource.PlayClipAtPoint(MegaWin, Camera.main.transform.position);
            }
            else//tie
            {
                WinText.text = "TIE";
            }
            Invoke("EndGame", 2f);
        }
        else//if no one won restart the game 
        {
            Invoke("ResetGame", 2f);
        }
        //update the UI to the new scores
        p1ScoreText.text = PlayerPrefs.GetInt("P1Wins").ToString();
        p2ScoreText.text = PlayerPrefs.GetInt("P2Wins").ToString();
    }

    /// <summary>
    /// This script is called at the ACtivate End scrpt and sets the game back to its starting condition
    /// </summary>
    private void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene);
    }

    /// <summary>
    /// This script is called at the ACtivate End scrpt and switches the scene back to the level select
    /// </summary>
    private void EndGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("VS Select");
    }

}
