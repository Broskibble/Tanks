using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    public Rigidbody2D rbd2d;
    public float maxSpeed = 10;
    public float rotationSpeed = 100;
    private Vector2 movementVector;

    private void Awake()
    {
        rbd2d = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
    }

   private void FixedUpdate()
    {
        rbd2d.velocity = (Vector2)transform.up * 
            movementVector.y * maxSpeed * Time.fixedDeltaTime;
        rbd2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x *
        rotationSpeed * Time.fixedDeltaTime));
    }
}
