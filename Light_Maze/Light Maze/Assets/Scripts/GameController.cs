using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameController : MonoBehaviour
{
    public string NextSceneName;
    public Text WinText;


    // Start is called before the first frame update
    void Start()
    {
        WinText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //tickdown
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
