using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamable : MonoBehaviour
{
    public bool onFire = false;

   


    public GameObject fire;
    public GameObject light;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = 11;
        
    }

    // Update is called once per frame
    void Update()
    {
       
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
            onFire = true;
            gameObject.layer = 10;

            if (GetComponentInChildren<Light>()) GetComponentInChildren<Light>().enabled = true;
            else Instantiate(light, transform, false);

            ParticleSystem ps = GetComponent<ParticleSystem>();

            if (!ps)
            {
                GetComponent<MeshRenderer>().material.color *= 0.1f; 

                ps = gameObject.AddComponent<ParticleSystem>();

                UnityEditorInternal.ComponentUtility.CopyComponent(fire.GetComponent<ParticleSystem>());
                UnityEditorInternal.ComponentUtility.PasteComponentValues(ps);

                ParticleSystem.ShapeModule sm = ps.shape;
                sm.shapeType = ParticleSystemShapeType.MeshRenderer;
                sm.meshShapeType = ParticleSystemMeshShapeType.Triangle;
                sm.meshRenderer = GetComponent<MeshRenderer>();
                
            }

            ps.Play(); 
        }
    }
}
