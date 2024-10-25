using UnityEngine;
using System.Collections;

public class RealisticTreeGrowth : MonoBehaviour
{
    public GameObject trunkPrefab;
    public GameObject branchPrefab;
    public GameObject leafPrefab;

    public float trunkHeight = 5f;       // Base height of the trunk
    public float trunkThickness = 0.5f;  // Thickness of the trunk at the base
    public int maxBranchDepth = 5;       // Depth of branching
    public float branchAngleVariance = 15f;  // Variance in branch angle
    public float growthSpeed = 0.5f;     // Speed of growth animation

    private bool isGrowing = true;

    void Start()
    {
        // Start by growing the trunk
        StartCoroutine(GrowTrunk(Vector3.up, 0, transform.position, Quaternion.identity, trunkHeight, trunkThickness));
    }

    IEnumerator GrowTrunk(Vector3 direction, int depth, Vector3 position, Quaternion rotation, float height, float thickness)
    {
        if (depth > maxBranchDepth) yield break; // Stop if max depth reached

        // Instantiate the trunk or branch prefab (use trunk prefab for the main trunk)
        GameObject trunk = Instantiate(trunkPrefab, position, rotation);
        trunk.transform.localScale = new Vector3(thickness, height, thickness);  // Set the thickness and height

        // Animate growth over time
        float growth = 0f;
        while (growth < height)
        {
            growth += Time.deltaTime * growthSpeed;
            trunk.transform.localScale = new Vector3(thickness, growth, thickness);  // Grow the trunk/branch vertically
            yield return null;
        }

        // Once the trunk has grown, start adding branches
        if (depth < maxBranchDepth)
        {
            // Create lower branches (larger angles) first
            StartCoroutine(GrowBranch(Vector3.up, depth + 1, trunk.transform.position + trunk.transform.up * height, rotation * Quaternion.Euler(0, 45, branchAngleVariance), height * 0.7f, thickness * 0.8f));

            // Create upper branches (smaller angles) later
            StartCoroutine(GrowBranch(Vector3.up, depth + 1, trunk.transform.position + trunk.transform.up * height, rotation * Quaternion.Euler(0, -45, -branchAngleVariance), height * 0.6f, thickness * 0.7f));
        }
    }

    IEnumerator GrowBranch(Vector3 direction, int depth, Vector3 position, Quaternion rotation, float height, float thickness)
    {
        if (depth > maxBranchDepth) yield break; // Stop if max depth reached

        // Instantiate the branch prefab
        GameObject branch = Instantiate(branchPrefab, position, rotation);
        branch.transform.localScale = new Vector3(thickness, height, thickness);  // Set initial thickness and height

        // Animate branch growth
        float growth = 0f;
        while (growth < height)
        {
            growth += Time.deltaTime * growthSpeed;
            branch.transform.localScale = new Vector3(thickness, growth, thickness);  // Grow the branch vertically
            yield return null;
        }

        // Spawn leaves at the end of the branch when max depth is reached
        if (depth == maxBranchDepth)
        {
            SpawnLeaves(branch.transform.position + branch.transform.up * height);
        }

        // Recursively grow smaller branches
        if (depth < maxBranchDepth)
        {
            // Recursively grow child branches (smaller and thinner)
            StartCoroutine(GrowBranch(Vector3.up, depth + 1, branch.transform.position + branch.transform.up * height, rotation * Quaternion.Euler(0, Random.Range(-branchAngleVariance, branchAngleVariance), Random.Range(-branchAngleVariance, branchAngleVariance)), height * 0.7f, thickness * 0.8f));
        }
    }

    void SpawnLeaves(Vector3 position)
    {
        // Instantiate a cluster of leaves at the given position
        for (int i = 0; i < Random.Range(3, 6); i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0, 0.5f), Random.Range(-0.5f, 0.5f));
            GameObject leaf = Instantiate(leafPrefab, position + offset, Quaternion.identity);
            leaf.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);  // Scale the leaf to an appropriate size
        }
    }
}
