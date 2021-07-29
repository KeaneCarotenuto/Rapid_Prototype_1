using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject m_player;
    public PlayerHealth m_pHealth;
    public NavMeshAgent m_selfAgent;

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
            m_selfAgent.SetDestination(m_player.transform.position);
        }
        else
        {
            m_selfAgent.isStopped = true;
        }
    }
}
