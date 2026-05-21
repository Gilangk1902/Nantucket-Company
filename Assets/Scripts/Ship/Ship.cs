using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Ship : MonoBehaviour
{
    [Header("Required Attributes")]
    [SerializeField] private Rigidbody rb;

    [Header("Movement Stats")]
    private Vector3 targetPosition;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float decelerationRate;

    [Header("Water Physics")]
    private float fluidDensity = 1000;
    [SerializeField] private float shipVolume;
    private float gravity = 9.8f;

    [Header("States")]
    private bool isMoving;
    private bool inWater;

    [Header("Other Attributes")]
    [SerializeField] private List<Vector3> boatDocksLocation;
    [SerializeField] private LayerMask waterLayer;

    public void Move(Vector3 destination)
    {
        targetPosition = destination;
        isMoving = true;
    }

    private float CountBuoyancy()
    {

        float depth = 0f - transform.position.y;
        return fluidDensity * shipVolume * gravity * depth;
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
            if (direction.magnitude < 10f) //TODO: adjust value
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

    public Vector3 GetBoatDockLocation(int index)
    {
        return boatDocksLocation[index];
    }

    public bool HasReachedDestination(){ return false; }

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
