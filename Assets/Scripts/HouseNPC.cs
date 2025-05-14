using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseNPC : MonoBehaviour
{
    public float turnPositionHigh;
    public float turnPositionLow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < turnPositionLow 
            || transform.position.z > turnPositionHigh)
        {
            transform.Rotate(0, 180, 0);
        }

        transform.Translate(0, 0, 5 * Time.deltaTime);
    }
}
