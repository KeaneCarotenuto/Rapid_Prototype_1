using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float m_health = 100.0f;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float _dmg)
    {
        if (!dead)
        {
            m_health -= _dmg;

            if (m_health <= 0) Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        dead = true;
        m_health = 0;
        transform.LookAt(transform.position - transform.up);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.name);
    }
}
