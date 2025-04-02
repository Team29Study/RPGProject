
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

// 시작 지점 알아야 함
public class DungeonGenerator: MonoBehaviour
{
    public List<Vector2> roomSizes; // 사이즈만 설정 - 혹은 이 또한 랜덤으로
    public GameObject door;
    
    private List<Rect> currentRoomSize = new();
    private List<Rect> selectedRoomSized = new();
    
    private RoomGenerator roomGenerator;
    private NavMeshSurface navMeshSurface;

    [Range(2, 10)] public int roomLength;
    public int currClearRoomLength = 0;

    public Vector3 startPosition;
    
    public void OnClearRoom()
    {
        currClearRoomLength += 1;
        if (currClearRoomLength >= roomLength - 1)
        {
            var player = GameObject.FindWithTag("Player");
            Instantiate(roomGenerator.stairs[0], player.transform.position + Vector3.forward * 2, Quaternion.identity);
        }
    }
    // 해당 클래스의 역할
    public void GenerateDungeon()
    {
        int startCount = currentRoomSize.Count;
        
        while (selectedRoomSized.Count != startCount)
        {
            for (int index = 0; index < currentRoomSize.Count; index++)
            {
                Rect room = currentRoomSize[index];
                
                // Rect paddedRoom = new(currentRoomSize[index].position + new Vector2(1, 1), currentRoomSize[index].size - new Vector2(2, 2));// 4의 배율을 가져야 타일을 유지한다.
                if (selectedRoomSized.Any(settledRoom => room.Overlaps(settledRoom)))
                {
        
                    float horSign = Random.value > 0.5f ? 1f : -1f;
                    float verSign = Random.value > 0.5f ? 1f : -1f;
                    
                    currentRoomSize[index] = new Rect(room.position + new Vector2(horSign * 4f, verSign * 4f), room.size); 
                    
                    continue;
                }
        
                selectedRoomSized.Add(room);
                currentRoomSize.RemoveAt(index);
                index--;
            }
        }
    }
    

    private void Awake()
    {
        roomGenerator = GetComponent<RoomGenerator>();
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    void Start()
    {
        currClearRoomLength = 0;
        // rooms = roomSizes.Select(size => new Rect(Vector2.zero, size)).ToList(); // 인스펙터에서 등록하고 싶은 경우

        currentRoomSize = Enumerable.Range(0, roomLength).Select(_ => new Rect(Vector2.zero, new Vector2(Random.Range(4, 8) * 4, Random.Range(4, 8) * 4))).ToList();
        // currentRoomSize.Add(new Rect(Vector2.zero, new Vector2(40, 40))); // 보스룸
        
        GenerateDungeon();
        
        // 플레이어 시작 위치 배정
        startPosition = new Vector3(selectedRoomSized[0].size.y / 2 , 0, selectedRoomSized[0].size.x / 2);
        // GameObject player = GameObject.FindWithTag("Player");
        // if (player) player.transform.position = startPosition;

        foreach (var room in selectedRoomSized)
        {
            Room.RoomType currRoomType;
            int index = selectedRoomSized.IndexOf(room);
            
            if(index == 0) currRoomType = Room.RoomType.StartPoint;
            // else if (index == selectedRoomSized.Count - 1) currRoomType = Room.RoomType.Boss;
            else currRoomType = Room.RoomType.Normal;
            
            roomGenerator.Generate(room, currRoomType, OnClearRoom);
        }

        // 각 방에게 인접된 방들을 정보를 알려준 뒤 플레이어가 있을 때 해당 맵 active
        // 문 연결 - 룸 제너레이터의 역할
        var rooms = roomGenerator.rooms;
        
        for (int index = 0; index < rooms.Count - 1; index++)
        {
            for (int nextIndex = index + 1; nextIndex < rooms.Count; nextIndex++)
            {
                var currOverlapWalls = rooms[index].walls.Where(wall => rooms[nextIndex].walls.Any(otherWall =>  wall.transform.position == otherWall.transform.position));
                var nextOverlapWalls = rooms[nextIndex].walls.Where(wall => rooms[index].walls.Any(otherWall => wall.transform.position == otherWall.transform.position));

                foreach (GameObject overlapWall in currOverlapWalls) Destroy(overlapWall);

                if (!nextOverlapWalls.Any()) continue;

                List<GameObject> overlapWallsList = nextOverlapWalls.ToList();
                GameObject randomWall = overlapWallsList[Random.Range(0, overlapWallsList.Count)];
        
                Instantiate(door, randomWall.transform.position, randomWall.transform.rotation, rooms[nextIndex].room.transform);  // 문 생성
                Destroy(randomWall);  // 문으로 바꿀 벽 삭제
            }
        }

        navMeshSurface.BuildNavMesh();
    }
}