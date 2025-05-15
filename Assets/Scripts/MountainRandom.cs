using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainRandom : MonoBehaviour
{
     // Start is called before the first frame update
    void Start()
    {
        foreach (Transform mountain in transform)
        {
            mountain.localScale = new Vector3(mountain.localScale.x, Random.Range(4, 10), mountain.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
