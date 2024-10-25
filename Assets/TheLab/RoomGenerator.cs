using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject wallPrefab;  // Prefab for the walls (can use a simple Cube)
    public GameObject floorPrefab; // Prefab for the floor (can use a Plane)
    
    public float roomWidth = 10f;   // Width of the room
    public float roomLength = 10f;  // Length of the room
    public float wallHeight = 3f;   // Height of the walls
    public float holeWidth = 2f;    // Width of the hole (for a door or window)
    public float holeHeight = 2f;   // Height of the hole (for a door or window)

    void Start()
    {
        GenerateRoom();
    }

    void GenerateRoom()
    {
        // Create the floor
        GameObject floor = Instantiate(floorPrefab, Vector3.zero, Quaternion.identity);
        floor.transform.localScale = new Vector3(roomWidth / 10, 1, roomLength / 10);  // Adjust the plane size

        // Create the four walls
        CreateWallWithHole(new Vector3(0, wallHeight / 2, roomLength / 2), new Vector3(roomWidth, wallHeight, 1), true);  // Front wall with a hole
        CreateWall(new Vector3(0, wallHeight / 2, -roomLength / 2), new Vector3(roomWidth, wallHeight, 1)); // Back wall
        CreateWall(new Vector3(-roomWidth / 2, wallHeight / 2, 0), new Vector3(1, wallHeight, roomLength)); // Left wall
        CreateWall(new Vector3(roomWidth / 2, wallHeight / 2, 0), new Vector3(1, wallHeight, roomLength)); // Right wall
    }

    void CreateWall(Vector3 position, Vector3 scale)
    {
        // Instantiate the wall and set its position and size
        GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);
        wall.transform.localScale = scale;
    }

    void CreateWallWithHole(Vector3 position, Vector3 scale, bool horizontal)
    {
        // Calculate the size of each section of the wall (left/right for horizontal, top/bottom for vertical)
        float halfWallWidth = (scale.x - holeWidth) / 2;

        if (horizontal)
        {
            // Create the left section of the wall
            Vector3 leftPosition = position + new Vector3(-halfWallWidth / 2 - holeWidth / 2, 0, 0);
            Vector3 leftScale = new Vector3(halfWallWidth, scale.y, scale.z);
            CreateWall(leftPosition, leftScale);

            // Create the right section of the wall
            Vector3 rightPosition = position + new Vector3(halfWallWidth / 2 + holeWidth / 2, 0, 0);
            Vector3 rightScale = new Vector3(halfWallWidth, scale.y, scale.z);
            CreateWall(rightPosition, rightScale);
        }
        else
        {
            // Create the bottom section of the wall (if vertical wall)
            Vector3 bottomPosition = position + new Vector3(0, -halfWallWidth / 2 - holeHeight / 2, 0);
            Vector3 bottomScale = new Vector3(scale.x, halfWallWidth, scale.z);
            CreateWall(bottomPosition, bottomScale);

            // Create the top section of the wall (if vertical wall)
            Vector3 topPosition = position + new Vector3(0, halfWallWidth / 2 + holeHeight / 2, 0);
            Vector3 topScale = new Vector3(scale.x, halfWallWidth, scale.z);
            CreateWall(topPosition, topScale);
        }
    }
}
