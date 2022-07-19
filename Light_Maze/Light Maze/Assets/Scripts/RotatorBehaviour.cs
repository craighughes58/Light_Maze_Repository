using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorBehaviour : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, transform.rotation.y + speed));
    }
}
