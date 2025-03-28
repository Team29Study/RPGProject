using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RoonGenerator: MonoBehaviour
{
    public List<GameObject> titles; // 가중치 추가하기
    public Vector2 roomSize;

    public List<GameObject> walls;
    public GameObject corner;
    
    private int tileSize = 2;
    private int wallSize = 4;

    public void Generate(Rect room)
    {
        var roomObject = new GameObject("room");
        roomObject.transform.position = new Vector3(room.position.x, 0, room.position.y);
        roomObject.transform.SetParent(transform);
        
        // 타일 만들기
        for (int row = 0; row < room.size.y; row += tileSize)
        {
            for (int col = 0; col < room.size.x; col += tileSize)
            {
                var currTile = Instantiate(titles[Random.Range(0, titles.Count)], roomObject.transform);
                currTile.transform.localPosition = new Vector3(col, 0, row);
            }
        }
        
        // roomObject 참조 필요함
        // float wallThickness = 0.2f; // 벽 두께
        // Vector2 roomCenter = room.position; // 방의 중심 좌표

        // 맨 앞 설치 - 맵 사이즈가 4의 배수만 가능한 현상 발생
        for (int index = 0; index < room.size.x; index+= 4)
        {
            GameObject font = Instantiate(this.walls[Random.Range(0, walls.Count)], roomObject.transform);
            font.transform.localPosition = new Vector3(index + 1, 0, -1.5f);
            
            // 뒤쪽 벽
            GameObject back = Instantiate(this.walls[Random.Range(0, walls.Count)], roomObject.transform);
            back.transform.localPosition = new Vector3(index + 1, 0, room.size.y -0.5f); // 앵커 발생
        }
        
        //상하
        for (int index = 0; index < room.size.y; index+= 4)
        {
            GameObject font = Instantiate(this.walls[Random.Range(0, walls.Count)], roomObject.transform);
            font.transform.localPosition = new Vector3(-1.5f, 0, index + 1);
            font.transform.rotation = Quaternion.Euler(0, 90, 0);
            
            // 뒤쪽 벽
            GameObject back = Instantiate(this.walls[Random.Range(0, walls.Count)], roomObject.transform);
            back.transform.localPosition = new Vector3(room.size.x -0.5f, 0, index + 1);
            back.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        
        // 꼭지점 - 앵커 관련 문제외에는 배열로 관리
        var a = Instantiate(corner, roomObject.transform);
        a.transform.localPosition = new Vector3(-1.5f, 0, - 1.5f);
        a.name = "a";
        
        var b = Instantiate(corner, roomObject.transform);
        b.transform.localPosition = new Vector3(- 1.5f, 0, room.size.y - 0.5f);
        b.name = "b";
        b.transform.rotation = Quaternion.Euler(0, 90, 0);
        
        var c = Instantiate(corner, roomObject.transform);
        c.transform.localPosition = new Vector3(room.size.x - 0.5f, 0, - 1.5f);
        c.name = "c";
        c.transform.rotation = Quaternion.Euler(0, 270, 0);
        
        var d = Instantiate(corner, roomObject.transform);
        d.transform.localPosition = new Vector3(room.size.x - 0.5f, 0, room.size.y - 0.5f);
        d.transform.rotation = Quaternion.Euler(0, 180, 0);
        d.name = "d";
    }

    public void GenerateWall(Rect room)
    {
    

        // CreateWall(new Vector2(roomCenter.x, roomCenter.y + roomSize.y / 2), new Vector2(roomSize.x, wallThickness)); // 상단 벽
        // CreateWall(new Vector2(roomCenter.x, roomCenter.y - roomSize.y / 2), new Vector2(roomSize.x, wallThickness)); // 하단 벽
        // CreateWall(new Vector2(roomCenter.x - roomSize.x / 2, roomCenter.y), new Vector2(wallThickness, roomSize.y)); // 좌측 벽
        // CreateWall(new Vector2(roomCenter.x + roomSize.x / 2, roomCenter.y), new Vector2(wallThickness, roomSize.y)); 
    }
    
    void CreateWall(Vector2 position, Vector2 size)
    {
        // GameObject wall = Instantiate(this.walls);
        // wall.transform.localPosition = new Vector3(position.x, position.y, 0);
        // var collider = wall.AddComponent<BoxCollider2D>();
        // GetComponent<Collider>().size = new Vector2(size.x, size.y);
    }
}