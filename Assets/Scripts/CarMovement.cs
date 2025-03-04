using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_Speed = 0f;
    public float m_MaxSpeed = 50f;
    public float m_TurnSpeed = 180f;
    public float m_Acceleration = 0.01f;

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
    }

    // Update is called once per frame
    void Update()
    {
        //m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        //m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
        Move();
        Turn();
    }

    private void Move()
    {
        //Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        //m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

        if (Input.GetKey("w"))
        {
            if (m_Speed < m_MaxSpeed)
                m_Speed += m_Acceleration;
        }
        else if (Input.GetKey("s"))
        {
            if (m_Speed > -m_MaxSpeed)
                m_Speed -= m_Acceleration;
        }
        else
        {
            if (m_Speed > -m_Acceleration && m_Speed < m_Acceleration)
                m_Speed = 0f;
            if (m_Speed > 0f)
                m_Speed -= m_Acceleration;
            else if (m_Speed < 0f)
                m_Speed += m_Acceleration;
        }

        transform.Translate(0, 0, m_Speed * Time.deltaTime);
    }

    private void Turn()
    {
        //float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        //Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);

        if (Input.GetKey("a"))
        {
            transform.Rotate(0, -m_TurnSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey("d"))
        {
            transform.Rotate(0, m_TurnSpeed * Time.deltaTime, 0);
        }
    }
}
