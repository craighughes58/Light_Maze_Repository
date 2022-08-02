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
    private static bool started = false;
    public string CurrentScene;
    private Text p1ScoreText;
    private Text p2ScoreText;
    public AudioClip Win;
    public AudioClip MegaWin;
    // Start is called before the first frame update
    void Start()
    {
        p1ScoreText = GameObject.Find("P1Score").GetComponent<Text>();
        p2ScoreText = GameObject.Find("P2Score").GetComponent<Text>();
        if (!started)
        {
            PlayerPrefs.SetInt("P1Wins", 0);
            PlayerPrefs.SetInt("P2Wins", 0);
        }

        started = true;
        Players.Add(GameObject.Find("PlayerVS 1"));
        Players.Add(GameObject.Find("PlayerVS 2"));
        Timer = TimerStartTime;
        WinText.enabled = false;
        p1ScoreText.text = PlayerPrefs.GetInt("P1Wins").ToString();
        p2ScoreText.text = PlayerPrefs.GetInt("P2Wins").ToString();

    }

    // Update is called once per frame
    void Update()
    {
        TickDownStart();
        CheckExit();
    }

    private void CheckExit()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("P1Wins", 0);
            PlayerPrefs.SetInt("P2Wins",0);
            UnityEngine.SceneManagement.SceneManager.LoadScene("VS Select");
        }
    }
    private void TickDownStart()
    {
        if(!begin)
        {
            Timer -= Time.deltaTime;
            TimerText.text = (int)Timer + "";
            if (Timer <= 1f)
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
        /*        if(Winner == 1)
                {
                    WinText.color = Color.red;
                }
                else
                {
                    WinText.color = Color.blue;
                }*/
        WinText.color = Color.magenta;
        WinText.text = "Player " + Winner + " wins!";
        foreach (GameObject G in Players)
        {
            G.GetComponent<VersusPlayer>().HasWonActivate();
        }

        if(Winner == 1)
        {
            PlayerPrefs.SetInt("P1Wins", PlayerPrefs.GetInt("P1Wins") + 1);
            AudioSource.PlayClipAtPoint(Win, Camera.main.transform.position);
        }
        else
        {
            PlayerPrefs.SetInt("P2Wins", PlayerPrefs.GetInt("P2Wins") + 1);
            AudioSource.PlayClipAtPoint(Win, Camera.main.transform.position);
        }
        if (PlayerPrefs.GetInt("P2Wins") >= 3 || PlayerPrefs.GetInt("P1Wins") >= 3)
        {
            if (PlayerPrefs.GetInt("P1Wins") >= 3 && !(PlayerPrefs.GetInt("P2Wins") >= 3))
            {
                WinText.text = "PLAYER 1 IS THE WINNER!";
                AudioSource.PlayClipAtPoint(MegaWin, Camera.main.transform.position);
            }
            else if (PlayerPrefs.GetInt("P2Wins") >= 3 && !(PlayerPrefs.GetInt("P1Wins") >= 3))
            {
                WinText.text = "PLAYER 2 IS THE WINNER!";
                AudioSource.PlayClipAtPoint(MegaWin, Camera.main.transform.position);
            }
            else
            {
                WinText.text = "TIE";
            }
            Invoke("EndGame", 2f);
        }
        else
        {
            Invoke("ResetGame", 2f);
        }
        p1ScoreText.text = PlayerPrefs.GetInt("P1Wins").ToString();
        p2ScoreText.text = PlayerPrefs.GetInt("P2Wins").ToString();
    }

    private void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene);
    }
    private void EndGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("VS Select");
    }

}
