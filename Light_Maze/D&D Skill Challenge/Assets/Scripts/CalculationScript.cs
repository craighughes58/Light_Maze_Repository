/*****************************************************************************
// File Name :         CalculationScript.cs
// Author :            Craig Hughes
// Creation Date :     June 22, 2022
//
// Brief Description : 
*****************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculationScript : MonoBehaviour

{
    //
    private InputField Successes;
    //
    private InputField Failures;
    //
    private InputField ChallengeRating;
    //
    private InputField RollInput;
    //
    private Text PercentageText;


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        Successes = GameObject.Find("Success Amt").GetComponent<InputField>();
        Failures = GameObject.Find("Fail Amt").GetComponent<InputField>();
        ChallengeRating = GameObject.Find("Challenge Rating").GetComponent<InputField>();
        RollInput = GameObject.Find("Proficiency Input").GetComponent<InputField>();
        PercentageText = GameObject.Find("Percentage").GetComponent<Text>();
        //print(CalculateProbability(.05f, 3));
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            CalculateProbability();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void CalculateProbability()
    {
        print(Successes.text);
        //before starting the calculation, make sure each input was a number
        if (CheckInputs(Successes.text) && CheckInputs(Failures.text) && CheckInputs(ChallengeRating.text) && CheckInputs(RollInput.text))
        {
            PercentageText.text = SimulateProbability(1000).ToString();
        }
        else
        {
            PercentageText.text = "ERROR";
        }
    }

    private float SimulateProbability(int loops)
    {
        List<bool> SimulationCollection = new List<bool>();
        for(int i = 0; i < 1000; i++)
        {
          SimulationCollection.Add(RunSimulation());
        }
        //find average
        return 0.0f;
    }

    /// <summary>
    /// This method declares variables based on the input
    /// after declaring variables, this will roll dice and add modifiers
    /// until the player reaches a cerrtain number of win or loss threshold
    /// </summary>
    /// <returns>true = the player succeeded false = the player lost</returns>
    private bool RunSimulation()
    {
        float maxFailures = int.Parse(Failures.text);
        float totalFailures = 0;
        float successesNeeded = int.Parse(Successes.text);
        float numSuccesses = 0;
        float modifier = int.Parse(RollInput.text);//WILL EVENTUALLY BE CHANGED ONCE MORE PLAYERS ARE ADDED
        float challenge = int.Parse(ChallengeRating.text);
        //calculate
        while(totalFailures < maxFailures && numSuccesses < successesNeeded)
        {
            if ((float)RollTwenty() + modifier >= challenge)//D20 + player Modifier compared to challenge rating
            {
                ++numSuccesses;
            }
            else
            {
                ++totalFailures;
            }
        }
        if(totalFailures >= maxFailures) //if the player lost
        {
            return false;
        }
        else//if the player won
        {
            return true;
        }
    }

    private void RemoveSpaces()
    {

    }

    private int RollTwenty()
    {
        return Random.Range(1, 21);
    }

    /// <summary>
    /// This class makes sure each field holds a number then returns if that 
    /// statement is true or not
    /// </summary>
    /// <param name="Input">the string of the input given by the player</param>
    /// <returns>if the input is a number or not</returns>
    private bool CheckInputs(string Input)
    {
        //the number that is parsed
        int output;
        if(int.TryParse(Input,out output))
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// This calculates the probability of rolling certain numbers in a specific order
    /// </summary>
    /// <param name="probability">the percentage of rolling a specific number(s)</param>
    /// <param name="times">how many times you multiply the probability</param>
    /// <returns></returns>
    private float CalculateProbability(float probability, int times)
    {
        if(times > 1)
        {
            return probability * CalculateProbability(probability, times - 1);
        }
        else
        {
            return probability;
        }
    }
}
