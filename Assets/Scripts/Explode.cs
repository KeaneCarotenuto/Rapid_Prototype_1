﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float m_ExplosionRadius;
    public float m_ForceMultiplier;



    public ParticleSystem m_ParticleSystem;
    public AudioSource m_AudioSource;



    void Update()
    {
        if (!m_AudioSource.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        GameObject cam = GameObject.Find("Main Camera");
        if (cam)
        {
            Shake shake = cam.GetComponent<Shake>();
            if (shake) shake.LongShake(5, 0.5f);
        }

        m_ParticleSystem.Play();
        m_AudioSource.Play();
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, m_ExplosionRadius, Vector3.up, LayerMask.GetMask("Flamable") | LayerMask.GetMask("Prop") | LayerMask.GetMask("Enemy") | LayerMask.GetMask("Player"));
        foreach (var hit in hits)
        {

            PlayerHealth pHealth = hit.transform.GetComponent<PlayerHealth>();

            if (pHealth)
            {
                pHealth.TakeDamage(-20);
            }

            EnemyHealth eHealth = hit.transform.GetComponent<EnemyHealth>();

            if (eHealth) eHealth.TakeDamage(100);

            if (hit.collider.gameObject.GetComponent<Rigidbody>())
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().AddExplosionForce(m_ForceMultiplier, transform.position, m_ExplosionRadius);
            }
            if (hit.collider.gameObject.GetComponent<Flamable>())
            {
                hit.collider.gameObject.GetComponent<Flamable>().StartFire();
            }

        }

    }


}
