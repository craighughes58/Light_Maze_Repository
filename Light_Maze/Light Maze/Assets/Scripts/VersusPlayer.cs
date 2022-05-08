using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        HasMoved = false;
        GameCon = GameObject.Find("GameController").GetComponent<VsGameController>();
        rb2d = GetComponent<Rigidbody2D>();
        CreateLine();

    }

    // Update is called once per frame
    void Update()
    {
        CheckDirection();
        if (HasMoved)
        {
            FitColliderBetween(NextCollide, LastLineEnd, transform.position);
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
        if (HasMoved && !won)
        {
            switch (direction)
            {

                case 0://up
                    rb2d.velocity = new Vector3(0, speed, 0);
                    break;
                case 1://down
                    rb2d.velocity = new Vector3(0, -speed, 0);
                    break;
                case 2://left
                    rb2d.velocity = new Vector3(-speed, 0, 0);
                    break;
                case 3://right
                    rb2d.velocity = new Vector3(speed, 0, 0);
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
        if (!won)
        {
            if (((Input.GetKeyDown(KeyCode.W) && playerNum == 1) || (Input.GetKeyDown(KeyCode.UpArrow) && playerNum == 2)) && direction != 1)//Switch to up
            {
                direction = 0;
                HasMoved = true;
                CreateLine();
            }
            else if (((Input.GetKeyDown(KeyCode.A) && playerNum == 1) || (Input.GetKeyDown(KeyCode.LeftArrow) && playerNum == 2)) && direction != 3)//Switch to Left
            {
                direction = 2;
                HasMoved = true;
                CreateLine();
            }
            else if (((Input.GetKeyDown(KeyCode.S) && playerNum == 1) || (Input.GetKeyDown(KeyCode.DownArrow) && playerNum == 2)) && direction != 0)//Switch to down
            {
                direction = 1;
                HasMoved = true;
                CreateLine();
            }
            else if (((Input.GetKeyDown(KeyCode.D) && playerNum == 1) || (Input.GetKeyDown(KeyCode.RightArrow) && playerNum == 2)) && direction != 2)//Switch to right
            {
                direction = 3;
                HasMoved = true;
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
        if ((collision.gameObject.tag.Equals("Line") && collision != NextCollide))
        {
            EndGame();
            //UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            EndGame();
            //UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene);
        }
    }

    private void EndGame()
    {
        CreateLine();
        if(playerNum == 1)
        {
            GameCon.ActivateEnd(2);
        }
        else
        {
            GameCon.ActivateEnd(1);
        }
        Destroy(gameObject);
    }
    public void HasWonActivate()
    {
        won = true;
    }
}
