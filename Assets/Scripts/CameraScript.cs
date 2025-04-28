using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject playerCar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float[] plCarPos = new float[3]
        {
            playerCar.transform.eulerAngles.x,
            playerCar.transform.eulerAngles.y,
            playerCar.transform.eulerAngles.z
        };

        transform.eulerAngles = new Vector3(plCarPos[0] - plCarPos[0], plCarPos[1], plCarPos[2] - plCarPos[2]);
    }
}
