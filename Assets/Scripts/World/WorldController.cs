using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldController : MonoBehaviour
{
    [Header("Required Attributes")]
    [SerializeField] private Game game;

    [Header("Whales Spawning Attributes")]
    [SerializeField] private GameObject[] whalesCatalog;
    [SerializeField] private List<GameObject> spawnedWhale;
    [SerializeField] private int maxWhale;
    [SerializeField] private List<Vector3> WhaleSightingPosition;

    private void Start()
    {
        SetWhaleSightingPosition();
    }
    private void Update()
    {
        TriggerSpawn();
    }

    private void TriggerSpawn()
    {
        Vector2 shipLocation = new Vector2(
            game.GetPlayerController().GetShipControrller().GetShip().gameObject.transform.position.x, 
            game.GetPlayerController().GetShipControrller().GetShip().gameObject.transform.position.z
        );

        for (int i = WhaleSightingPosition.Count - 1; i >= 0; i--)
        {
            Vector3 sightingPosition = WhaleSightingPosition[i];

            if (Vector2.Distance(
                shipLocation,
                new Vector2(sightingPosition.x, sightingPosition.z)
            ) < 15f)
            {
                SpawnWhale(sightingPosition);

                WhaleSightingPosition.RemoveAt(i);
            }
        }
    }

    private void SetWhaleSightingPosition()
    {
        WhaleSightingPosition.Clear();
        for(int i=0; i<maxWhale; i++)
        {
            int xPosition = Random.Range(0, 100);
            int zPosition = Random.Range(0, 100);

            WhaleSightingPosition.Add(new Vector3(xPosition, 0, zPosition));
        }
    }

    private void SpawnWhale(Vector3 position)
    {
        GameObject whale = Instantiate(whalesCatalog[GetRandomWhale()], position, Quaternion.identity);
        spawnedWhale.Add(whale);
    }

    private int GetRandomWhale()
    {
        return Random.Range(0, whalesCatalog.Length); 
    }
}
