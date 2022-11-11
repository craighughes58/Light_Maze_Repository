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
    //
    private bool HasMoved;
    //0 = up, 1 = down, 2 = left, 3 = right
    private int direction;
    [Tooltip("")]
    public float speed;
    //
    private Rigidbody2D rb2d;
    [Tooltip("")]
    public GameObject Line;
    //
    private GameController GameCon;
    //
    private Collider2D NextCollide;
    //
    private Vector2 LastLineEnd;
    //
    private bool won = false;
    [Tooltip("")]
    public string CurrentScene;
    [Tooltip("")]
    public float speedControl;
    [Tooltip("")]
    public bool tutorial;
    //
    private bool breaks;
    //
    private float breakTimer;
    //
    private GameObject BreakSlider;
    [Tooltip("")]
    public AudioClip Turn;
    [Tooltip("")]
    public AudioClip WinSound;
    [Tooltip("")]
    public AudioClip LossSound;
    [Tooltip("")]
    public AudioClip CoinSound;
    
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        breakTimer = 3f;
        tutorial = false;
        HasMoved = false;
        direction = -1;
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
    /// 
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
    /// 
    /// </summary>
    private void CheckShift()
    {
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && breakTimer > 0f && HasMoved)
        {
            breaks = true;
            breakTimer -= Time.deltaTime;
            BreakSlider.GetComponent<Slider>().value = breakTimer;
        }
        else
        {
            breaks = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CheckMovement()
    {
        float localSpeed;
        if ((breaks))//timer comes later
        {
            localSpeed = speed / 3f + 2f;
        }
        else
        {
            localSpeed = speed;
        }
        if (HasMoved && !won)
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
        else
        {
            rb2d.velocity = new Vector3(0, 0, 0);
        }
    }

    /// <summary>
    /// 
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
            else if((Input.GetKeyDown(KeyCode.Escape)))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Selection Scene");
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CreateLine()
    {
        LastLineEnd = transform.position;
        GameObject G = Instantiate(Line, transform.position, Quaternion.identity);
        NextCollide = G.GetComponent<Collider2D>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="co"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    private void FitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        co.transform.position = a + (b - a) * .5f;
        float distance = Vector2.Distance(a, b);

        if(a.x != b.x)
        {
            co.transform.localScale = new Vector2(distance + .5f,.5f);
        }
        else
        {
            co.transform.localScale = new Vector2(.5f, distance + .5f);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag.Equals("Line") && collision != NextCollide))
        {
            Invoke("DelayEnd", .75f);
            won = true;
            AudioSource.PlayClipAtPoint(LossSound, Camera.main.transform.position);
        }
        if (collision.gameObject.tag.Equals("Coin"))
        {
            Destroy(collision.gameObject);
            collision.gameObject.GetComponent<CoinBehaviour>().DeactivateCoin();
            AudioSource.PlayClipAtPoint(CoinSound, Camera.main.transform.position);
            PlayerPrefs.SetInt("wallet", PlayerPrefs.GetInt("wallet") + 1);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Wall"))
        {
            Invoke("DelayEnd", .75f);
            won = true;
            AudioSource.PlayClipAtPoint(LossSound, Camera.main.transform.position);

        }
        if (collision.gameObject.tag.Equals("Goal") && collision.gameObject.GetComponent<SpriteRenderer>().color == Color.blue)
        {
            GameCon.ActivateWin();
            PlayerPrefs.SetInt(CurrentScene, 1);
            won = true;
            AudioSource.PlayClipAtPoint(WinSound, Camera.main.transform.position);
        }
        else
        {
            Invoke("DelayEnd", .75f);
            won = true;
            AudioSource.PlayClipAtPoint(LossSound, Camera.main.transform.position);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void DelayEnd()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene);
    }

}
