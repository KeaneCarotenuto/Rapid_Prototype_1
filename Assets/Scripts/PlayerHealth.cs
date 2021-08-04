using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public UnityEvent OnDeath;
    public UnityEvent OnDamage;
    public float m_health = 100;
    public float m_maxhealth = 100;


    public bool dead = false;

    public Image m_Bar;

    Color copyCol;
    Color copyParticleCol;
    Color copyParticleEmmisionCol;

    // Start is called before the first frame update
    void Start()
    {
        copyCol = GetComponent<MeshRenderer>().material.color;
        copyParticleCol = GetComponent<ParticleSystemRenderer>().material.color;
        copyParticleEmmisionCol = GetComponent<ParticleSystemRenderer>().material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        m_Bar.fillAmount = m_health / m_maxhealth;
    }

    public void TakeDamage(float _dmg)
    {
        if (!dead)
        {
            m_health -= _dmg;

            UpdateAppearance();

            OnDamage.Invoke();

            if (m_health <= 0) Die();
        }
    }

    public void Die()
    {
        dead = true;
        m_health = 0;
        transform.LookAt(transform.position - transform.up);
        OnDeath.Invoke();
        Invoke("ReturnToMenu", 2);
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void UpdateAppearance()
    {
        //Fix the alpha being changed on colour change

        GetComponent<ParticleSystemRenderer>().material.color = copyParticleCol * (m_health / 100.0f);
        GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", copyParticleEmmisionCol * (m_health / 100.0f));
        GetComponent<MeshRenderer>().material.color = copyCol * (m_health / 100.0f);

        ParticleSystem.VelocityOverLifetimeModule vel = GetComponent<ParticleSystem>().velocityOverLifetime;
        vel.speedModifierMultiplier = m_health / 100.0f;
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(0.05f);
    }
}
