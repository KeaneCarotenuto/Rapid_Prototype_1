using System;
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


    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = 11;

        ogCol = GetComponent<MeshRenderer>().material.color;
    }

    void Awake()
    {
# if UNITY_EDITOR
        ParticleSystem ps = GetComponent<ParticleSystem>();

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
        if (onFire && other.layer == 12)
        {
            gameObject.layer = 11;

            ParticleSystem ps = GetComponent<ParticleSystem>();
            if (ps)
            {
                if (GetComponentInChildren<Light>())
                {
                    GetComponentInChildren<Light>().enabled = false;
                }

                ps.Stop();
                //Destroy(ps, 5);
                onFire = false;
            }
        }
        else if ((!GetComponent<ParticleSystem>() || (GetComponent<ParticleSystem>() && GetComponent<ParticleSystem>().isStopped)) && gameObject.layer == 11 && other.layer != 12)
        {
            GetComponent<MeshRenderer>().material.color = ogCol * 0.1f;
            onFire = true;
            gameObject.layer = 10;

            if (GetComponentInChildren<Light>()) GetComponentInChildren<Light>().enabled = true;
            else Instantiate(light, transform, false);

            ParticleSystem ps = GetComponent<ParticleSystem>();

            if (!ps)
            {
                GetComponent<MeshRenderer>().material.color *= 0.1f;

                ps = gameObject.AddComponent<ParticleSystem>();

                Type type = ps.GetType();

                PropertyInfo[] pinfos = type.GetProperties();
                foreach (var pinfo in pinfos)
                {
                    if (pinfo.CanWrite)
                    {
                        try
                        {
                            pinfo.SetValue(ps, pinfo.GetValue(fire.GetComponent<ParticleSystem>(), null), null);
                        }
                        catch { }
                    }
                }
                FieldInfo[] finfos = type.GetFields();
                foreach (var finfo in finfos)
                {
                    finfo.SetValue(ps, finfo.GetValue(fire.GetComponent<ParticleSystem>()));
                }


                //gameObject.AddComponent<ParticleSystem>(fire.GetComponent<ParticleSystem>());

                //ComponentUtility.CopyComponent(fire.GetComponent<ParticleSystem>());
                //UnityEditorInternal.ComponentUtility.PasteComponentValues(ps);

                ParticleSystem.ShapeModule sm = ps.shape;
                sm.shapeType = ParticleSystemShapeType.MeshRenderer;
                sm.meshShapeType = ParticleSystemMeshShapeType.Triangle;
                sm.meshRenderer = GetComponent<MeshRenderer>();

            }

            ps.Play();
        }
    }
}
