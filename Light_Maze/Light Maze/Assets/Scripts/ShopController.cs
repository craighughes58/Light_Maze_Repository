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
    private string CurrentColor;
    private int CurrentPrice;
    // Start is called before the first frame update
    void Start()
    {
        VendorButton = GameObject.Find("SelectedColor").GetComponent<Button>();
        BuyButton = GameObject.Find("Buy").GetComponent<Button>();
        ApplyButton = GameObject.Find("Apply").GetComponent<Button>();
        SetColor("Red");
        CurrentColor = "Red";
        CurrentPrice = 0;
        PlayerPrefs.SetInt("HasRed", 1);

        CheckPurchasedColor("HasRed");

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
                VendorButton.GetComponent<Image>().color = Color.red;
                break;
            case "Blue":
                CheckPurchasedColor("HasBlue");
                VendorButton.GetComponent<Image>().color = Color.blue;
                break;
            case "Green":
                CheckPurchasedColor("HasGreen");
                VendorButton.GetComponent<Image>().color = Color.green;
                break;
            case "Purple":
                CheckPurchasedColor("HasPurple");
                VendorButton.GetComponent<Image>().color = new Color32(154,0,255,255);
                break;
             case "Orange":
                CheckPurchasedColor("HasOrange");
                VendorButton.GetComponent<Image>().color = new Color32(255,160,0,255);
                break;
            case "Yellow":
                CheckPurchasedColor("HasYellow");
                VendorButton.GetComponent<Image>().color = Color.yellow;
                break;
            case "Cyan":
                CheckPurchasedColor("HasCyan");
                VendorButton.GetComponent<Image>().color = new Color(0, 255, 250, 255);
                break;
            case "Magenta":
                CheckPurchasedColor("HasMagenta");
                VendorButton.GetComponent<Image>().color = Color.magenta;
                break;
            case "Pink":
                CheckPurchasedColor("HasPink");
                VendorButton.GetComponent<Image>().color = new Color32(255, 100, 166, 255);
                break;
        }
    }

    public void ApplyColor()
    {
       
    }
    public void BuyColor()
    {

    }

    private void CheckPurchasedColor(string PrefName)
    {
        //"HasRed"
        if (PlayerPrefs.GetInt(PrefName) == 1)
        {
            ApplyButton.interactable = true;
            BuyButton.interactable = false;
        }
        else
        {
            ApplyButton.interactable = false;
            BuyButton.interactable = true;
        }
    }






}
