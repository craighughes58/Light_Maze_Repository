using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public int ButtonNum;

    // Start is called before the first frame update
    void Start()
    {
        SetButtonsActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetButtonsActive()
    {
        int last = 1;
        for (int i = 1; i <=ButtonNum; i++)
        {
            if(PlayerPrefs.GetInt("Level" + i) == 1)
            {
                GameObject.Find("LvlBtn" + i).GetComponent<Button>().interactable = true;
                last = i + 1;
            }
            else
            {
                GameObject.Find("LvlBtn" + i).GetComponent<Button>().interactable = false;
            }
        }
        if (last < 16)
        {
            GameObject.Find("LvlBtn" + (last)).GetComponent<Button>().interactable = true;
        }
    }
}
