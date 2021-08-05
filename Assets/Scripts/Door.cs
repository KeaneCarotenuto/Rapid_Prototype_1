using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool m_IsOpen = true;

    public Animator m_Anim;

    public GameObject m_ColliderObj;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_Anim.SetBool("Open", m_IsOpen);

        m_ColliderObj.SetActive(!m_IsOpen);

        
    }
}
