using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
    public float MaxSpeed;
    public float TurnSpeed;
    public float Acceleration;
    public float BrakePower;
    public float BoostAmount;

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

    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private float m_MovementInputValue;
    private float m_TurnInputValue;

    private Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //m_MovementAxisName = "Vertical" + m_PlayerNumber;
        //m_TurnAxisName = "Horizontal" + m_PlayerNumber;
        m_Rigidbody = GetComponent<Rigidbody>();

        bronzeRocketUI.SetActive(false);
        silverRocketUI.SetActive(false);
        goldRocketUI.SetActive(false);
        greenCrocUI.SetActive(false);
        redCrocUI.SetActive(false);
        greenCroc.SetActive(false);
        redCroc.SetActive(false);
        rockets = new GameObject[] { bronzeRocketUI, silverRocketUI, goldRocketUI };
        crocs = new GameObject[] { greenCrocUI, redCrocUI };
    }

    // Update is called once per frame
    void Update()
    {
        //m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        //m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
        Move();
        Turn();
        UseItem();
    }

    private void Move()
    {
        //Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        //m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

        if (Input.GetKey(KeyCode.W))
        {
            if (Speed < MaxSpeed && Speed >= 0)
                Speed += Acceleration;
            else if (Speed > -MaxSpeed && Speed < 0)
                Speed += BrakePower;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Speed > -MaxSpeed && Speed <= 0)
                Speed -= Acceleration;
            else if (Speed < MaxSpeed && Speed > 0)
                Speed -= BrakePower;
        }
        else
        {
            if (Speed > -Acceleration && Speed < Acceleration)
                Speed = 0f;
            if (Speed > 0f)
                Speed -= Acceleration;
            else if (Speed < 0f)
                Speed += Acceleration;
        }

        //Slow down car while boosted
        if (Speed > MaxSpeed)
        {
            Speed -= Acceleration;
        }

        transform.Translate(0, 0, Speed * Time.deltaTime);
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
                    greenCroc.gameObject.SetActive(true);
                    greenCroc.transform.position = new Vector3(transform.position.x, 4, transform.position.z);
                    greenCroc.transform.rotation = transform.rotation;
                    greenCrocScript.Speed = 90;
                    crocs[0].SetActive(false);
                    itemHeld = Items.NONE;
                    break;

                case Items.RED_CROCODILE:

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
            coinCountText.text = "Coins: " + coinCount;
        }
        else if (obj.CompareTag("Chest") && obj.gameObject.activeSelf)
        {
            obj.gameObject.SetActive(false);
            int itemChooser = UnityEngine.Random.Range(0, 2);
            switch (itemChooser)
            {
                case 0:
                    int rocketChosen = UnityEngine.Random.Range(0, 3);
                    rockets[rocketChosen].gameObject.SetActive(true);
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
                    int crocChosen = UnityEngine.Random.Range(0, 2);
                    crocs[crocChosen].gameObject.SetActive(true);
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
    }
}
