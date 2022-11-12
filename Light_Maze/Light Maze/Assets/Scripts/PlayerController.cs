/*****************************************************************************
// File Name :         PlayerController.cs
// Author :            Craig D. Hughes
// Creation Date :     May 6, 2022
//
// Brief Description : This script handles player movement, data, and condition
//                  
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    //check if the player has already clicked any of the move buttons
    private bool HasMoved;
    //0 = up, 1 = down, 2 = left, 3 = right
    private int direction;
    [Tooltip("How fast the player moves")]
    public float speed;
    //the reference to the player's rigidbody2d
    private Rigidbody2D rb2d;
    [Tooltip("The object made behind the player")]
    public GameObject Line;
    //the reference to the gamecontroller
    private GameController GameCon;
    //the reference to the newest line's collider
    private Collider2D NextCollide;
    //the point that the last line ends
    private Vector2 LastLineEnd;
    //bool that keeps track of if the player has won or not
    private bool won = false;
    [Tooltip("The name of the current scene")]
    public string CurrentScene;
    [Tooltip("The speed when the player is slowing down")]
    public float speedControl;
    [Tooltip("is a tutorial level")]
    public bool tutorial;
    //represents if the speed needs to be slowed or not true = yes, false = no
    private bool breaks;
    //the maximum amount of time the player can use the break
    private float breakTimer;
    // a reference to the UI that displays how much time on the break timer is left
    private GameObject BreakSlider;
    [Tooltip("The sound made when the player changes direction")]
    public AudioClip Turn;
    [Tooltip("The sound made when the player wins")]
    public AudioClip WinSound;
    [Tooltip("The sound made when the player loses")]
    public AudioClip LossSound;
    [Tooltip("The sound the coin makes when collided with")]
    public AudioClip CoinSound;
    
    /// <summary>
    /// set the variables to their starting values
    /// get the object references
    /// </summary>
    void Start()
    {
        //variables
        breakTimer = 3f;
        tutorial = false;
        HasMoved = false;
        direction = -1;
        //object references 
        BreakSlider = GameObject.Find("BreakSlider");
        BreakSlider.GetComponent<Slider>().maxValue = breakTimer;
        BreakSlider.GetComponent<Slider>().value = breakTimer;
        GameCon = GameObject.Find("GameController").GetComponent<GameController>();
        rb2d = GetComponent<Rigidbody2D>();

       //look at the player prefs to see what the bike color is supposed to be set to
       //then set the bike and line color to the color saved in player prefs
       switch (PlayerPrefs.GetString("BikeColor"))
        {
            case "Red":
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                Line.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case "Blue":
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                Line.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case "Green":
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                Line.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case "Purple":
                gameObject.GetComponent<SpriteRenderer>().color = new Color32(154, 0, 255, 255);
                Line.gameObject.GetComponent<SpriteRenderer>().color = new Color32(154, 0, 255, 255);
                break;
            case "Orange":
                gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 160, 0, 255);
                Line.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 160, 0, 255);
                break;
            case "Yellow":
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                Line.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case "Cyan":
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 250, 255);
                Line.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 250, 255);
                break;
            case "Magenta":
                gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                Line.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
            case "Pink":
                gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 100, 166, 255);
                Line.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 100, 166, 255);
                break;

        }

    }

    /// <summary>
    /// during the update the change direction function is called, then the line is drawn behind the player
    /// then the method that checks the breaks is called
    /// </summary>
    void Update()
    {
        CheckDirection();
        if(HasMoved)
        {
            FitColliderBetween(NextCollide, LastLineEnd, transform.position);
        }
        if(!tutorial)
        {
            CheckShift();
        }
    }

    /// <summary>
    /// every fixed update the movement is called 
    /// </summary>
    private void FixedUpdate()
    {
        CheckMovement();
    }

    /// <summary>
    /// this method controls the break speed by checking if the player is pressing shift or not
    /// </summary>
    private void CheckShift()
    {
        //if the player is pressing shift and they still have break time left, the breaks are activated 
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && breakTimer > 0f && HasMoved)
        {
            breaks = true;
            breakTimer -= Time.deltaTime; //tik down the breaktimer
            BreakSlider.GetComponent<Slider>().value = breakTimer; //display new break timer
        }
        else
        {
            breaks = false;
        }
    }

    /// <summary>
    /// this method controls the speed and direction the player is moving
    /// </summary>
    private void CheckMovement()
    {
        float localSpeed;
        if ((breaks))//slow down
        {
            localSpeed = speed / 3f + 2f;
        }
        else//normal speed
        {
            localSpeed = speed;
        }
        if (HasMoved && !won)//move in the chosen direction
        {
            switch (direction)
            {

                case 0://up
                    rb2d.velocity = new Vector3(0,localSpeed,0);
                    break;
                case 1://down
                    rb2d.velocity = new Vector3(0, -localSpeed, 0);
                    break;
                case 2://left
                    rb2d.velocity = new Vector3(-localSpeed, 0, 0);
                    break;
                case 3://right
                    rb2d.velocity = new Vector3(localSpeed, 0, 0);
                    break;
            }

        }
        else //stay still if all else fails
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
        if (!won)
        {
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && direction != 1 && direction != 0)//Switch to up
            {
                direction = 0;
                HasMoved = true;
                CreateLine();
                AudioSource.PlayClipAtPoint(Turn, Camera.main.transform.position);
            }
            else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && direction != 3 && direction != 2)//Switch to Left
            {
                direction = 2;
                HasMoved = true;
                CreateLine();
                AudioSource.PlayClipAtPoint(Turn, Camera.main.transform.position);
            }
            else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && direction != 0 && direction != 1)//Switch to down
            {
                direction = 1;
                HasMoved = true;
                CreateLine();
                AudioSource.PlayClipAtPoint(Turn, Camera.main.transform.position);
            }
            else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && direction != 2 && direction != 3)//Switch to right
            {
                direction = 3;
                HasMoved = true;
                CreateLine();
                AudioSource.PlayClipAtPoint(Turn, Camera.main.transform.position);
            }
            else if((Input.GetKeyDown(KeyCode.Escape)))//leave the scene
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Selection Scene");
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
        GameObject G = Instantiate(Line, transform.position, Quaternion.identity); //make a new line
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

        if(a.x != b.x)//increase it vertically
        {
            co.transform.localScale = new Vector2(distance + .5f,.5f);
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
        if((collision.gameObject.tag.Equals("Line") && collision != NextCollide))//if you collide with a line that you aren't currently making
        {
            Invoke("DelayEnd", .75f);
            won = true;
            AudioSource.PlayClipAtPoint(LossSound, Camera.main.transform.position);
        }
        if (collision.gameObject.tag.Equals("Coin"))//collect the coin and then destroy it in the scene
        {
            Destroy(collision.gameObject);
            collision.gameObject.GetComponent<CoinBehaviour>().DeactivateCoin();
            AudioSource.PlayClipAtPoint(CoinSound, Camera.main.transform.position);
            PlayerPrefs.SetInt("wallet", PlayerPrefs.GetInt("wallet") + 1);
        }
    }

    /// <summary>
    /// this script handles collisions with other colliders and differentiates them by tags
    /// </summary>
    /// <param name="collision">the collider the object is interacting with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Wall"))//restart the game
        {
            Invoke("DelayEnd", .75f);
            won = true;
            AudioSource.PlayClipAtPoint(LossSound, Camera.main.transform.position);

        }
        if (collision.gameObject.tag.Equals("Goal") && collision.gameObject.GetComponent<SpriteRenderer>().color == Color.blue)//win the game
        {
            GameCon.ActivateWin();
            PlayerPrefs.SetInt(CurrentScene, 1);
            won = true;
            AudioSource.PlayClipAtPoint(WinSound, Camera.main.transform.position);
        }
        else//if you hit anything else it's a loss
        {
            Invoke("DelayEnd", .75f);
            won = true;
            AudioSource.PlayClipAtPoint(LossSound, Camera.main.transform.position);
        }
    }

    /// <summary>
    /// called to change the scene and leave the current level
    /// </summary>
    private void DelayEnd()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene);
    }

}
