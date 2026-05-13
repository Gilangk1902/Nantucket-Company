using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Ship : MonoBehaviour
{
    private bool isMoving;
    private bool inWater;
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float buoyancyForce;
    [SerializeField] private float decelerationRate;

    [SerializeField] private Rigidbody rb;
    private Vector3 targetPosition;

    public void Move(Vector3 destination)
    {
        targetPosition = destination;
        isMoving = true;
    }

    private void FixedUpdate()
    {
        if (!isMoving)
        {
            //rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, decelerationRate * Time.fixedDeltaTime);
            if (inWater)
            {
                rb.AddForce(Vector3.up * buoyancyForce * Time.fixedDeltaTime, ForceMode.Force);
                rb.drag = 3;
            }
            else
            {
                rb.drag = 0;
            }
        }
        else
        {
            if (inWater)
            {
                rb.AddForce(Vector3.up * buoyancyForce * Time.fixedDeltaTime, ForceMode.Force);
                rb.drag = 3;
            }
            else
            {
                rb.drag = 0;
            }

            // Direction to target
            Vector3 direction = (targetPosition - transform.position);
            direction.y = 0f;

            // Stop if close enough
            if (direction.magnitude < 2f) //TODO: adjust value
            {
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, decelerationRate * Time.fixedDeltaTime);
                //rb.velocity = Vector3.zero;
                isMoving = false;

            }
            else
            {
                // Rotate toward target
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                Quaternion smoothRotation = Quaternion.Slerp(
                    rb.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime
                );

                rb.MoveRotation(smoothRotation);

                // Move forward
                Vector3 moveDirection = transform.forward * moveSpeed;
                rb.AddForce(new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z) * moveSpeed * Time.fixedDeltaTime, ForceMode.Force);
                //rb.velocity = new Vector3(
                //    moveDirection.x,
                //    rb.velocity.y,
                //    moveDirection.z
                //);
            }

        }
    }
    public bool HasReachedDestination()
    {
        return false;
    }

    [SerializeField] private LayerMask waterLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & waterLayer) != 0)
        {
            inWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & waterLayer) != 0)
        {
            inWater = false;
        }

    }
}
