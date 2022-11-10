/*****************************************************************************
// File Name :         BugController.cs
// Author :            Craig D. Hughes
// Creation Date :     July 19, 2022
//
// Brief Description : Bugs are an enemy added into mazes that chase the
//                     the player down in a straight line.
//
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour
{
    //this is the reference to the player that the script will use to chase them
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }
    /// <summary>
    /// If the player has started moving then the enemy will chase them in a straight line
    /// </summary>
    private void FixedUpdate()
    {
        if(Player.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, .07f);
        }
    }
}
