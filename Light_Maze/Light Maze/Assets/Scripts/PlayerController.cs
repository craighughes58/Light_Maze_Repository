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
    private Collider2D NextCollide;
    private Vector2 LastLineEnd;
    // Start is called before the first frame update
    void Start()
    {
        HasMoved = false;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDirection();
        if(HasMoved)
        {
            FitColliderBetween(NextCollide, LastLineEnd, transform.position);
        }
    }

    private void FixedUpdate()
    {
        CheckMovement();
    }

    private void CheckMovement()
    {
        if (HasMoved)
        {
            switch (direction)
            {

                case 0://up
                    rb2d.velocity = new Vector3(0,speed,0);
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
    }

    private void CheckDirection()
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
        if(collision != NextCollide)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);//TEMPORARY
        }
    }
}
