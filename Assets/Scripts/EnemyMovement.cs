﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject m_player;
    public PlayerHealth m_pHealth;
    public NavMeshAgent m_selfAgent;

    public Animator m_Anim;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("Player");
        m_pHealth = m_player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_pHealth.dead)
        {
            if (Vector3.Distance(transform.position, m_player.transform.position) < 4)
            {
                m_selfAgent.isStopped = true;
                m_Anim.SetBool("IsWalking", false);
                transform.LookAt(new Vector3(m_player.transform.position.x, transform.position.y, m_player.transform.position.z));
            }
            else
            {
                m_selfAgent.isStopped = false;
                m_Anim.SetBool("IsWalking", true);
                m_selfAgent.SetDestination(m_player.transform.position);
            }
                
        }
        else
        {
            m_selfAgent.isStopped = true;
            m_Anim.SetBool("IsWalking", false);
        }
    }
}
