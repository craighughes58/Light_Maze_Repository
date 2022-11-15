/*****************************************************************************
// File Name :         VersusPlayer.cs
// Author :            Craig D. Hughes
// Creation Date :     July 21, 2022
//
// Brief Description : This script is a modified version of the single
//                     player controller that handles two versions of the
//                     player so that they can fight eachother in the same
//                     scene
//
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VersusPlayer : MonoBehaviour
{
    //represents if the players are able to move
    private bool HasMoved;
    //0 = up, 1 = down, 2 = left, 3 = right
    private int direction;
    [Tooltip("How fast the player's movement is modified")]
    public float speed;
    //The reference to the object's rigidbody
    private Rigidbody2D rb2d;
    [Tooltip("The object made behind the player")]
    public GameObject Line;
    //a reference to the game controller that has been modified for the versus games
    private VsGameController GameCon;
    //the reference to the newest line's collider
    private Collider2D NextCollide;
    //the point that the last line ends
    private Vector2 LastLineEnd;
    //bool that keeps track of if the player has won or not
    private bool won = false;
    [Tooltip("The name of the current scene")]
    public string CurrentScene;
    [Tooltip("Represents if the player is the first or second")]
    public int playerNum;
    [Tooltip("reference to the player's slider UI")]
    public Slider SpeedSlider;
    //how much time the player can spend sped up
    private float speedTime;
    //represents if the player is holding down the speed button
    private bool isSpeeding;
    //represents if the game has ended
    private bool hasEnded;
    [Tooltip("The sound the player makes when they turn")]
    public AudioClip Turn;

    /// <summary>
    /// set the variables to their starting values
    /// get the object references
    /// </summary>
    void Start()
    {
        //starting variable values
        HasMoved = false;
        hasEnded = false;
        isSpeeding = false;
        //object references
        GameCon = GameObject.Find("GameController").GetComponent<VsGameController>();
        rb2d = GetComponent<Rigidbody2D>();
        //make starting line
        CreateLine();
        //change color based on player
        if(playerNum == 1)
        {
            Line.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            Line.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        //setup speed timer
        speedTime = SpeedSlider.GetComponent<Slider>().maxValue;
        SpeedSlider.GetComponent<Slider>().value = speedTime;

    }

    /// <summary>
    /// during the update the change direction function is called, then the line is drawn behind the player
    /// then the method that checks the speed boost is called
    /// </summary>
    void Update()
    {
        CheckDirection();
        if (HasMoved)
        {
            FitColliderBetween(NextCollide, LastLineEnd, transform.position);
        }
        CheckSpeedBoost();
    }

    /// <summary>
    /// This script is called in update
    /// if the player is pressing their speed boost button, the isSpeeding variable is set to true, the timer starts to tick down, and the UI is updated
    /// </summary>
    private void CheckSpeedBoost()
    {

        if(((playerNum == 1 && Input.GetKey(KeyCode.LeftShift) && speedTime > 0f) || (playerNum == 2 && Input.GetKey(KeyCode.RightShift) && speedTime > 0f)) && HasMoved && !won)//speedboost
        {
            isSpeeding = true;
            speedTime -= Time.deltaTime;
            SpeedSlider.GetComponent<Slider>().value = speedTime;

        }
        else//normal movement
        {
            isSpeeding = false; 
        }
    }

    /// <summary>
    /// This script is called by the vs game controller
    /// it sets the starting directions of the two players
    /// </summary>
    public void StartMove()
    {
        HasMoved = true;
        if(playerNum == 1)//player 1
        {
            direction = 0;
        }
        else //player 2
        {
            direction = 1;
        }
    }

    /// <summary>
    /// Every fixed  update the movement method is called
    /// </summary>
    private void FixedUpdate()
    {
        CheckMovement();
    }

    /// <summary>
    /// This script is called in the fixed update
    /// this script controls movement and movement modifications from speed boost
    /// </summary>
    private void CheckMovement()
    {
        int modifier = 0;//speed boost modifier
        if(isSpeeding)
        {
            modifier = 2;
        }

        if (HasMoved && !won)
        {
            switch (direction)
            {

                case 0://up
                    rb2d.velocity = new Vector3(0, speed + modifier, 0);
                    break;
                case 1://down
                    rb2d.velocity = new Vector3(0, -speed + modifier, 0);
                    break;
                case 2://left
                    rb2d.velocity = new Vector3(-speed + modifier, 0, 0);
                    break;
                case 3://right
                    rb2d.velocity = new Vector3(speed + modifier, 0, 0);
                    break;
            }

        }
        else//start of turn before game controller timer is done
        {
            rb2d.velocity = new Vector3(0, 0, 0);
        }
    }

    /// <summary>
    /// based on inputs (WASD) the direction is checekd and changed during fixed update  
    /// every time the direction is changed the line behind the player has to be cut off and a new one must be made
    /// </summary>
    private void CheckDirection()
    {
        if (!won && HasMoved)//if the player is moving and no one has won
        {
            if (((Input.GetKeyDown(KeyCode.W) && playerNum == 1) || (Input.GetKeyDown(KeyCode.UpArrow) && playerNum == 2)) && direction != 1)//Switch to up
            {
                direction = 0;
                //HasMoved = true;
                CreateLine();
                AudioSource.PlayClipAtPoint(Turn, Camera.main.transform.position);
            }
            else if (((Input.GetKeyDown(KeyCode.A) && playerNum == 1) || (Input.GetKeyDown(KeyCode.LeftArrow) && playerNum == 2)) && direction != 3)//Switch to Left
            {
                direction = 2;
                //HasMoved = true;
                CreateLine();
                AudioSource.PlayClipAtPoint(Turn, Camera.main.transform.position);
            }
            else if (((Input.GetKeyDown(KeyCode.S) && playerNum == 1) || (Input.GetKeyDown(KeyCode.DownArrow) && playerNum == 2)) && direction != 0)//Switch to down
            {
                direction = 1;
                //HasMoved = true;
                CreateLine();
                AudioSource.PlayClipAtPoint(Turn, Camera.main.transform.position);
            }
            else if (((Input.GetKeyDown(KeyCode.D) && playerNum == 1) || (Input.GetKeyDown(KeyCode.RightArrow) && playerNum == 2)) && direction != 2)//Switch to right
            {
                direction = 3;
                //HasMoved = true;
                CreateLine();
                AudioSource.PlayClipAtPoint(Turn, Camera.main.transform.position);
            }
        }
    }

    /// <summary>
    /// this is called when the direction change
    /// end the line currently being altered
    /// create a new line and then give it to the script to alter
    /// </summary>
    private void CreateLine()
    {
        LastLineEnd = transform.position;//set the end of the current line
        GameObject G = Instantiate(Line, transform.position, Quaternion.identity);//make a new line
        NextCollide = G.GetComponent<Collider2D>();//give the collider to the script in order ro stretch it
    }

    /// <summary>
    /// this script is given the starting and ending point of a collider the shapes the collder between the two points
    /// </summary>
    /// <param name="co">the collider of the newest line</param>
    /// <param name="a">point 1</param>
    /// <param name="b">point 2</param>
    private void FitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        co.transform.position = a + (b - a) * .5f;
        float distance = Vector2.Distance(a, b);

        if (a.x != b.x)//increase it vertically
        {
            co.transform.localScale = new Vector2(distance + .5f, .5f);
        }
        else//increase it horizontally
        {
            co.transform.localScale = new Vector2(.5f, distance + .5f);
        }
    }

    /// <summary>
    /// this script handles triggers with other colliders and differentiates them by tags
    /// </summary>
    /// <param name="collision">the collider the object is interacting with</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag.Equals("Line") && collision != NextCollide) && !hasEnded)//triggering with a line ends the game
        {
            EndGame();
            //UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene);
        }
    }

    /// <summary>
    /// this script handles collision with other colliders and differentiates them by tags
    /// </summary>
    /// <param name="collision">the collider the object is interacting with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall") && !hasEnded)//colliding with a wall ends the game
        {
            EndGame();
            //UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene);
        }
    }


    /// <summary>
    /// This script ends the game on the player's script and notifies the gamecontroller to reset
    /// </summary>
    private void EndGame()
    {
        //player cleanup
        hasEnded = true;
        CreateLine();
        //notify the game controller
        if(playerNum == 1)
        {
            GameCon.ActivateEnd(2);
        }
        else
        {
            GameCon.ActivateEnd(1);
        }
    }

    /// <summary>
    /// called externally to set the won bool to true
    /// </summary>
    public void HasWonActivate()
    {
        won = true;
    }

}
