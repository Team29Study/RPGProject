using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

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

    void Start()
    {
        roomSizes.ForEach(size => rooms.Add(new Rect(Vector2.zero, size)));
        int startCount = rooms.Count;

        while (settledRooms.Count != startCount)
        {
            for (int index = 0; index < rooms.Count; index++)
            {
                Rect room = rooms[index];

                if (settledRooms.Any(settledRoom => room.Overlaps(settledRoom)))
                {

                    float horSign = Random.value > 0.5f ? 1f : -1f;
                    float verSign = Random.value > 0.5f ? 1f : -1f;
                    
                    rooms[index] = new Rect(room.position + new Vector2(horSign * Random.Range(20, 28), verSign * Random.Range(20, 28)), room.size);
                    continue;
                }

                settledRooms.Add(room);
                rooms.RemoveAt(index);
                index--;
            }
        }


        foreach (var room in settledRooms)
        {
            roonGenerator.Generate(room);
            // rect 사이즈를 고려해서 문 생성
            // roonGenerator.GenerateWall(room);
        }
        navMeshSurface.BuildNavMesh();
    }
}