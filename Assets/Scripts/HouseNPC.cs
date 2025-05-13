using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseNPC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < 127 || transform.position.z > 160)
        {
            transform.Rotate(0, 180, 0);
        }

        transform.Translate(0, 0, 5 * Time.deltaTime);
    }
}
