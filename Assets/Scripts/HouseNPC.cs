using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseNPC : MonoBehaviour
{
    public float turnPositionHigh;
    public float turnPositionLow;
    public string mode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case "X":
                if (transform.position.x < turnPositionLow
                    || transform.position.x > turnPositionHigh)
                {
                    transform.Rotate(0, 180, 0);
                }
                break;
            case "Y":
                break;
            case "Z":
                if (transform.position.z < turnPositionLow
                    || transform.position.z > turnPositionHigh)
                {
                    transform.Rotate(0, 180, 0);
                }
                
                break;
        }

        transform.Translate(0, 0, 5 * Time.deltaTime);




    }
}
