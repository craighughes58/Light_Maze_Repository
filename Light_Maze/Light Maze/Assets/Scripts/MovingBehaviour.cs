/*****************************************************************************
// File Name :         MovingBehaviour.cs
// Author :            Craig D. Hughes
// Creation Date :     July 26, 2022
//
// Brief Description : This Script will move an item in a selected direction
//                     
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBehaviour : MonoBehaviour
{
    [Tooltip("This bool is true if it's moving vertically and false if it's horizontally")]
    public bool MoveUp;
    [Tooltip("how far up the object can move on the x axis")]
    public float xMax;
    [Tooltip("how far up the object can move on the y axis")]
    public float yMax;
    [Tooltip("How fast the object will move")]
    public float speed;
    //the reference to the objects rigidbody
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    /// <summary>
    /// during the fixed update if the object is moving up, move the object's position based on speed then if it's reached the max y value, reset it to the starting position
    /// during the fixed update if the object isn't moving up, move the object's position based on speed then if it's reached the max x value, reset it to the starting position
    /// </summary>
    private void FixedUpdate()
    {
        if (MoveUp)//vertical
        {
            rb2d.velocity = new Vector3(0,speed,0);
            if(transform.position.y > yMax)//reset position
            {
                transform.position = new Vector3(transform.position.x,-yMax, 0);
            }
        }
        else//horizontal
        {
            rb2d.velocity = new Vector3(speed, 0, 0);
            if (transform.position.x > xMax)//reset position
            {
                transform.position = new Vector3(-xMax, transform.position.y, 0);
            }
        }
        
    }
}

