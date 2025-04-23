using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCar : MonoBehaviour
{
    public float Speed;
    public float MaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(0, 0, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Croc") && obj.gameObject.activeSelf)
        {
            Speed = 0;
            obj.gameObject.SetActive(false);
        }
    }
}
