/*****************************************************************************
// File Name :         ExpanderBehaviour.cs
// Author :            Craig D. Hughes
// Creation Date :     July 7, 2022
//
// Brief Description : This script slides the scale of an object between two
//                     points
//                  
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpanderBehaviour : MonoBehaviour
{
    [Tooltip("The absolute largest the object can get through ping pong")]
    public float MaxSize;

    /// <summary>
    /// Every update continue to oscilate between two points using ping pong
    /// </summary>
    void Update()
    {
        transform.localScale = new Vector3(Mathf.PingPong(Time.time, MaxSize), Mathf.PingPong(Time.time, MaxSize),0); //only the x  and y values are altered so that it equally expands both vertically and horizontally 
        //Mathf.PingPong(Time.time, .5f)
    }
}
