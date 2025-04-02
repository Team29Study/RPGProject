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
        room = new GameObject("Room") { transform = { position = Vector3.zero, parent = parent } };
        room.transform.localPosition = position;
    }

    public GameObject CreateMap(string name)
    {
        GameObject instance = new GameObject("TileMap") { transform = { position = room.transform.position, parent = room.transform } };
        instance.transform.localPosition = Vector3.zero;
        
        return instance;
    }

    public void CreateDefaultMaps()
    {
        tileMap = CreateMap("TileMap");
        wallMap = CreateMap("WallMap");
        itemMap = CreateMap("ItemMap");
    }

    public void AddWall(GameObject wall)
    {
        this.walls.Add(wall);
    }
}