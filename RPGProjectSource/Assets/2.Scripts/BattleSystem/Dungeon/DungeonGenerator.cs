using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class DungeonGenerator: MonoBehaviour
{
    public List<Vector2> roomSizes; // 사이즈만 설정 - 혹은 이 또한 랜덤으로
    private List<Rect> rooms = new();
    private List<Rect> settledRooms = new();
    
    private RoonGenerator roonGenerator;
    private NavMeshSurface navMeshSurface;

    private void Awake()
    {
        roonGenerator = GetComponent<RoonGenerator>();
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    // draw rail

    void Start()
    {
        rooms = Enumerable.Range(0, 24).Select(_ => new Rect(Vector2.zero, new Vector2(Random.Range(2, 10) * 4, Random.Range(2, 10) * 4))).ToList();
        // rooms = roomSizes.Select(size => new Rect(Vector2.zero, size)).ToList();
        
        int startCount = rooms.Count;
        
        while (settledRooms.Count != startCount)
        {
            for (int index = 0; index < rooms.Count; index++)
            {
                Rect room = rooms[index];
                
                // 4의 배율을 가져야 타일을 유지한다.
                // Rect paddedRoom = new(rooms[index].position - new Vector2(8, 8), rooms[index].size + new Vector2(16, 16));
                Rect paddedRoom = new(rooms[index].position - new Vector2(0, 0), rooms[index].size + new Vector2(0, 0));
        
                if (settledRooms.Any(settledRoom => paddedRoom.Overlaps(settledRoom)))
                {
        
                    float horSign = Random.value > 0.5f ? 1f : -1f;
                    float verSign = Random.value > 0.5f ? 1f : -1f;
                    
                    rooms[index] = new Rect(room.position + new Vector2(horSign * 2, verSign * 2), room.size);
                    
                    continue;
                }
        
                settledRooms.Add(room);
                rooms.RemoveAt(index);
                index--;
            }
        }
    
        foreach (var room in settledRooms) { roonGenerator.Generate(room); }
        //     navMeshSurface.BuildNavMesh();
    }
}