
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

public class DungeonGenerator: MonoBehaviour
{
    public List<Vector2> roomSizes; // 사이즈만 설정 - 혹은 이 또한 랜덤으로
    public GameObject door;
    
    private List<Rect> currentRoomSize = new();
    private List<Rect> selectedRoomSized = new();
    
    private RoomGenerator roomGenerator;
    private NavMeshSurface navMeshSurface;

    private void Awake()
    {
        roomGenerator = GetComponent<RoomGenerator>();
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    void Start()
    {
        currentRoomSize = Enumerable.Range(0, 10).Select(_ => new Rect(Vector2.zero, new Vector2(Random.Range(4, 8) * 4, Random.Range(4, 8) * 4))).ToList();
        // rooms = roomSizes.Select(size => new Rect(Vector2.zero, size)).ToList();
        
        int startCount = currentRoomSize.Count;
        
        while (selectedRoomSized.Count != startCount)
        {
            for (int index = 0; index < currentRoomSize.Count; index++)
            {
                Rect room = currentRoomSize[index];
                
                // 4의 배율을 가져야 타일을 유지한다.
                // Rect paddedRoom = new(currentRoomSize[index].position + new Vector2(1, 1), currentRoomSize[index].size - new Vector2(2, 2));
        
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
    
        foreach (var room in selectedRoomSized) { roomGenerator.Generate(room); }

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

        navMeshSurface.BuildNavMesh();  // 네이비메시 다시 생성

    }
}