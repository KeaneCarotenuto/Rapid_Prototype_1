using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float shakeAmount = 1.0f;

    float lsMulti = 1;
    float lsTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Cinemachine.CinemachineBrain>().ManualUpdate();
        if (lsTime > 0)
        {
            lsTime -= Time.deltaTime;
            ShakeCam(lsMulti);
        }
        
    }

    public void ShakeCam(float multiplier = 1.0f) 
    {
        Vector3 rand = multiplier * new Vector3(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount));
        transform.position = Vector3.Lerp(transform.position, transform.position + rand, 0.5f);
    }

    public void LongShake(float multiplier, float duration)
    {
        lsMulti = Mathf.Max(lsMulti, multiplier);
        lsTime = Mathf.Max(lsTime, duration);
    }
}
