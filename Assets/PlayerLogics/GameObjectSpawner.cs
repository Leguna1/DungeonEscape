using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : MonoBehaviour
{
    [SerializeField] private Vector3[] spawnPoints; // Spawn points for the mobs
    [SerializeField] private float spawnTime = 5f; // Respawn delay after a mob is destroyed
    [SerializeField] private GameObject gameObjectPrefab; // The mob prefab to spawn

    private Dictionary<Vector3, GameObject> spawnedObjects = new Dictionary<Vector3, GameObject>();
    private Dictionary<Vector3, float> respawnTimers = new Dictionary<Vector3, float>();

    void Start()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            SpawnObjectAtPoint(spawnPoint); // Initial spawn at each specified point
        }
    }

    void Update()
    {
        List<Vector3> pointsToRespawn = new List<Vector3>();

        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnedObjects[spawnPoint] == null) // Check if mob at this point was destroyed
            {
                respawnTimers[spawnPoint] += Time.deltaTime;

                if (respawnTimers[spawnPoint] >= spawnTime) // Check if the respawn delay has elapsed
                {
                    pointsToRespawn.Add(spawnPoint);
                    respawnTimers[spawnPoint] = 0; // Reset the timer
                }
            }
        }

        foreach (var point in pointsToRespawn)
        {
            SpawnObjectAtPoint(point);
        }
    }

    private void SpawnObjectAtPoint(Vector3 spawnPoint)
    {
        GameObject spawnedObject = Instantiate(gameObjectPrefab, spawnPoint, Quaternion.identity);
        spawnedObjects[spawnPoint] = spawnedObject;
        respawnTimers[spawnPoint] = 0; // Ensure the timer is reset for this spawn point
    }
}