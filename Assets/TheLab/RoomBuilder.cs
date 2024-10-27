using UnityEngine;

public class RoomBuilder : MonoBehaviour
{
    public GameObject floorPrefab; // Floor prefab
    public GameObject wallPrefab; // Wall prefab
    public float floorSize = 20f; // Size of the floor
    public float wallLength = 8f; // Length of each wall section
    public float wallThickness = 0.5f; // Thickness of the walls
    public float wallHeight = 3f; // Height of the walls
    public float gapSize = 1.5f; // Gap size in the middle of each wall

    void Start()
    {
        BuildRoom();
    }

    void BuildRoom()
    {
        // Instantiate the floor at the center
        GameObject floor = Instantiate(floorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        floor.transform.localScale = new Vector3(floorSize / 10, 1, floorSize / 10);

        // Half lengths
        float halfFloor = floorSize / 2;
        float halfWall = wallLength / 2;
        float halfGap = gapSize / 2;

        // Position each wall section with a gap in the middle of each side
        // Top wall
        PlaceWallSection(new Vector3(-halfFloor + halfWall, wallHeight / 2, halfFloor), Vector3.forward);
        PlaceWallSection(new Vector3(halfFloor - halfWall, wallHeight / 2, halfFloor), Vector3.forward);

        // Bottom wall
        PlaceWallSection(new Vector3(-halfFloor + halfWall, wallHeight / 2, -halfFloor), Vector3.back);
        PlaceWallSection(new Vector3(halfFloor - halfWall, wallHeight / 2, -halfFloor), Vector3.back);

        // Left wall
        PlaceWallSection(new Vector3(-halfFloor, wallHeight / 2, halfFloor - halfWall), Vector3.left);
        PlaceWallSection(new Vector3(-halfFloor, wallHeight / 2, -halfFloor + halfWall), Vector3.left);

        // Right wall
        PlaceWallSection(new Vector3(halfFloor, wallHeight / 2, halfFloor - halfWall), Vector3.right);
        PlaceWallSection(new Vector3(halfFloor, wallHeight / 2, -halfFloor + halfWall), Vector3.right);
    }

    void PlaceWallSection(Vector3 position, Vector3 direction)
    {
        // Create a wall section and orient it based on direction
        GameObject wallSection = Instantiate(wallPrefab, position, Quaternion.LookRotation(direction));
        wallSection.transform.localScale = new Vector3(wallThickness, wallHeight, wallLength);
    }
}
