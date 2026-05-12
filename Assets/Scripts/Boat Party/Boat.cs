using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boat : MonoBehaviour
{
    private string id;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private string characterName;


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
        agent.SetDestination(destination);
        Debug.Log("move" + id);
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
