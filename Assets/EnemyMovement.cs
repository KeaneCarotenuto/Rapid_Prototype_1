using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent selfAgent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        
    }

    // Update is called once per frame
    void Update()
    {
        selfAgent.SetDestination(player.transform.position);
    }
}
