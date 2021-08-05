using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyMovement m_movement;

    public GameObject m_player;
    public PlayerHealth m_pHealth;

    public ParticleSystem m_particleSystem;
    ParticleSystem.EmissionModule m_emmision;

    public Animator m_Anim;

    // Start is called before the first frame update
    void Start()
    {
        //m_particleSystem = GetComponent<ParticleSystem>();
        m_emmision = m_particleSystem.emission;
        m_emmision.enabled = false;

        m_movement = GetComponent<EnemyMovement>();

        m_player = GameObject.Find("Player");
        m_pHealth = m_player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        m_emmision.enabled = false;

        if (!m_pHealth.dead)
        {
            if (Vector3.Distance(transform.position, m_player.transform.position) < 4)
            {
                m_emmision.enabled = true;
                m_Anim.SetBool("IsFiring", true);
            }
        }
        else
        {
            m_Anim.SetBool("IsFiring", false);
        }
    }
}
