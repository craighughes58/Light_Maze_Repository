/*****************************************************************************
// File Name :         ShopController.cs
// Author :            Craig D. Hughes
// Creation Date :     July 10, 2022
//
// Brief Description : This script keeps track of the shop variables, methods
//                     and current status of the store, it also takes coins
//                     away from the user when they purchase an item
//
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    //this is the reference to the button that allows you to see the current selected item up close
    private Button VendorButton;
    //This is the reference to the button that allows you to buy items
    private Button BuyButton;
    //this is a reference to the button that allows you to apply colors the players have purchased
    private Button ApplyButton;
    //need a current price text var
    private Text currentPriceText;
    //This text displays how many coins the player has
    private Text WalletText;
    //this string holds the current color that the player is
    private string CurrentColor;
    [Tooltip("The sound when a player buys a new item")]
    public AudioClip BuySound;
    [Tooltip("The sound when a player applies a purchased color")]
    public AudioClip ApplySound;
    
    /// <summary>
    /// set starting values for variables
    /// </summary>
    void Start()
    {
        //gameobject
        currentPriceText = GameObject.Find("TextAMT").GetComponent<Text>();
        WalletText = GameObject.Find("Wallet").GetComponent<Text>();
        VendorButton = GameObject.Find("SelectedColor").GetComponent<Button>();
        BuyButton = GameObject.Find("Buy").GetComponent<Button>();
        ApplyButton = GameObject.Find("Apply").GetComponent<Button>();
        //set starting color
        SetColor("Red");
        CurrentColor = "Red";
        PlayerPrefs.SetInt("HasRed", 1);
        CheckPurchasedColor("HasRed");
        //set wallet text
        WalletText.text = "WALLET: " + PlayerPrefs.GetInt("wallet");


    }

    /// <summary>
    /// this method takes the color currently being viewed by the player
    /// and sets the applied color to it
    /// </summary>
    /// <param name="name"></param>
    public void SetColor(string name)
    {
        //sets the name to the current color
        CurrentColor = name;
        switch (name)//set variables to color and update texts and buttons
        {
            case "Red":
                CheckPurchasedColor("HasRed");
                currentPriceText.text = "0";
                VendorButton.GetComponent<Image>().color = Color.red;
                break;
            case "Blue":
                CheckPurchasedColor("HasBlue");
                currentPriceText.text = "1";
                VendorButton.GetComponent<Image>().color = Color.blue;
                break;
            case "Green":
                CheckPurchasedColor("HasGreen");
                currentPriceText.text = "5";
                VendorButton.GetComponent<Image>().color = Color.green;
                break;
            case "Purple":
                CheckPurchasedColor("HasPurple");
                currentPriceText.text = "5";
                VendorButton.GetComponent<Image>().color = new Color32(154,0,255,255);
                break;
             case "Orange":
                CheckPurchasedColor("HasOrange");
                currentPriceText.text = "6";
                VendorButton.GetComponent<Image>().color = new Color32(255,160,0,255);
                break;
            case "Yellow":
                CheckPurchasedColor("HasYellow");
                currentPriceText.text = "7";
                VendorButton.GetComponent<Image>().color = Color.yellow;
                break;
            case "Cyan":
                CheckPurchasedColor("HasCyan");
                currentPriceText.text = "8";
                VendorButton.GetComponent<Image>().color = new Color(0, 255, 250, 255);
                break;
            case "Magenta":
                CheckPurchasedColor("HasMagenta");
                currentPriceText.text = "8";
                VendorButton.GetComponent<Image>().color = Color.magenta;
                break;
            case "Pink":
                CheckPurchasedColor("HasPink");
                currentPriceText.text = "10";
                VendorButton.GetComponent<Image>().color = new Color32(255, 100, 166, 255);
                break;
        }
    }


    /// <summary>
    /// called when the player clicks the apply button on a purchased color
    /// if they have bought the color then their color is set to the current color
    /// </summary>
    public void ApplyColor()
    {
        if (PlayerPrefs.GetInt("Has" + CurrentColor) == 1)//if the player bought the color
        {
            switch (CurrentColor)
            {
                case "Red":
                    PlayerPrefs.SetString("BikeColor", "Red");
                    break;
                case "Blue":
                    PlayerPrefs.SetString("BikeColor", "Blue");
                    break;
                case "Green":
                    PlayerPrefs.SetString("BikeColor", "Green");
                    break;
                case "Purple":
                    PlayerPrefs.SetString("BikeColor", "Purple");
                    break;
                case "Orange":
                    PlayerPrefs.SetString("BikeColor", "Orange");
                    break;
                case "Yellow":
                    PlayerPrefs.SetString("BikeColor", "Yellow");
                    break;
                case "Cyan":
                    PlayerPrefs.SetString("BikeColor", "Cyan");
                    break;
                case "Magenta":
                    PlayerPrefs.SetString("BikeColor", "Magenta");
                    break;
                case "Pink":
                    PlayerPrefs.SetString("BikeColor", "Pink");
                    break;
            }
            AudioSource.PlayClipAtPoint(ApplySound, Camera.main.transform.position);
        }
        //change current color in CheckPurchaseColor method
        CheckPurchasedColor("Has" + CurrentColor);
    }


    /// <summary>
    /// called from the buy color button
    /// if the player has enough money to buy the color then variables representing the
    /// color are set to their "purchased state"
    /// </summary>
    public void BuyColor()
    {
        switch (CurrentColor)
        {
            case "Blue":
                if(PlayerPrefs.GetInt("wallet") - 1 > -1 && PlayerPrefs.GetInt("HasBlue") == 0) //if they have enough money and they haven't bought the color before
                {
                    PlayerPrefs.SetInt("wallet", PlayerPrefs.GetInt("wallet") - 1);//remove coins from the wallet
                    PlayerPrefs.SetInt("HasBlue", 1);//set player prefs to purchased state (1)
                    CheckPurchasedColor("HasBlue");//update UI
                    AudioSource.PlayClipAtPoint(BuySound, Camera.main.transform.position);//play sound
                }
                break;
            case "Green":
                if (PlayerPrefs.GetInt("wallet") - 5 > -1 && PlayerPrefs.GetInt("HasGreen") == 0)
                {
                    PlayerPrefs.SetInt("wallet", PlayerPrefs.GetInt("wallet") - 5);
                    PlayerPrefs.SetInt("HasGreen", 1);
                    CheckPurchasedColor("HasGreen");
                    AudioSource.PlayClipAtPoint(BuySound, Camera.main.transform.position);
                }
                break;
            case "Purple":
                if (PlayerPrefs.GetInt("wallet") - 5 > -1 && PlayerPrefs.GetInt("HasPurple") == 0)
                {
                    PlayerPrefs.SetInt("wallet", PlayerPrefs.GetInt("wallet") - 5);
                    PlayerPrefs.SetInt("HasPurple", 1);
                    CheckPurchasedColor("HasPurple");
                    AudioSource.PlayClipAtPoint(BuySound, Camera.main.transform.position);
                }
                break;
            case "Orange":
                if (PlayerPrefs.GetInt("wallet") - 6 > -1 && PlayerPrefs.GetInt("HasOrange") == 0)
                {
                    PlayerPrefs.SetInt("wallet", PlayerPrefs.GetInt("wallet") - 6);
                    PlayerPrefs.SetInt("HasOrange", 1);
                    CheckPurchasedColor("HasOrange");
                    AudioSource.PlayClipAtPoint(BuySound, Camera.main.transform.position);
                }
                break;
            case "Yellow":
                if (PlayerPrefs.GetInt("wallet") - 7 > -1 && PlayerPrefs.GetInt("HasYellow") == 0)
                {
                    PlayerPrefs.SetInt("wallet", PlayerPrefs.GetInt("wallet") - 7);
                    PlayerPrefs.SetInt("HasYellow", 1);
                    CheckPurchasedColor("HasYellow");
                    AudioSource.PlayClipAtPoint(BuySound, Camera.main.transform.position);
                }
                break;
            case "Cyan":
                if (PlayerPrefs.GetInt("wallet") - 8 > -1 && PlayerPrefs.GetInt("HasCyan") == 0)
                {
                    PlayerPrefs.SetInt("wallet", PlayerPrefs.GetInt("wallet") - 8);
                    PlayerPrefs.SetInt("HasCyan", 1);
                    CheckPurchasedColor("HasCyan");
                    AudioSource.PlayClipAtPoint(BuySound, Camera.main.transform.position);
                }
                break;
            case "Magenta":
                if (PlayerPrefs.GetInt("wallet") - 8 > -1 && PlayerPrefs.GetInt("HasMagenta") == 0)
                {
                    PlayerPrefs.SetInt("wallet", PlayerPrefs.GetInt("wallet") - 8);
                    PlayerPrefs.SetInt("HasMagenta", 1);
                    CheckPurchasedColor("HasMagenta");
                    AudioSource.PlayClipAtPoint(BuySound, Camera.main.transform.position);
                }
                break;
            case "Pink":
                if (PlayerPrefs.GetInt("wallet") - 10 > -1 && PlayerPrefs.GetInt("HasPink") == 0)
                {
                    PlayerPrefs.SetInt("wallet", PlayerPrefs.GetInt("wallet") - 10);
                    PlayerPrefs.SetInt("HasPink", 1);
                    CheckPurchasedColor("HasPink");
                    AudioSource.PlayClipAtPoint(BuySound, Camera.main.transform.position);
                }
                break;
        }
        WalletText.text = "WALLET: " + PlayerPrefs.GetInt("wallet");//update wallet text to new value after purchase
    }

    /// <summary>
    /// This script checks if a player has bought a color if they have then it sets the apply color to true
    /// otherwise it sets the buy color button to true
    /// </summary>
    /// <param name="PrefName">the color the method is checking</param>
    private void CheckPurchasedColor(string PrefName)
    {
        //is the color has been bought
        if (PlayerPrefs.GetInt(PrefName) == 1 && !PrefName.Substring(3).Equals(PlayerPrefs.GetString("BikeColor")))
        {
            ApplyButton.interactable = true;
            BuyButton.interactable = false;
        }
        else if(PrefName.Substring(3).Equals(PlayerPrefs.GetString("BikeColor")))//is the color currently being used
        {
            ApplyButton.interactable = false;
            BuyButton.interactable = false;
        }
        else//does the color need to be bought
        {
            ApplyButton.interactable = false;
            BuyButton.interactable = true;
        }
       
    }






}
