using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private Button VendorButton;
    private Button BuyButton;
    private Button ApplyButton;
    //need a current price text var
    private Text currentPriceText;
    private Text WalletText;
    private string CurrentColor;
    public AudioClip BuySound;
    public AudioClip ApplySound;
    // Start is called before the first frame update
    void Start()
    {
        currentPriceText = GameObject.Find("TextAMT").GetComponent<Text>();
        WalletText = GameObject.Find("Wallet").GetComponent<Text>();
        VendorButton = GameObject.Find("SelectedColor").GetComponent<Button>();
        BuyButton = GameObject.Find("Buy").GetComponent<Button>();
        ApplyButton = GameObject.Find("Apply").GetComponent<Button>();
        SetColor("Red");
        CurrentColor = "Red";
        PlayerPrefs.SetInt("HasRed", 1);
        CheckPurchasedColor("HasRed");
        WalletText.text = "WALLET: " + PlayerPrefs.GetInt("wallet");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetColor(string name)
    {
        CurrentColor = name;
        switch (name)
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

    public void ApplyColor()
    {
        if (PlayerPrefs.GetInt("Has" + CurrentColor) == 1)
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
        CheckPurchasedColor("Has" + CurrentColor);
    }
    public void BuyColor()
    {
        switch (CurrentColor)
        {
            case "Blue":
                if(PlayerPrefs.GetInt("wallet") - 1 > -1 && PlayerPrefs.GetInt("HasBlue") == 0)
                {
                    PlayerPrefs.SetInt("wallet", PlayerPrefs.GetInt("wallet") - 1);
                    PlayerPrefs.SetInt("HasBlue", 1);
                    CheckPurchasedColor("HasBlue");
                    AudioSource.PlayClipAtPoint(BuySound, Camera.main.transform.position);
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
        WalletText.text = "WALLET: " + PlayerPrefs.GetInt("wallet");
    }

    private void CheckPurchasedColor(string PrefName)
    {
        if (PlayerPrefs.GetInt(PrefName) == 1 && !PrefName.Substring(3).Equals(PlayerPrefs.GetString("BikeColor")))
        {
            ApplyButton.interactable = true;
            BuyButton.interactable = false;
        }
        else if(PrefName.Substring(3).Equals(PlayerPrefs.GetString("BikeColor")))
        {
            ApplyButton.interactable = false;
            BuyButton.interactable = false;
        }
        else
        {
            ApplyButton.interactable = false;
            BuyButton.interactable = true;
        }
       
    }






}
