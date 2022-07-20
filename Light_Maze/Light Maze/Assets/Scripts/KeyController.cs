using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private GameObject Goal;
    // Start is called before the first frame update
    void Start()
    {
        Goal = GameObject.Find("Goal");
        Goal.GetComponent<SpriteRenderer>().color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.magenta, Color.cyan, Mathf.PingPong(Time.time, 1f));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
            Goal.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
}
