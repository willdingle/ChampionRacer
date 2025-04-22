using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCroc : MonoBehaviour
{
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Car"))
        {
            obj.gameObject.GetComponent<CarMovement>().Speed = 0;
        }
    }
}
