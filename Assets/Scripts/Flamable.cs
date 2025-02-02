﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Flamable : MonoBehaviour
{
    public bool onFire = false;
    public bool explodable;
    public float m_ExplosionTimer;
    public GameObject m_ExplosionPrefab;


    public GameObject fire;
    public new GameObject light;

    private Color ogCol;

    bool m_firstBurn = true;

    public GameObject firstBurnObj;

    float startTime = float.MaxValue;
    float maxTime = 5;

    bool burntOut = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!Application.IsPlaying(gameObject)) return;

        gameObject.layer = 11;

        ogCol = GetComponent<MeshRenderer>().material.color;
    }

    void Awake()
    {
        //this.enabled = true;

        ParticleSystem ps = GetComponent<ParticleSystem>();

#if UNITY_EDITOR
        this.enabled = true;

        if (!ps)
        {
            ps = gameObject.AddComponent<ParticleSystem>();

        }

        UnityEditorInternal.ComponentUtility.CopyComponent(fire.GetComponent<ParticleSystem>());
        UnityEditorInternal.ComponentUtility.PasteComponentValues(ps);
#endif

        ParticleSystem.ShapeModule sm = ps.shape;
        sm.shapeType = ParticleSystemShapeType.MeshRenderer;
        sm.meshShapeType = ParticleSystemMeshShapeType.Triangle;
        sm.meshRenderer = GetComponent<MeshRenderer>();

        ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.IsPlaying(gameObject)) return;

        if (!explodable && Time.time - startTime > maxTime)
        {
            burntOut = true;
            StopFire();
        }

        if (onFire && explodable)
        {
            if (m_ExplosionTimer <= 0)
            {
                GameObject.Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
            else
            {
                m_ExplosionTimer -= Time.deltaTime;
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!Application.IsPlaying(gameObject)) return;

        if (onFire && other.layer == 12)
        {
            gameObject.layer = 11;

            StopFire();
        }
        else if ((!GetComponent<ParticleSystem>() || (GetComponent<ParticleSystem>() && GetComponent<ParticleSystem>().isStopped)) && gameObject.layer == 11 && other.layer != 12)
        {
            StartFire();
        }
    }

    public void StopFire()
    {
        if (!Application.IsPlaying(gameObject)) return;

        if (GetComponentInChildren<Light>())
        {
            GetComponentInChildren<Light>().enabled = false;
        }

        ParticleSystem ps = GetComponent<ParticleSystem>();

        if (ps) ps.Stop();
        onFire = false;
    }

    public void StartFire()
    {
        if (burntOut) return;

        if (m_firstBurn)
        {
            startTime = Time.time;

            m_firstBurn = false;

            Destroy(Instantiate(firstBurnObj, transform.position, Quaternion.identity, null), 5);
        }

        if (!Application.IsPlaying(gameObject)) return;

        GetComponent<MeshRenderer>().material.color = ogCol * 0.1f;
        onFire = true;
        gameObject.layer = 10;

        if (GetComponentInChildren<Light>()) GetComponentInChildren<Light>().enabled = true;
        else Instantiate(light, transform, false);

        ParticleSystem ps = GetComponent<ParticleSystem>();

        ps.Play();
    }
}
