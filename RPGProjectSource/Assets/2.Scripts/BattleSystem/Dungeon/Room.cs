using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public GameObject room;
    public List<GameObject> walls = new();

    public void AddWall(GameObject wall)
    {
        this.walls.Add(wall);
    }
}