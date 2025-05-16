using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCar : MonoBehaviour
{
    private enum Items
    {
        NONE,
        BRONZE_ROCKET,
        SILVER_ROCKET,
        GOLD_ROCKET,
        GREEN_CROCODILE,
        RED_CROCODILE,
        PISTOL_GUN,
        MACHINE_GUN
    }

    public int PlayerNumber = 1;
    public float Speed;
    public TMP_Text speedText;
    public float MaxSpeed;
    public float TurnSpeed;
    public float Acceleration;
    public float BrakePower;
    public float BoostAmount;

    public GameObject flWheel, frWheel, rlWheel, rrWheel;

    private int coinCount = 0;
    public TMP_Text coinCountText;

    private Items itemHeld = Items.NONE;
    private int silverRocketCount = 0;
    private float goldRocketTime = 0f;
    private bool goldRocketActivated = false;

    public GameObject bronzeRocketUI;
    public GameObject silverRocketUI;
    public GameObject goldRocketUI;
    public GameObject greenCrocUI;
    public GameObject redCrocUI;
    private GameObject[] rockets;
    private GameObject[] crocs;

    public GameObject greenCroc;
    public GameObject redCroc;

    public GameObject waypointsHolder;
    public List<GameObject> waypoints;
    public GameObject chestsHolder;
    public List<GameObject> chests;

    public GameObject opponentCar;

    int racePos = 2;
    public TMP_Text racePosText;

    int lap = 1;
    public TMP_Text lapText;

    // Start is called before the first frame update
    void Start()
    {
        bronzeRocketUI.SetActive(false);
        silverRocketUI.SetActive(false);
        goldRocketUI.SetActive(false);
        greenCrocUI.SetActive(false);
        redCrocUI.SetActive(false);
        greenCroc.SetActive(false);
        redCroc.SetActive(false);
        rockets = new GameObject[] { bronzeRocketUI, silverRocketUI, goldRocketUI };
        crocs = new GameObject[] { greenCrocUI, redCrocUI };

        foreach (Transform waypoint in waypointsHolder.transform)
        {
            waypoints.Add(waypoint.gameObject);
            //Debug.Log(waypoint.name);
        }

        foreach (Transform chest in chestsHolder.transform)
        {
            waypoints.Add(chest.gameObject);
            //Debug.Log(waypoint.name);
        }

        MaxSpeed = GlobalData.MaxSpeed;
        Acceleration = GlobalData.Acceleration;
        coinCount = GlobalData.coins;
        coinCountText.text = "Coins: " + coinCount;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(waypoints.Count);
        if (waypoints.Count > 0)
        {
            calcRacePos();
        }
        else
        {
            lap += 1;
            if (lap > 3)
            {
                //Player wins
                GlobalData.nextRace = "Track 2";
                SceneManager.LoadScene("WinRace");
            }
            else
            {
                lapText.text = "Lap: " + lap;

                foreach (Transform waypoint in waypointsHolder.transform)
                {
                    waypoints.Add(waypoint.gameObject);
                    //Debug.Log(waypoint.name);
                }

                foreach(GameObject chest in chests)
                {
                    chest.SetActive(true);
                }
            }
        }

        checkIfOnRoad();
        Move();
        Turn();
        UseItem();
        Animate();

        //transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
    }

    void checkIfOnRoad()
    {
        LayerMask mask = LayerMask.GetMask("Roads");

        //Raycast downwards from 1 unit above the car, with a maximum length of infinity, only checking for collision with roads
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, Mathf.Infinity, mask))
        {
            //Debug.Log("on road");
            MaxSpeed = GlobalData.MaxSpeed;
        }
        else
        {
            //Debug.Log("off road");
            MaxSpeed = GlobalData.MaxSpeed - 30;
        }
    }

    void calcRacePos()
    {
        foreach(GameObject waypoint in waypoints)
        {
            //Work out dot product between car and waypoint
            Vector3 carWaypointDistanceNorm = (waypoint.transform.position - transform.position).normalized;
            float dotProduct = Vector3.Dot(transform.forward, carWaypointDistanceNorm);

            //If dot product > 0, this shows that is the next waypoint so break out of waypoint checking
            //If dot product < 0, remove this waypoint (check if waypoint passed)
            if (dotProduct > 0)
            {
                break;
            }
            else
            {
                waypoints.Remove(waypoint);
            }
        }
        

        //Check if player and opponent are on different laps
        int oppLap = opponentCar.GetComponent<OpponentCar>().lap;
        if (lap > oppLap)
        {
            racePos = 1;
        }
        else if (lap < oppLap)
        {
            racePos = 2;
        }
        else
        {
            //Check if player and opponent have a different number of waypoints left
            int oppWaypointsLeft = opponentCar.GetComponent<OpponentCar>().waypoints.Count;
            //Debug.Log(waypoints.Count);
            //Debug.Log(oppWaypointsLeft);
            if (oppWaypointsLeft > waypoints.Count)
            {
                racePos = 1;
            }
            else if (oppWaypointsLeft < waypoints.Count)
            {
                racePos = 2;
            }
            else
            {
                //Check difference in player and opponent distances between them and the next waypoint
                float playerDistanceToWaypoint = Vector3.Distance(transform.position, waypoints[0].transform.position);
                float oppDistanceToWaypoint = Vector3.Distance(opponentCar.transform.position, waypoints[0].transform.position);
                Debug.Log(playerDistanceToWaypoint);
                Debug.Log(oppDistanceToWaypoint);

                if (playerDistanceToWaypoint < oppDistanceToWaypoint)
                {
                    racePos = 1;
                }
                else
                {
                    racePos = 2;
                }
            }
        }

        

        if (racePos == 1)
        {
            racePosText.text = "1st";
        }
        else
        {
            racePosText.text = "2nd";
        }

    }

    private void Move()
    {

        if (Input.GetKey(KeyCode.W))
        {
            if (Speed < MaxSpeed && Speed >= 0)
                Speed += Acceleration * Time.deltaTime;
            else if (Speed > -MaxSpeed && Speed < 0)
                Speed += BrakePower * Time.deltaTime;

        }
        else if (Input.GetKey(KeyCode.S))
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

        //Slow down car while boosted
        if (Speed > MaxSpeed)
        {
            Speed -= Acceleration * Time.deltaTime;
        }

        transform.Translate(0, 0, Speed * Time.deltaTime);
        speedText.text = "" + Math.Round(Speed);
    }

    private void Turn()
    {
        //float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        //Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -TurnSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.D))
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

    private void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.Return) && itemHeld != Items.NONE)
        {
            switch (itemHeld)
            {
                case Items.BRONZE_ROCKET:
                    Speed += BoostAmount;
                    if (Speed > MaxSpeed + BoostAmount)
                        Speed = MaxSpeed + BoostAmount;
                    itemHeld = Items.NONE;
                    rockets[0].SetActive(false);
                    break;

                case Items.SILVER_ROCKET:
                    Speed += BoostAmount;
                    if (Speed > MaxSpeed + BoostAmount)
                        Speed = MaxSpeed + BoostAmount;
                    silverRocketCount--;
                    if (silverRocketCount <= 0)
                    {
                        itemHeld = Items.NONE;
                        rockets[1].SetActive(false);
                    }
                    break;

                case Items.GOLD_ROCKET:
                    if (!goldRocketActivated)
                        goldRocketActivated = true;
                    Speed += BoostAmount;
                    if (Speed > MaxSpeed + BoostAmount)
                        Speed = MaxSpeed + BoostAmount;
                    break;

                case Items.GREEN_CROCODILE:
                    GreenCroc greenCrocScript = greenCroc.GetComponent<GreenCroc>();
                    greenCroc.SetActive(true);
                    greenCroc.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    greenCroc.transform.rotation = transform.rotation;
                    greenCrocScript.Speed = 90;
                    crocs[0].SetActive(false);
                    itemHeld = Items.NONE;
                    break;

                case Items.RED_CROCODILE:
                    RedCroc redCrocScript = redCroc.GetComponent<RedCroc>();
                    redCroc.SetActive(true);
                    redCroc.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    redCroc.transform.rotation = transform.rotation;
                    redCrocScript.Speed = 70;
                    crocs[1].SetActive(false);
                    itemHeld = Items.NONE;
                    break;
            }
        }

        if (goldRocketActivated)
        {
            goldRocketTime -= Time.deltaTime;
            if (goldRocketTime <= 0)
            {
                itemHeld = Items.NONE;
                goldRocketTime = 0f;
                goldRocketActivated = false;
                rockets[2].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Coin") && obj.gameObject.activeSelf)
        {
            obj.gameObject.SetActive(false);
            coinCount++;
            GlobalData.coins++;
            coinCountText.text = "Coins: " + coinCount;
        }
        else if (obj.CompareTag("Chest") && obj.gameObject.activeSelf)
        {
            obj.gameObject.SetActive(false);
            itemHeld = Items.NONE;
            foreach(GameObject rocket in rockets)
                rocket.SetActive(false);
            foreach (GameObject croc in crocs)
                croc.SetActive(false);

            int itemChooser = UnityEngine.Random.Range(1, 2);
            switch (itemChooser)
            {
                case 0:
                    int rocketChosen = UnityEngine.Random.Range(0, 3);
                    rockets[rocketChosen].SetActive(true);
                    switch (rocketChosen)
                    {
                        case 0:
                            itemHeld = Items.BRONZE_ROCKET;
                            break;
                        case 1:
                            itemHeld = Items.SILVER_ROCKET;
                            silverRocketCount = 3;
                            break;
                        case 2:
                            itemHeld = Items.GOLD_ROCKET;
                            goldRocketTime = 5f;
                            break;
                    }

                    break;

                case 1:
                    int crocChosen = UnityEngine.Random.Range(1, 2);
                    crocs[crocChosen].SetActive(true);
                    switch (crocChosen)
                    {
                        case 0:
                            itemHeld = Items.GREEN_CROCODILE;
                            break;
                        case 1:
                            itemHeld = Items.RED_CROCODILE;
                            break;
                    }
                    break;
            }
        }
        else if (!obj.CompareTag("Croc"))
        {
            Speed = 0f;
        }
    }
}
