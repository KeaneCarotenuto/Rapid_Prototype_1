using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    Quaternion newRot = Quaternion.Euler(0, 180, 0);
    float angleChange = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float q = Quaternion.Dot(transform.rotation, newRot);

        if (q >= 0.99f)
        {
            newRot = Quaternion.Euler(Random.Range(-angleChange, angleChange), Random.Range(180 - angleChange, 180 + angleChange), Random.Range(-angleChange, angleChange));
        }
        

        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.001f);;
    }
}
