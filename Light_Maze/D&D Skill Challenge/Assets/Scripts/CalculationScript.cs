/*****************************************************************************
// File Name :         CalculationScript.cs
// Author :            Craig Hughes
// Creation Date :     June 22, 2022
//
// Brief Description : This script runs a simulation of a skill challenge 
                       then accumulates all wins and losses into a percentage
*****************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculationScript : MonoBehaviour

{
    //The input field where the player puts the successes needed to win
    private InputField Successes;
    //The input field where the player puts the failures needed to lose
    private InputField Failures;
    //The input field where the player puts the challenge rating
    private InputField ChallengeRating;
    //The text field that shows the player the percentage of victory
    private Text PercentageText;
    //The total number of players
    private int players;
    //The text field the shows the user the number of players
    private Text PlayerText;
    //the list of input fields for the players
    private List<GameObject> ModInput = new List<GameObject>();
    //the list of text fields for the player
    private List<GameObject> InputText = new List<GameObject>();

    /// <summary>
    /// At the start, All variables are declared and instantiated
    /// Then the interface is updated to starting state
    /// </summary>
    void Start()
    {
        //set up variables
        Successes = GameObject.Find("Success Amt").GetComponent<InputField>();
        Failures = GameObject.Find("Fail Amt").GetComponent<InputField>();
        ChallengeRating = GameObject.Find("Challenge Rating").GetComponent<InputField>();
        for(int i = 1; i <= 10; i++)
        {
            ModInput.Add(GameObject.Find("Proficiency Input " + i));
            InputText.Add(GameObject.Find("Proficiency Txt " + i));
        }
        PercentageText = GameObject.Find("Percentage").GetComponent<Text>();
        PlayerText = GameObject.Find("Total Player Txt").GetComponent<Text>();
        players = 1;
        //start interface
        UpdateInterface();
    }

    /// <summary>
    /// If the player pressed enter start the simulation
    /// if the player pressed escape, quit
    /// </summary>
    private void Update()
    {
        //run probability
        if(Input.GetKeyDown(KeyCode.Return))
        {
            CalculateProbability();
        }
        //quit
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitTool();
        }
    }

    /// <summary>
    /// called from a button in the interface or the escape
    /// key
    /// This will quit out of the application
    /// </summary>
    public void QuitTool()
    {
        Application.Quit();
    }

    /// <summary>
    /// This will check if all of the inputs are valid, if they
    /// the simulation will start and then show the percentage
    /// otherwise it will show the user an error
    /// </summary>
    public void CalculateProbability()
    {
        //Format
        RemoveSpaces();
        //before starting the calculation, make sure each input was a number
        if (CheckInputs(Successes.text) && CheckInputs(Failures.text) && CheckInputs(ChallengeRating.text) && CheckAllActivePlayerInputs())
        {
            PercentageText.text = ((int)(SimulateProbability()*100)).ToString() + "%";
        }
        else//SHOW AN ERROR
        {
            PercentageText.text = "ERROR: INPUT FIELDS HOLD A VALUE THAT ISN'T A POSITIVE NUMBER";
        }
    }



    /// <summary>
    /// This will run simulations in order then establish the percentage
    /// of a certain set of parameters succeding 
    /// </summary>
    /// <param name="loops"></param>
    /// <returns>The fraction of successes over total runs</returns>
    private float SimulateProbability()
    {
        //declare variables and containers
        List<bool> SimulationCollection = new List<bool>();
        float wins = 0f;
        //run 1 million simulatiions
        for(int i = 0; i < 1000000; i++) //1000000
        {
          SimulationCollection.Add(RunSimulation());
            if(SimulationCollection[i] == true)
            {
                ++wins;
            }
        }
        //find average
        return wins / SimulationCollection.Count;
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
        float totalFailures;
        float successesNeeded = int.Parse(Successes.text);
        float numSuccesses;
        float modifier;
        numSuccesses = 0;
        totalFailures = 0;
        float challenge = int.Parse(ChallengeRating.text);
        //calculate
        while (totalFailures < maxFailures && numSuccesses < successesNeeded)//while the player hasn't won or lost
        {
            for (int i = 0; i < players; i++)//go through each player
            {
                modifier = int.Parse(ModInput[i].GetComponent<InputField>().text);
                
                if ((float)RollTwenty() + modifier >= challenge)//D20 + player Modifier compared to challenge rating
                {
                    ++numSuccesses;
                }
                else
                {
                    ++totalFailures;
                }
                if (totalFailures >= maxFailures) //if the player lost
                {
                    return false;
                }
                if(numSuccesses >= successesNeeded)
                {
                    return true;
                }
            }
        }
        return true;
    }

    private void RemoveSpaces()
    {

    }

    /// <summary>
    /// return a random number between 1 and 20
    /// </summary>
    /// <returns>the value of the random number</returns>
    private int RollTwenty()
    {
        return Random.Range(1, 21);
    }

    /// <summary>
    /// This class makes sure each field holds a positive number then returns if that 
    /// statement is true or not
    /// </summary>
    /// <param name="Input">the string of the input given by the player</param>
    /// <returns>if the input is a number or not</returns>
    private bool CheckInputs(string Input)
    {
        //the number that is parsed
        int output;
        if(int.TryParse(Input,out output) && Mathf.Sign(int.Parse(Input)) == 1)
        {
            return true;
        }
        return false;
    }
    

    /// <summary>
    /// This method will go through all the active players and make sure all of
    /// them are active and only contain numbers
    /// </summary>
    /// <returns>true = active and numerical, false = inactive or not a number</returns>
    private bool CheckAllActivePlayerInputs()
    {
        for(int i = 0; i < players; i++)
        {
            if(!ModInput[i].activeInHierarchy || !CheckInputs(ModInput[i].GetComponent<InputField>().text))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// if the number of players is less than 10, add another
    /// player then update the interface
    /// </summary>
    public void AddPlayer()
    {
        if(players < 10)
        {
            ++players;
        }
        UpdateInterface();
    }

    /// <summary>
    /// if the number of players is greater than 1 then remove
    /// a player and update the interface
    /// </summary>
    public void RemovePlayer()
    {
        if(players > 1)
        {
            --players;
        }
        UpdateInterface();
    }

    /// <summary>
    /// This increases or decreases the number of player fields
    /// equal to the amount of players chosen
    /// </summary>
    private void UpdateInterface()
    {
        PlayerText.text = players.ToString();
        for(int i = 0; i < 10; i++)//reset players
        {
            ModInput[i].SetActive(false);
            InputText[i].SetActive(false);
        }
        for (int j= 0; j < players; j++)//activate players equal to the number selected
        {
            ModInput[j].SetActive(true);
            InputText[j].SetActive(true);
        }
    }
}
