using UnityEngine;
using System.Collections;

public class TreasureSpawner : MonoBehaviour
{
    public GameObject treasureChestPrefab; // Reference to the chest prefab
    public Transform[] spawnLocations; // Array of spawn locations (set 3 in Inspector)
    public float respawnDelay = 5f; // Delay before respawning the chest if it's null

    private GameObject currentChest;

    private void Start()
    {
        SpawnChest();
    }

    private void Update()
    {
        // Check if the chest is null, meaning it needs to be respawned
        if (currentChest == null)
        {
            StartCoroutine(RespawnChestAfterDelay());
        }
    }

    private IEnumerator RespawnChestAfterDelay()
    {
        // Wait for the delay period
        yield return new WaitForSeconds(respawnDelay);

        // Spawn a new chest if none exists
        if (currentChest == null)
        {
            SpawnChest();
        }
    }

    private void SpawnChest()
    {
        // Choose a random spawn location from the array
        int randomIndex = Random.Range(0, spawnLocations.Length);
        Transform spawnPoint = spawnLocations[randomIndex];

        // Instantiate the chest at the selected location and keep reference
        currentChest = Instantiate(treasureChestPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}