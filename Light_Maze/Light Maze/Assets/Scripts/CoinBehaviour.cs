/*****************************************************************************
// File Name :         CoinBehaviour.cs
// Author :            Craig D. Hughes
// Creation Date :     July 7, 2022
//
// Brief Description : This script controls the currency that appears through
//                     the levels. They add currency to the playerprefs that
//                     can then be used at the shop
//
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [Tooltip("The unique number that identifies this coin in the player prefs")]
    public int coinID;
    [Tooltip("The number that this coin appears on")]
    public int levelNum;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("level" + levelNum + coinID) == 1) //if the player has already collected this coin
        {
            Destroy(gameObject);//destroy it
        }
    }

    /// <summary>
    /// called by the player, this sets the coin's int to 1 in player prefs so it can't be recollected
    /// </summary>
    public void DeactivateCoin()
    {
        PlayerPrefs.SetInt("level" + levelNum + coinID, 1);
    }
}
