using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    bool doZoom = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (doZoom)
        {
            GetComponent<Camera>().orthographicSize *= 1.005f;
            GetComponent<Camera>().farClipPlane *= 1.005f;
            
        }
    }

    public void StartZoom()
    {
        transform.position -= transform.forward * 100;
        doZoom = true;
        Destroy(GetComponent<Cinemachine.CinemachineBrain>());
    }
}
