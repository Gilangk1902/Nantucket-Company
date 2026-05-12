using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ship : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    public void Move(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    public bool HasReachedDestination()
    {
        float tolerance = 0.5f;
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= tolerance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
