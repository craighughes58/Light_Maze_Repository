using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VersusPlayer : MonoBehaviour
{
    private bool HasMoved;
    //0 = up, 1 = down, 2 = left, 3 = right
    private int direction;
    public float speed;
    private Rigidbody2D rb2d;
    public GameObject Line;
    private VsGameController GameCon;
    private Collider2D NextCollide;
    private Vector2 LastLineEnd;
    private bool won = false;
    public string CurrentScene;
    public int playerNum;
    public Slider SpeedSlider;
    private float speedTime;
    private bool isSpeeding;
    private bool hasEnded;
    // Start is called before the first frame update
    void Start()
    {
        HasMoved = false;
        hasEnded = false;
        isSpeeding = false;
        GameCon = GameObject.Find("GameController").GetComponent<VsGameController>();
        rb2d = GetComponent<Rigidbody2D>();
        CreateLine();
        if(playerNum == 1)
        {
            Line.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            Line.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        speedTime = SpeedSlider.GetComponent<Slider>().maxValue;
        SpeedSlider.GetComponent<Slider>().value = speedTime;

    }

    // Update is called once per frame
    void Update()
    {
        CheckDirection();
        if (HasMoved)
        {
            FitColliderBetween(NextCollide, LastLineEnd, transform.position);
        }
        CheckSpeedBoost();
    }

    private void CheckSpeedBoost()
    {

        if(((playerNum == 1 && Input.GetKey(KeyCode.LeftShift) && speedTime > 0f) || (playerNum == 2 && Input.GetKey(KeyCode.RightShift) && speedTime > 0f)) && HasMoved && !won)
        {
            isSpeeding = true;
            speedTime -= Time.deltaTime;
            SpeedSlider.GetComponent<Slider>().value = speedTime;

        }
        else
        {
            isSpeeding = false; 
        }
    }
    public void StartMove()
    {
        HasMoved = true;
        if(playerNum == 1)
        {
            direction = 0;
        }
        else
        {
            direction = 1;
        }
    }
    private void FixedUpdate()
    {
        CheckMovement();
    }

    private void CheckMovement()
    {
        int modifier = 0;
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
        else
        {
            rb2d.velocity = new Vector3(0, 0, 0);
        }
    }

    private void CheckDirection()
    {
        if (!won && HasMoved)
        {
            if (((Input.GetKeyDown(KeyCode.W) && playerNum == 1) || (Input.GetKeyDown(KeyCode.UpArrow) && playerNum == 2)) && direction != 1)//Switch to up
            {
                direction = 0;
                //HasMoved = true;
                CreateLine();
            }
            else if (((Input.GetKeyDown(KeyCode.A) && playerNum == 1) || (Input.GetKeyDown(KeyCode.LeftArrow) && playerNum == 2)) && direction != 3)//Switch to Left
            {
                direction = 2;
                //HasMoved = true;
                CreateLine();
            }
            else if (((Input.GetKeyDown(KeyCode.S) && playerNum == 1) || (Input.GetKeyDown(KeyCode.DownArrow) && playerNum == 2)) && direction != 0)//Switch to down
            {
                direction = 1;
                //HasMoved = true;
                CreateLine();
            }
            else if (((Input.GetKeyDown(KeyCode.D) && playerNum == 1) || (Input.GetKeyDown(KeyCode.RightArrow) && playerNum == 2)) && direction != 2)//Switch to right
            {
                direction = 3;
                //HasMoved = true;
                CreateLine();
            }
        }
    }

    private void CreateLine()
    {
        LastLineEnd = transform.position;
        GameObject G = Instantiate(Line, transform.position, Quaternion.identity);
        NextCollide = G.GetComponent<Collider2D>();
    }

    private void FitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        co.transform.position = a + (b - a) * .5f;
        float distance = Vector2.Distance(a, b);

        if (a.x != b.x)
        {
            co.transform.localScale = new Vector2(distance + .5f, .5f);
        }
        else
        {
            co.transform.localScale = new Vector2(.5f, distance + .5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag.Equals("Line") && collision != NextCollide) && !hasEnded)
        {
            EndGame();
            //UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall") && !hasEnded)
        {
            EndGame();
            //UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene);
        }
    }

    private void EndGame()
    {
        hasEnded = true;
        CreateLine();
        if(playerNum == 1)
        {
            GameCon.ActivateEnd(2);
        }
        else
        {
            GameCon.ActivateEnd(1);
        }
    }
    public void HasWonActivate()
    {
        won = true;
    }

}
