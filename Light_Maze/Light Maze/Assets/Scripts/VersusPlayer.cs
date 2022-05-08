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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartMove()
    {

    }
}
