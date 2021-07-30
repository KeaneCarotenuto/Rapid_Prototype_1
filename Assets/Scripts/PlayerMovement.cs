using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float m_moveSpeed;

    public Vector3 m_velocity;

    PlayerHealth m_healthComp;

    // Start is called before the first frame update
    void Start()
    {
        m_healthComp = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_healthComp.dead)
        {
            return;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        m_velocity = Vector3.ClampMagnitude(new Vector3(x + z,0,z - x), 1.0f) * m_moveSpeed;

        controller.Move(m_velocity * Time.deltaTime);

        if (m_velocity.magnitude > 0)
        {
            transform.LookAt(transform.position + m_velocity);
        }
        
    }
}
