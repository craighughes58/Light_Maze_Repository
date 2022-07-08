using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public int coinID;
    public int levelNum;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("level" + levelNum + coinID) == 1)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeactivateCoin()
    {
        PlayerPrefs.SetInt("level" + levelNum + coinID, 1);
    }
}
