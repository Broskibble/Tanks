using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 100.0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Rotate the player left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        // Rotate the player right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
       
        // Move the player forward or backward based on input
        if (verticalInput != 0)
        {
            rb.velocity = transform.forward * verticalInput * moveSpeed;
        }
        else
        {
            rb.velocity = Vector3.zero; // Stop movement if no key is pressed
        }
    }
}

