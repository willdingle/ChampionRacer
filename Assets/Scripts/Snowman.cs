using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    public float TurnSpeed;
    public float Speed;
    public char mode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case 'S':
                transform.Rotate(0, TurnSpeed * Time.deltaTime, 0);
                break;
            case 'M':
                transform.Rotate(0, TurnSpeed * Time.deltaTime, 0);
                transform.Translate(0, 0, Speed * Time.deltaTime);
                break;
        }
    }
}
