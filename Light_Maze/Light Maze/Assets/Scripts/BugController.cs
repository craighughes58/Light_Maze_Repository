using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }
    private void FixedUpdate()
    {
        if(Player.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, .1f);
        }
    }
}
