using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VsGameController : MonoBehaviour
{
    public Text TimerText;
    public Text WinText;
    public float TimerStartTime;
    private float Timer;
    private List<GameObject> Players= new List<GameObject>();
    private bool begin = false;
    // Start is called before the first frame update
    void Start()
    {
        Players.Add(GameObject.Find("PlayerVS 1"));
        Players.Add(GameObject.Find("PlayerVS 2"));
        Timer = TimerStartTime;
        WinText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        TickDownStart();
    }
    private void TickDownStart()
    {
        if(!begin)
        {
            Timer -= Time.deltaTime;
            TimerText.text = (int)Timer + "";
            if (Timer <= 0f)
            {
                foreach (GameObject G in Players)
                {
                    G.GetComponent<VersusPlayer>().StartMove();
                }
                begin = true;
                TimerText.enabled = false;
            }
        }
    }
    public void ActivateEnd(int Winner)
    {
        WinText.enabled = true;
        if(Winner == 1)
        {
            WinText.color = Color.red;
        }
        else
        {
            WinText.color = Color.blue;
        }
        WinText.text = "Player " + Winner + " wins!";
        foreach (GameObject G in Players)
        {
            G.GetComponent<VersusPlayer>().HasWonActivate();
        }

    }


}
