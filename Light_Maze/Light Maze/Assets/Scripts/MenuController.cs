using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetString("CurrentColor").Equals(""))
        {
            PlayerPrefs.SetString("CurrentColor", "Red");
            PlayerPrefs.SetInt("HasRed", 1);
        }
    }
    public void MoveScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
