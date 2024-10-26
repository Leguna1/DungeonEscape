using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject floorPrefab;      // Prefab for the floor
    public GameObject wallPrefab;       // Prefab for the walls (hallways)
    public Vector2 floorSize;           // Size of the dungeon floor (width, length)
    public float hallwayWidth = 1f;     // Width of the outer hallways
    public float wallHeight = 2.5f;     // Height of the walls

    private void Start()
    {
        GenerateFloor();
        GenerateOuterHallways();
    }

    // Generate the floor based on the provided size
    void GenerateFloor()
    {
        GameObject floor = Instantiate(floorPrefab, new Vector3(floorSize.x / 2, 0, floorSize.y / 2), Quaternion.identity);
        floor.transform.localScale = new Vector3(floorSize.x, 1, floorSize.y);  // Adjust the scale of the plane to cover the area
    }

    // Generate the outer hallways (corridors) around the perimeter of the map
    void GenerateOuterHallways()
    {
        float roomSize = floorSize.x / 3; // Assuming 3x3 rooms

        // Top hallway
        CreateHallway(new Vector3(floorSize.x / 2, wallHeight / 2, floorSize.y - hallwayWidth / 2), new Vector3(floorSize.x, wallHeight, hallwayWidth));
        
        // Bottom hallway
        CreateHallway(new Vector3(floorSize.x / 2, wallHeight / 2, hallwayWidth / 2), new Vector3(floorSize.x, wallHeight, hallwayWidth));

        // Left hallway
        CreateHallway(new Vector3(hallwayWidth / 2, wallHeight / 2, floorSize.y / 2), new Vector3(hallwayWidth, wallHeight, floorSize.y));
        
        // Right hallway
        CreateHallway(new Vector3(floorSize.x - hallwayWidth / 2, wallHeight / 2, floorSize.y / 2), new Vector3(hallwayWidth, wallHeight, floorSize.y));
    }

    // Helper function to instantiate a hallway (a wall along the edge)
    void CreateHallway(Vector3 position, Vector3 scale)
    {
        GameObject hallway = Instantiate(wallPrefab, position, Quaternion.identity);
        hallway.transform.localScale = scale;  // Scale the hallway to cover the desired area
    }
}
