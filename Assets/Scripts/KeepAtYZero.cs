﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAtYZero : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
    }
}
