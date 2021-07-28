using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float moveSpeed;

    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = Vector3.ClampMagnitude(new Vector3(x + z,0,z - x), 1.0f);

        controller.Move(move * Time.deltaTime * moveSpeed);

        if (move.magnitude > 0)
        {
            transform.LookAt(transform.position + move);
        }
        
    }
}
