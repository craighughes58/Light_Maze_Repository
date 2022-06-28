using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameController : MonoBehaviour
{
    public string NextSceneName;
    public Text WinText;
    private Color lerpedColor = Color.white;


    // Start is called before the first frame update
    void Start()
    {
        WinText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //tickdown
        lerpedColor = Color.Lerp(Color.blue, Color.magenta, Mathf.PingPong(Time.time, .5f));
        WinText.color = lerpedColor;
    }

    public void ActivateWin()
    {
        WinText.enabled = true;
        Invoke("ChangeToNextScene",1f);

    }

    public void ChangeToNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneName);
    }
}
