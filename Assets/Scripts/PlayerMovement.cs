using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float m_moveSpeed;

    public Vector3 m_velocity;

    public Animator m_anim;

    PlayerHealth m_healthComp;

    bool isCharging;

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

        m_velocity = Vector3.zero;

        if (Input.GetButton("Fire1"))
        {
            if (!m_anim.GetBool("isCharging")) m_anim.SetBool("isCharging", true);
        }
        else
        {
            m_anim.SetBool("isCharging", false);

            float z = -Input.GetAxis("Horizontal");
            float x = Input.GetAxis("Vertical");

            m_velocity = Vector3.ClampMagnitude(new Vector3(x + z, 0, z - x), 1.0f) * m_moveSpeed;

            controller.Move(m_velocity * Time.deltaTime);
        }

        if (m_velocity.magnitude > 0)
        {
            m_anim.SetBool("isWalking", true);
            transform.LookAt(transform.position + m_velocity);
        }
        else
        {
            m_anim.SetBool("isWalking", false);
        }

        

    }
}
