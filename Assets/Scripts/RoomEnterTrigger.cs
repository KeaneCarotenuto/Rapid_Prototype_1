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

    public GameObject m_CameraHolder;
    public bool m_RoomTriggered = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
