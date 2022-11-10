/*****************************************************************************
// File Name :         KeyController.cs
// Author :            Craig D. Hughes
// Creation Date :     July 14, 2022
//
// Brief Description : This script controls if the door that ends the game
//                     is open or not. This prevents the player from going
//                     directly yto the end.
//
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    //the reference to the object that ends the game once the player collides with it
    private GameObject Goal;
    // Start is called before the first frame update
    void Start()
    {
        //find the goal in the hierarchy
        Goal = GameObject.Find("Goal");
        //change the color of the goal to white
        Goal.GetComponent<SpriteRenderer>().color = Color.white;
    }


    /// <summary>
    /// during the fixed update change the color between two colors over time
    /// </summary>
    private void FixedUpdate()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.magenta, Color.cyan, Mathf.PingPong(Time.time, 1f));
    }

    /// <summary>
    /// when the object gets triggered from a player, it will unlock the end door and destroy itself
    /// </summary>
    /// <param name="collision">the object that this one is colliding with</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            //destroy itself
            Destroy(gameObject);
            //open the end goal
            Goal.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
}
