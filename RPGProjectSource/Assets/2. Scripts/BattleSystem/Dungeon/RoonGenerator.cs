
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoonGenerator: MonoBehaviour
{
    public List<GameObject> titles; // 가중치 추가하기
    public Vector2 roomSize;

    public List<GameObject> walls;
    public GameObject corner;

    public List<GameObject> doors;
    
    public List<GameObject> items;
    
    private int tileSize = 2;
    private int wallSize = 4;
    
    // 장애물 갯수(정확히 2의 배수로 배치됨)
    
    void Start()
    {
        // 2의 배수로만 가능
        // DrawRail("r", 16);
        // DrawRail("u", 16);
    }

    // 방향은 상하좌우
    public void DrawRail(string direction, int length, Vector2 startPos = default)
{
    if (direction == "r") // Right
    {
        for (int row = 0; row < 2; row++)
        {
            for (int col = 0; col < length; col++)
            {
                var originLocalPosition = Instantiate(titles[Random.Range(0, titles.Count)], transform).transform.localPosition = 
                    new Vector3(startPos.x + col * 2, 0, startPos.y + row * 2);

                if (col % 2 == 0)
                {
                    Instantiate(walls[0], transform).transform.localPosition = originLocalPosition + 
                        new Vector3(1, 0, row % 2 == 0 ? -1.5f : 1.5f);
                }
            }
        }
    }
    
    if (direction == "l") // Left
    {
        for (int row = 0; row < 2; row++)
        {
            for (int col = 0; col < length; col++)
            {
                var originLocalPosition = Instantiate(titles[Random.Range(0, titles.Count)], transform).transform.localPosition = 
                    new Vector3(startPos.x - col * 2, 0, startPos.y + row * 2);

                if (col % 2 == 0)
                {
                    Instantiate(walls[0], transform).transform.localPosition = originLocalPosition + 
                        new Vector3(-1, 0, row % 2 == 0 ? -1.5f : 1.5f);
                }
            }
        }
    }
    
    if (direction == "u") // Up
    {
        for (int row = 0; row < length; row++)
        {
            for (int col = 0; col < 2; col++)
            {
                var originLocalPosition = Instantiate(titles[Random.Range(0, titles.Count)], transform).transform.localPosition = 
                    new Vector3(startPos.x + col * 2, 0, startPos.y + row * 2);

                if (row % 2 == 0)
                {
                    Instantiate(walls[0], transform).transform.localPosition = originLocalPosition + 
                        new Vector3(col % 2 == 0 ? -1.5f : 1.5f, 0, 1);
                }
            }
        }
    }

    if (direction == "d") // Down
    {
        for (int row = 0; row < length; row++)
        {
            for (int col = 0; col < 2; col++)
            {
                var originLocalPosition = Instantiate(titles[Random.Range(0, titles.Count)], transform).transform.localPosition = 
                    new Vector3(startPos.x + col * 2, 0, startPos.y - row * 2);

                if (row % 2 == 0)
                {
                    Instantiate(walls[0], transform).transform.localPosition = originLocalPosition + 
                        new Vector3(col % 2 == 0 ? -1.5f : 1.5f, 0, -1);
                }
            }
        }
    }
}

    public void Generate(Rect room)
    {
        
        var roomObject = new GameObject("Room") { transform = { position = new Vector3(room.position.x, 0, room.position.y), parent = transform } };
        
        // 타일 만들기
        for (int row = 0; row < room.size.y; row += tileSize)
        {
            for (int col = 0; col < room.size.x; col += tileSize)
            {
                Instantiate(titles[Random.Range(0, titles.Count)], roomObject.transform).transform.localPosition = new Vector3(col, 0, row);
            }
        }
        

        // 벽설치 2군데 정도는 출입 문(하나 추가)
        // walls = new List<GameObject>();
        // walls.Add(new GameObject("door"));
        // for (int index = 0; index < (room.size.x / 4) - 1; index++) { walls.Add(walls[Random.Range(0, walls.Count)]); }

        // 맨 앞 설치 - 맵 사이즈가 4의 배수만 가능한 현상 발생
        for (int index = 0; index < room.size.x; index+= 4)
        {
            Instantiate(walls[Random.Range(0, walls.Count)], Vector3.zero, Quaternion.identity, roomObject.transform).transform.localPosition = new Vector3(index + 1, 0, -1.5f);
            Instantiate(walls[Random.Range(0, walls.Count)], Vector3.zero, Quaternion.identity, roomObject.transform).transform.localPosition = new Vector3(index + 1, 0, room.size.y -0.5f);
        }
        for (int index = 0; index < room.size.y; index+= 4)
        {
            Instantiate(walls[Random.Range(0, walls.Count)], Vector3.zero, Quaternion.Euler(0, 90, 0), roomObject.transform).transform.localPosition = new Vector3(-1.5f, 0, index + 1);
            Instantiate(walls[Random.Range(0, walls.Count)], Vector3.zero, Quaternion.Euler(0, 90, 0), roomObject.transform).transform.localPosition = new Vector3(room.size.x -0.5f, 0, index + 1);
        }


        // edge로 인한 현상 수정 필요
        // List<(int, Vector2)> edges = new()
        // {
        //     new(0, new(-1.5f, -1.5f)),
        //     new(90, new(-1.5f, room.size.y - 0.5f)),
        //     new(180, new(room.size.x - 0.5f, room.size.y - 0.5f)),
        //     new(270, new(room.size.x - 0.5f, -1.5f)),
        // };
        //
        // edges.ForEach(edge => Instantiate(corner, Vector3.zero, Quaternion.Euler(0, edge.Item1, 0), roomObject.transform).transform.localPosition = new Vector3(edge.Item2.x, 0, edge.Item2.y));
        
        // // 아이템 생성
        // Enumerable.Range(0, 10).ToList().ForEach(_ => Instantiate(items[Random.Range(0, items.Count)], new Vector3(Random.Range(1, room.size.x - 1), 0, Random.Range(1, room.size.y - 1)), Quaternion.identity, roomObject.transform));
    }
}