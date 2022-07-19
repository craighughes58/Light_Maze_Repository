using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool HasMoved;
    //0 = up, 1 = down, 2 = left, 3 = right
    private int direction;
    public float speed;
    private Rigidbody2D rb2d;
    public GameObject Line;
    private GameController GameCon;
    private Collider2D NextCollide;
    private Vector2 LastLineEnd;
    private bool won = false;
    public string CurrentScene;
    public float speedControl;
    private bool breaks;
    // Start is called before the first frame update
    void Start()
    {
        HasMoved = false;
        direction = -1;
        GameCon = GameObject.Find("GameController").GetComponent<GameController>();
        rb2d = GetComponent<Rigidbody2D>();


       switch (PlayerPrefs.GetString("BikeColor"))
        {
            /*
             *                 VendorButton.GetComponent<Image>().color = Color.red;
                VendorButton.GetComponent<Image>().color = Color.blue;
                VendorButton.GetComponent<Image>().color = Color.green;
                VendorButton.GetComponent<Image>().color = new Color32(154,0,255,255);//Purple
                VendorButton.GetComponent<Image>().color = new Color32(255,160,0,255);//Orange
                VendorButton.GetComponent<Image>().color = Color.yellow;
                VendorButton.GetComponent<Image>().color = new Color(0, 255, 250, 255);//cyan
                VendorButton.GetComponent<Image>().color = Color.magenta;
                VendorButton.GetComponent<Image>().color = new Color32(255, 100, 166, 255);//pink

             */
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

    // Update is called once per frame
    void Update()
    {
        CheckDirection();
        if(HasMoved)
        {
            FitColliderBetween(NextCollide, LastLineEnd, transform.position);
        }
        CheckShift();
        
    }

    private void FixedUpdate()
    {
        CheckMovement();
    }

    private void CheckShift()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            breaks = true;
        }
        else
        {
            breaks = false;
        }
    }

    private void CheckMovement()
    {
        float localSpeed;
        if ((breaks))//timer comes later
        {
            localSpeed = speed / 3f;
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

    private void CheckDirection()
    {
        if (!won)
        {
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && direction != 1)//Switch to up
            {
                direction = 0;
                HasMoved = true;
                CreateLine();
            }
            else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && direction != 3)//Switch to Left
            {
                direction = 2;
                HasMoved = true;
                CreateLine();
            }
            else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && direction != 0)//Switch to down
            {
                direction = 1;
                HasMoved = true;
                CreateLine();
            }
            else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && direction != 2)//Switch to right
            {
                direction = 3;
                HasMoved = true;
                CreateLine();
            }
            else if((Input.GetKeyDown(KeyCode.Escape)))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Selection Scene");
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

        if(a.x != b.x)
        {
            co.transform.localScale = new Vector2(distance + .5f,.5f);
        }
        else
        {
            co.transform.localScale = new Vector2(.5f, distance + .5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag.Equals("Line") && collision != NextCollide))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene);
        }
        if(collision.gameObject.tag.Equals("Coin"))
        {
            Destroy(collision.gameObject);
            collision.gameObject.GetComponent<CoinBehaviour>().DeactivateCoin();
            PlayerPrefs.SetInt("wallet", PlayerPrefs.GetInt("wallet") + 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Wall"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene);
        }
        if (collision.gameObject.tag.Equals("Goal"))
        {
            GameCon.ActivateWin();
            PlayerPrefs.SetInt(CurrentScene, 1);
            won = true;
        }
    }
}
