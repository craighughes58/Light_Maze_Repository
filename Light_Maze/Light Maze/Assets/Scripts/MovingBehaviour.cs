using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBehaviour : MonoBehaviour
{
    public bool MoveUp;
    public float xMax;
    public float yMax;
    public float speed;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (MoveUp)
        {
            rb2d.velocity = new Vector3(0,speed,0);
            if(transform.position.y > yMax)
            {
                transform.position = new Vector3(transform.position.x,-yMax, 0);
            }
        }
        else
        {
            rb2d.velocity = new Vector3(speed, 0, 0);
            if (transform.position.x > xMax)
            {
                transform.position = new Vector3(-xMax, transform.position.y, 0);
            }
        }
        
    }
}

