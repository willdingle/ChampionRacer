using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpponentCar : MonoBehaviour
{
    public float Speed;
    public float MaxSpeed;
    public float TurnSpeed;
    public float Acceleration;
    public float BrakePower;
    //-1 for backwards, 0 for stationary, 1 for forwards
    public int carDirection;
    //-1 for left, 0 for not turning, 1 for right
    public int carRotation;

    public GameObject waypointsHolder;
    public List<GameObject> waypoints;

    public GameObject flWheel, frWheel, rlWheel, rrWheel;

    public int lap = 1;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform waypoint in waypointsHolder.transform)
        {
            waypoints.Add(waypoint.gameObject);
            //Debug.Log(waypoint.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Count > 0)
        {
            calcMovement();
        }
        else
        {
            lap += 1;
            if (lap > 1)
            {
                //Opponent wins
                SceneManager.LoadScene("LoseRace");
            }
            else
            {
                foreach (Transform waypoint in waypointsHolder.transform)
                {
                    waypoints.Add(waypoint.gameObject);
                    //Debug.Log(waypoint.name);
                }
            }
        }

        Move();
        Turn();
        Animate();
    }

    void calcMovement()
    {
        GameObject nextWaypoint;

        foreach (GameObject waypoint in waypoints)
        {
            //Work out dot product between car and waypoint
            Vector3 carWaypointDistanceNorm = (waypoint.transform.position - transform.position).normalized;
            float dotProduct = Vector3.Dot(transform.forward, carWaypointDistanceNorm);

            //If dot product < 0, go to next waypoint
            // If dot product > 0, store this waypoint
            if (dotProduct < 0)
            {
                waypoints.Remove(waypoint);
                continue;
            }
            else
            {
                nextWaypoint = waypoint;
                carDirection = 1;
                //Debug.Log(nextWaypoint.name);
                float angleToWaypoint = Vector3.SignedAngle(transform.forward, carWaypointDistanceNorm, Vector3.up);
                if (angleToWaypoint < 0)
                {
                    carRotation = -1;
                }
                else if (angleToWaypoint > 0)
                {
                    carRotation = 1;
                }
                else
                {
                    carRotation = 0;
                }
                break;
            }
        }
    }

    private void Move()
    {
        if (carDirection == 1)
        {
            if (Speed < MaxSpeed && Speed >= 0)
                Speed += Acceleration * Time.deltaTime;
            else if (Speed > -MaxSpeed && Speed < 0)
                Speed += BrakePower * Time.deltaTime;

        }
        else if (carDirection == -1)
        {
            if (Speed > -MaxSpeed && Speed <= 0)
                Speed -= Acceleration * Time.deltaTime;
            else if (Speed < MaxSpeed && Speed > 0)
                Speed -= BrakePower * Time.deltaTime;
        }
        else
        {
            if (Speed > -1 && Speed < 1)
                Speed = 0f;
            if (Speed > 0f)
                Speed -= Acceleration * Time.deltaTime;
            else if (Speed < 0f)
                Speed += Acceleration * Time.deltaTime;
        }

        transform.Translate(0, 0, Speed * Time.deltaTime);
    }

    private void Turn()
    {
        if (carRotation == -1)
        {
            transform.Rotate(0, -TurnSpeed * Time.deltaTime, 0);
        }
        else if (carRotation == 1)
        {
            transform.Rotate(0, TurnSpeed * Time.deltaTime, 0);
        }
    }

    private void Animate()
    {
        flWheel.transform.Rotate(Speed / 5, 0, 0);
        frWheel.transform.Rotate(Speed / 5, 0, 0);
        rlWheel.transform.Rotate(Speed / 5, 0, 0);
        rrWheel.transform.Rotate(Speed / 5, 0, 0);
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
