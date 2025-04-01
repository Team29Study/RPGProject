using System.Collections.Generic;
using UnityEngine;

public class Room
{
    
    public GameObject room;

    public GameObject tileMap;
    public GameObject wallMap;
    public GameObject itemMap;
    
    public List<GameObject> walls = new();
    public enum RoomType { StartPoint, Corridor, Normal, Boss}

    public Room(Vector3 position, Transform parent)
    {
        room = new GameObject("Room") { transform = { position = position, parent = parent } };
    }

    public void build()
    {
        tileMap = new GameObject("TileMap") { transform = { position = room.transform.position, parent = room.transform } };
        tileMap.transform.localPosition = Vector3.zero;
        
        wallMap = new("WallMap");
        wallMap.transform.SetParent(room.transform);
        wallMap.transform.localPosition = Vector3.zero;
        
        itemMap = new("ItemMap");
        itemMap.transform.SetParent(room.transform);
        itemMap.transform.localPosition = Vector3.zero;
    }

    public void AddWall(GameObject wall)
    {
        this.walls.Add(wall);
    }
}