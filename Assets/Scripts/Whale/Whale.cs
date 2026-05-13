using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{
    [Header("Required Attributes")]
    [SerializeField] private Rigidbody rb;

    [Header("Movement Stats")]
    private Vector3 targetPosition;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float decelerationRate;
    [SerializeField] private float whaleRestTime;

    [Header("Water Physics")]
    private float fluidDensity = 1000;
    private float gravity = 9.8f;
    [SerializeField] private float whaleVolume;

    [Header("States")]
    [SerializeField] private bool isMoving;
    [SerializeField] private bool inWater;

    private float CountBuoyancy()
    {

        float depth = 0f - transform.position.y;
        return fluidDensity * whaleVolume * gravity * depth;
    }

    private void FixedUpdate()
    {
        float buoyancy = CountBuoyancy();
        if (!isMoving)
        {
            //rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, decelerationRate * Time.fixedDeltaTime);
            if (inWater)
            {
                rb.AddForce(Vector3.up * buoyancy * Time.fixedDeltaTime, ForceMode.Force);
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
                rb.AddForce(Vector3.up * buoyancy * Time.fixedDeltaTime, ForceMode.Force);
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
            }

        }
    }

    [SerializeField] private LayerMask waterLayer;

    private void OnTriggerEnter(Collider other)
    {
        //transform.position.y < 2f && transform.position.y > 0f
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

    public void Move(Vector3 destination)
    {
        targetPosition = destination;
        isMoving = true;
    }

    public bool GetIsWhaleMoving()
    {
        return this.isMoving;
    }

    public float GetWhaleRestTime()
    {
        return this.whaleRestTime;
    }
}
