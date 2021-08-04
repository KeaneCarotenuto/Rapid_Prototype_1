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
    public Image m_Vignette;

    Color copyParticleCol;
    Color copyParticleEmmisionCol;

    public float m_HealthDrainMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        copyParticleCol = GetComponent<ParticleSystemRenderer>().material.color;
        copyParticleEmmisionCol = GetComponent<ParticleSystemRenderer>().material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        m_Bar.fillAmount = m_health / m_maxhealth;
        m_Vignette.color = new Color(0,0,0, 1 - (m_health / m_maxhealth));
        TakeDamage(Time.deltaTime * m_HealthDrainMultiplier);
    }

    public void TakeDamage(float _dmg)
    {
        if (!dead)
        {
            m_health -= _dmg;

            UpdateAppearance();

            OnDamage.Invoke();

            if (m_health <= 0) Die();
            if (m_health >= m_maxhealth) m_health = m_maxhealth;
            
        }
    }

    public void Die()
    {
        GameObject mc = GameObject.Find("Main Camera");
        mc.GetComponent<ZoomOut>().StartZoom();
        //Cinemachine.ICinemachineCamera cam = mc.GetComponent<Cinemachine.CinemachineBrain>().ActiveVirtualCamera;
        


        dead = true;
        m_health = 0;
        transform.LookAt(transform.position - transform.up);
        OnDeath.Invoke();
        Invoke("ReturnToMenu", 5);
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

        ParticleSystem.VelocityOverLifetimeModule vel = GetComponent<ParticleSystem>().velocityOverLifetime;
        vel.speedModifierMultiplier = m_health / 100.0f;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == 12)
        {
            TakeDamage(0.05f);
        }
    }
}
