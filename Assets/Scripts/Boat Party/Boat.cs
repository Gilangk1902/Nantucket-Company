using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Boat : MonoBehaviour
{
    [Header("Required Attributes")]
    private string id;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private string characterName;

    [Header("Movement Stats")]
    private Vector3 targetPosition;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float decelerationRate;

    [Header("Water Physics")]
    private float fluidDensity = 1000;
    [SerializeField] private float boatVolume;
    private float gravity =  9.8f;

    [Header("States")]
    [SerializeField] private bool isMoving;
    [SerializeField] private bool inWater;


    private void Awake()
    {
        id = GenerateID();
    }
    public string getId()
    {
        return this.id;
    }

    public string getCharacterName()
    {
        return this.characterName;
    }
    private float CountBuoyancy()
    {
        
        float depth = 0f - transform.position.y;
        return fluidDensity * boatVolume * gravity * depth;
    }

    private void FixedUpdate()
    {
        float buoyancy = CountBuoyancy();
        Debug.Log(buoyancy);
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

    public static string GenerateID()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder("C");
        for (int i = 0; i < 5; i++)
        {
            int digit = Random.Range(0, 10);
            sb.Append(digit);
        }
        return sb.ToString();
    }

    public void Move(Vector3 destination)
    {
        targetPosition = destination;
        isMoving = true;
    }

    public bool HasReachedDestination()
    {
        return false;
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
}
