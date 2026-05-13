using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Boat : MonoBehaviour
{
    private string id;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float buoyancyForce;
    [SerializeField] private float decelerationRate;
    [SerializeField] private string characterName;
    
    [SerializeField] private bool isMoving;
    [SerializeField] private bool inWater;
    [SerializeField] private Rigidbody rb;
    private Vector3 targetPosition;

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
