
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RoomGenerator: MonoBehaviour
{ 
    public List<GameObject> tileList; // 가중치 추가하기
    public List<GameObject> wallList;
    
    public List<GameObject> items;
    
    private int tileSize = 2;

    public List<Room> rooms = new();
    
    private float wallGap = 0.5f;
    
    
    public void Generate(Rect room)
    {

        var currRoom = new Room();
        currRoom.room = new GameObject("Room") { transform = { position = new Vector3(room.position.x, 0, room.position.y), parent = transform } };
        
        // 타일 만들기
        for (int row = 0; row < room.size.y; row += tileSize)
        {
            for (int col = 0; col < room.size.x; col += tileSize)
            {
                Instantiate(tileList[Random.Range(0, tileList.Count)], currRoom.room.transform).transform.localPosition = new Vector3(col, 0, row);
            }
        }
        
        GameObject carrWall;
        // // 맨 앞 설치 - 맵 사이즈가 4의 배수만 가능한 현상 발생
        for (int index = 0; index < room.size.x; index+= 4)
        {
            carrWall = Instantiate(wallList[Random.Range(0, wallList.Count)], Vector3.zero, Quaternion.identity, currRoom.room.transform);
            carrWall.transform.localPosition = new Vector3(index, 0, - wallGap);
            currRoom.AddWall(carrWall);
            
            carrWall = Instantiate(wallList[Random.Range(0, wallList.Count)], Vector3.zero, Quaternion.identity, currRoom.room.transform);
            carrWall.transform.localPosition = new Vector3(index, 0, room.size.y - 1 + wallGap);
            currRoom.AddWall(carrWall);
        
        }
        for (int index = 0; index < room.size.y; index+= 4)
        {
            carrWall = Instantiate(wallList[Random.Range(0, wallList.Count)], Vector3.zero, Quaternion.Euler(0, 90, 0), currRoom.room.transform);
            carrWall.transform.localPosition = new Vector3(-wallGap, 0, index + 4);
            currRoom.AddWall(carrWall);
        
            carrWall = Instantiate(wallList[Random.Range(0, wallList.Count)], Vector3.zero, Quaternion.Euler(0, 90, 0), currRoom.room.transform);
            carrWall.transform.localPosition = new Vector3(room.size.x - 1 + wallGap, 0, index + 4);
            currRoom.AddWall(carrWall);
        }
        
        rooms.Add(currRoom);
        // // 아이템 생성
        Enumerable.Range(0, 4).ToList().ForEach(_ => Instantiate(items[Random.Range(0, items.Count)], Vector3.zero, Quaternion.identity, currRoom.room.transform).transform.localPosition = new Vector3(Random.Range(1, room.size.x - 1), 0, Random.Range(1, room.size.y - 1)));
    }
}