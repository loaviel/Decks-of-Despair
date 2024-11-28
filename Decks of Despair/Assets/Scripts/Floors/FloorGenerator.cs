using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{

    private int floorWidth;
    private int floorHeight;

    private enum room { none, possible, empty, normal, full, shop, boss}

    // Make a 2D array of floor width and floor height 
    // Customise layout based on adajency to starting room
    // Build layout until a certain number of rooms are made
    // Customize each room with a room type based on rules on that room type.

    
}
