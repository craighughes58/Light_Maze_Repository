using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpanderBehaviour : MonoBehaviour
{
    public float MaxSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(Mathf.PingPong(Time.time, MaxSize), Mathf.PingPong(Time.time, MaxSize),0);
        //Mathf.PingPong(Time.time, .5f)
    }
}
