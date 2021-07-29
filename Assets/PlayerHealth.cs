﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float m_health = 100;

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

            UpdateAppearance();

            if (m_health <= 0) Die();
        }
    }

    public void Die()
    {
        dead = true;
        m_health = 0;
        transform.LookAt(transform.position - transform.up);

        Invoke("ReturnToMenu", 2);
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void UpdateAppearance()
    {
        GetComponent<ParticleSystemRenderer>().material.color = new Color(m_health / 100.0f, 0, 0);
        GetComponent<MeshRenderer>().material.color = new Color(m_health / 100.0f, 0, 0);

        ParticleSystem.VelocityOverLifetimeModule vel = GetComponent<ParticleSystem>().velocityOverLifetime;
        vel.speedModifierMultiplier = m_health / 100.0f;
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(0.05f);
    }
}
