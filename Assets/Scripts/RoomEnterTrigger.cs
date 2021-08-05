using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class RoomEnterTrigger : MonoBehaviour
{

    public UnityEvent OnRoomTrigger;
    public UnityEvent OnRoomEnter;
    public UnityEvent OnRoomLeave;

    public UnityEvent OnRoomComplete;

    public GameObject m_CameraHolder;
    public bool m_RoomTriggered = false;

    BoxCollider m_roomcollider;

    public bool m_RoomCompleted = false;
    // Start is called before the first frame update
    void Start()
    {
        m_roomcollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_RoomTriggered && !m_RoomCompleted)
        {
            RaycastHit[] hits = Physics.BoxCastAll(this.transform.position, new Vector3(28, 28, 28), Vector3.up, Quaternion.identity, LayerMask.GetMask("Enemy"));
            bool foundenemy = false;
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.GetComponent<EnemyHealth>())
                {
                    foundenemy = true;
                }
            }
            if (!foundenemy)
            {
                m_RoomCompleted = true;
                OnRoomComplete.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !m_RoomTriggered)
        {
            m_RoomTriggered = true;
            OnRoomTrigger.Invoke();
            GetComponent<Room>().GenerateNeighbors();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            m_CameraHolder.SetActive(true);
            OnRoomEnter.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_CameraHolder.SetActive(false);
            OnRoomLeave.Invoke();
        }
    }
}
