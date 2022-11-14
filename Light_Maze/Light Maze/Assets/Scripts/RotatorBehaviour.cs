/*****************************************************************************
// File Name :         RotatorBehaviour.cs
// Author :            Craig D. Hughes
// Creation Date :     May 19, 2022
//
// Brief Description : This script is attached to an item to make it rotate
//                     constatnly at a fixed speed
//
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorBehaviour : MonoBehaviour
{
    [Tooltip("How fast the object rotates")]
    public float speed;

    //Every fixed update the transform is rotated based on speed
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, transform.rotation.y + speed));
    }
}
