
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomGenerator: MonoBehaviour
{ 
    public GameObject EnemyRespawnArea;
    
    public List<GameObject> tileList; // 가중치 추가하기
    public List<GameObject> wallList;
    public List<GameObject> items;
    public List<GameObject> stairs; // 시작과 나가는 다리

    private int tileSize = 2;
    private float wallGap = 0.5f;
    public List<Room> rooms = new();
    
    public Room StartRoom { get; private set; }
    public Room BossRoom { get; private set; }
    
    public void Generate(Rect room, Room.RoomType roomType, Action clearCallback)
    {
        var currRoom = new Room(new Vector3(room.position.x, 0, room.position.y), transform);
        currRoom.CreateDefaultMaps();
        
        // 타일 만들기
        for (int row = 0; row < room.size.y; row += tileSize)
        {
            for (int col = 0; col < room.size.x; col += tileSize)
            {
                Instantiate(tileList[Random.Range(0, tileList.Count)], currRoom.room.transform.position, Quaternion.identity, currRoom.tileMap.transform).transform.localPosition = new Vector3(col, 0, row);
            }
        }
        
        // 벽 생성
        GameObject carrWall;
        // // 맨 앞 설치 - 맵 사이즈가 4의 배수만 가능한 현상 발생
        for (int index = 0; index < room.size.x; index+= 4)
        {
            carrWall = Instantiate(wallList[Random.Range(0, wallList.Count)], Vector3.zero, Quaternion.identity, currRoom.wallMap.transform);
            carrWall.transform.localPosition = new Vector3(index, 0, - wallGap);
            currRoom.AddWall(carrWall);
            
            carrWall = Instantiate(wallList[Random.Range(0, wallList.Count)], Vector3.zero, Quaternion.identity, currRoom.wallMap.transform);
            carrWall.transform.localPosition = new Vector3(index, 0, room.size.y - 1 + wallGap);
            currRoom.AddWall(carrWall);
        
        }
        for (int index = 0; index < room.size.y; index+= 4)
        {
            carrWall = Instantiate(wallList[Random.Range(0, wallList.Count)], Vector3.zero, Quaternion.Euler(0, 90, 0), currRoom.wallMap.transform);
            carrWall.transform.localPosition = new Vector3(-wallGap, 0, index + 4);
            currRoom.AddWall(carrWall);
        
            carrWall = Instantiate(wallList[Random.Range(0, wallList.Count)], Vector3.zero, Quaternion.Euler(0, 90, 0), currRoom.wallMap.transform);
            carrWall.transform.localPosition = new Vector3(room.size.x - 1 + wallGap, 0, index + 4);
            currRoom.AddWall(carrWall);
        }
        
        if (roomType == Room.RoomType.Normal)
        {
            // 아이템 생성
            Enumerable.Range(0, 4).ToList().ForEach(_ =>
            {
                var item = Instantiate(items[Random.Range(0, items.Count)], Vector3.zero, Quaternion.identity, currRoom.itemMap.transform);
                item.transform.localPosition = new Vector3(Random.Range(1, room.size.x - 1), 0, Random.Range(1, room.size.y - 1));
            });
            
            // 적 생성
            var enemyArea = Instantiate(EnemyRespawnArea, Vector3.zero, Quaternion.identity, currRoom.room.transform);
            enemyArea.transform.localPosition = new Vector3(room.size.x / 2, 0 ,room.size.y / 2);

            EnemyRespawnArea respawnArea = enemyArea.GetComponent<EnemyRespawnArea>();
            respawnArea.Set(Mathf.FloorToInt(room.size.x * room.size.y / 100), new Vector3(room.size.x, 1, room.size.y), new Vector3(room.size.x - 2, 1, room.size.y - 2));
            respawnArea.onClearWave += clearCallback;
        }

        if (roomType == Room.RoomType.StartPoint)
        {
            StartRoom = currRoom;
            Instantiate(stairs[0], transform).transform.localPosition = new Vector3(room.size.x / 2, 0, room.size.y / 2);
        }

        if (roomType == Room.RoomType.Boss)
        {
            BossRoom = currRoom;
        }
        
        rooms.Add(currRoom);
    }
}