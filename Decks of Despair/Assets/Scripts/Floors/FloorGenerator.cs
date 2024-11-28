using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{

    public int floorWidth;
    public int floorHeight;

    public int numberOfRoomsNeeded;
    private int createdRooms = 0;

    private enum Room { none, possible, empty, normal, unassigned, full, shop, boss, start}

    // Make a 2D array of floor width and floor height 
    // Add starting room in the middle of the floor each time.
    // Customise layout based on adajency to starting room
    // Build layout until a certain number of rooms are made
    // Customize each room with a room type based on rules on that room type.

    public void CreateFloor() {
        // Create a series of room objects inside of a 2D array

        Room[,] floor = new Room[floorWidth, floorHeight];

        // Gives every room on the floor a starting room value of none

        for(int i = 0; i < floorWidth; i++)
        {
            for(int j = 0; j < floorHeight; j++)
            {
                floor[i, j] = Room.none;
            }
        }

        // Access the middle of the floor to set a starting room by assuming an odd width and height with the usage of the floor function

        floor[(int)Mathf.Floor(floorWidth / 2), (int)Mathf.Floor(floorHeight / 2)] = Room.start;

        // Creates a while loop that loops until the floor is created

        while (createdRooms < numberOfRoomsNeeded)
        {
            // Loops through every room on the floor and compiles every possible room location

            // Uses a hashset to avoid duplicates in the room generation

            HashSet <int[,]> set = new HashSet <int[,]> ();

            for (int i = 0; i < floorWidth - 1; i++)
            {
                for (int j = 0; j < floorHeight - 1; j++)
                {
                    if (floor[i, j] == Room.start || floor[i, j] == Room.normal)
                    {
                        set.Add(CheckPossibleRooms(i, j, floor));
                    }
                }
            }

            foreach (int[,] location in set)
            {
                foreach (int ints in location)
                {
                    Debug.Log(ints);
                }
            }

            break;
        }

        

   }

    private int[,] CheckPossibleRooms(int roomX, int roomY, Room[,] floor)
    {
        // Check all four directions for possible room generation and creates a jagged array for these locations to be added to

       int[,] possibleRoomLocations = new int[4,4];

        // Check left room is possible for a room

        if (roomX != 0) {
            if (floor[roomX - 1, roomY] == Room.none)
            {
                possibleRoomLocations[0, 0] = roomX - 1;
                possibleRoomLocations[0, 1] = roomY;
            }
        }

        // Check right room is possible for a room

        if (roomX != 3)
        {
            if (floor[roomX + 1, roomY] == Room.none)
            {
                possibleRoomLocations[1, 0] = roomX + 1;
                possibleRoomLocations[1, 1] = roomY;
            }
        }

        // Check upward room is possible for a room

        if (roomY != 0)
        {
            if (floor[roomX, roomY - 1] == Room.none)
            {
                possibleRoomLocations[2, 0] = roomX;
                possibleRoomLocations[2, 1] = roomY - 1;
            }
        }

        // Check downward room is possible for a room

        if (roomY != 3)
        {
            if (floor[roomX, roomY + 1] == Room.none)
            { 
                possibleRoomLocations[3, 0] = roomX;
                possibleRoomLocations[3, 1] = roomY + 1;
            }
        }

        return possibleRoomLocations;

    }
}
