using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public SoundController m_audio;
    public float m_moveSpeed;

    public Vector3 m_velocity;

    public Animator m_anim;

    PlayerHealth m_healthComp;

    public bool isCharging;

    public LayerMask canCharge;

    // Start is called before the first frame update
    void Start()
    {
        m_healthComp = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        if (m_healthComp.dead)
        {
            return;
        }

        m_velocity = Vector3.zero;

        if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
        {
            if (!m_anim.GetBool("isCharging")) m_anim.SetBool("isCharging", true);
            isCharging = true;
        }
        else
        {
            if (!m_anim.GetCurrentAnimatorStateInfo(0).IsName("Armature|RELEASE") && !m_anim.GetCurrentAnimatorStateInfo(0).IsName("Armature|CHARGE"))
            {
                isCharging = false;
            }

            m_anim.SetBool("isCharging", false);

            float z = -Input.GetAxis("Horizontal");
            float x = Input.GetAxis("Vertical");

            m_velocity = Vector3.ClampMagnitude(new Vector3(x + z, 0, z - x), 1.0f) * m_moveSpeed;




            controller.Move(m_velocity * Time.deltaTime);
            controller.Move(new Vector3(0, -2, 0));
        }

        if (m_velocity.magnitude > 0)
        {
            m_anim.SetBool("isWalking", true);
            m_audio.SetRunning(false);
            transform.LookAt(transform.position + m_velocity);

            if (isCharging)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, 1, canCharge);

                foreach (Collider _col in hits)
                {
                    EnemyHealth eHealth = _col.GetComponent<EnemyHealth>();

                    if (eHealth) eHealth.TakeDamage(100);
                }
            }
        }
        else
        {
            m_anim.SetBool("isWalking", false);
            m_audio.SetRunning(true);
        }



    }
}
