using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCroc : MonoBehaviour
{
    public float Speed;
    public float TurnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject oppCar = GameObject.Find("Opponent Car");
        Vector3 crocCarDistanceNorm = (oppCar.transform.position - transform.position).normalized;
        float angleToCar = Vector3.SignedAngle(transform.forward, crocCarDistanceNorm, Vector3.up);

        if (angleToCar < 0)
        {
            transform.Rotate(0, -TurnSpeed * Time.deltaTime, 0);
        }
        else if (angleToCar > 0)
        {
            transform.Rotate(0, TurnSpeed * Time.deltaTime, 0);
        }

        transform.Translate(0, 0, Speed * Time.deltaTime);
    }
}
