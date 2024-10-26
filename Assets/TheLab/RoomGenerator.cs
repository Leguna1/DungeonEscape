using UnityEngine;

public class DungeonGeneratorWithDoors : MonoBehaviour
{
    public GameObject floorPrefab;  // Plane prefab for the floor
    public GameObject wallPrefab;   // Cube prefab for the walls
    public float roomSize = 10f;    // Size of each room
    public float hallwayWidth = 5f; // Width of hallways
    public float wallHeight = 5f;   // Height of the walls
    public float doorWidth = 3f;    // Width of the doors
    public int rows = 3;            // Number of rows of rooms
    public int cols = 3;            // Number of columns of rooms

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        // Create the main floor that covers the entire dungeon area
        float floorWidth = cols * (roomSize + hallwayWidth) - hallwayWidth;
        float floorLength = rows * (roomSize + hallwayWidth) - hallwayWidth;
        Vector3 floorPosition = new Vector3(floorWidth / 2, 0, floorLength / 2);

        GameObject floor = Instantiate(floorPrefab, floorPosition, Quaternion.identity);
        floor.transform.localScale = new Vector3(floorWidth, 1, floorLength);

        // Loop through grid to create rooms and walls with doors
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Calculate room position
                Vector3 roomPosition = new Vector3(col * (roomSize + hallwayWidth), 0, row * (roomSize + hallwayWidth));

                // Create walls around the room and add doors
                CreateRoomWallsWithDoors(row, col, roomPosition);
            }
        }
    }

    void CreateRoomWallsWithDoors(int row, int col, Vector3 roomPosition)
    {
        // Create the 4 walls around the room and add doors at specific locations

        // Front wall (check for a door)
        Vector3 frontWallPos = roomPosition + new Vector3(0, wallHeight / 2, roomSize / 2);
        if (row < rows - 1) // Add door to the front wall if there's a room ahead
        {
            CreateWallWithDoor(frontWallPos, new Vector3(roomSize, wallHeight, 1), true);
        }
        else // No door, full wall
        {
            CreateWall(frontWallPos, new Vector3(roomSize, wallHeight, 1));
        }

        // Back wall (check for a door)
        Vector3 backWallPos = roomPosition + new Vector3(0, wallHeight / 2, -roomSize / 2);
        if (row > 0) // Add door to the back wall if there's a room behind
        {
            CreateWallWithDoor(backWallPos, new Vector3(roomSize, wallHeight, 1), true);
        }
        else // No door, full wall
        {
            CreateWall(backWallPos, new Vector3(roomSize, wallHeight, 1));
        }

        // Left wall (check for a door)
        Vector3 leftWallPos = roomPosition + new Vector3(-roomSize / 2, wallHeight / 2, 0);
        if (col > 0) // Add door to the left wall if there's a room to the left
        {
            CreateWallWithDoor(leftWallPos, new Vector3(1, wallHeight, roomSize), false);
        }
        else // No door, full wall
        {
            CreateWall(leftWallPos, new Vector3(1, wallHeight, roomSize));
        }

        // Right wall (check for a door)
        Vector3 rightWallPos = roomPosition + new Vector3(roomSize / 2, wallHeight / 2, 0);
        if (col < cols - 1) // Add door to the right wall if there's a room to the right
        {
            CreateWallWithDoor(rightWallPos, new Vector3(1, wallHeight, roomSize), false);
        }
        else // No door, full wall
        {
            CreateWall(rightWallPos, new Vector3(1, wallHeight, roomSize));
        }
    }

    void CreateWallWithDoor(Vector3 position, Vector3 scale, bool isHorizontal)
    {
        // Create two segments of a wall with a gap for the door in the middle
        float doorOffset = doorWidth / 2;

        if (isHorizontal) // Door on a horizontal wall (front/back)
        {
            // Left wall segment
            Vector3 leftSegmentPos = position + new Vector3(-doorOffset, 0, 0);
            CreateWall(leftSegmentPos, new Vector3(scale.x / 2 - doorOffset, scale.y, scale.z));

            // Right wall segment
            Vector3 rightSegmentPos = position + new Vector3(doorOffset, 0, 0);
            CreateWall(rightSegmentPos, new Vector3(scale.x / 2 - doorOffset, scale.y, scale.z));
        }
        else // Door on a vertical wall (left/right)
        {
            // Bottom wall segment
            Vector3 bottomSegmentPos = position + new Vector3(0, 0, -doorOffset);
            CreateWall(bottomSegmentPos, new Vector3(scale.x, scale.y, scale.z / 2 - doorOffset));

            // Top wall segment
            Vector3 topSegmentPos = position + new Vector3(0, 0, doorOffset);
            CreateWall(topSegmentPos, new Vector3(scale.x, scale.y, scale.z / 2 - doorOffset));
        }
    }

    void CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);
        wall.transform.localScale = scale;  // Scale the wall according to room size
    }
}
