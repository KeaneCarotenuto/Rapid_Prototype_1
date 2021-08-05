using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedActivation : MonoBehaviour
{

    public float m_delay;
    float m_timer;
    // Start is called before the first frame update
    void Start() {
        m_timer = m_delay;
        m_coll.enabled = false;
    }

    public BoxCollider m_coll;
    

    // Update is called once per frame
    void Update()
    {
        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            m_coll.enabled = true;
        }
    }
}
