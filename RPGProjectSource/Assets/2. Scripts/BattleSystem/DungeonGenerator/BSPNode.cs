using System.Collections.Generic;
using UnityEngine;

public class BSPNode
{
    public Vector3 position;
    public Vector3 size;
    
    public BSPNode left;
    public BSPNode right;

    public BSPNode(Vector3 position, Vector3 size)
    {
        this.position = position;
        this.size = size;
        
        left = null;
        right = null;
    }
    
    private bool IsLeaf => left == null && right == null;

    public void Split()
    {
        if (Random.value > 0.5f)
        {
            float splitHeight = Random.Range(0.3f, 0.7f);
            Vector3 leftSize = new Vector3(size.x, 0, size.z * splitHeight);
            Vector3 rightSize = new Vector3(size.x, 0, size.z * (1 - splitHeight));

            left = new BSPNode(position, leftSize);
            right = new BSPNode(new Vector3(position.x, position.y, position.z + leftSize.z), rightSize);
        }
        else
        {
            float splitWidth = Random.Range(0.3f, 0.7f);
            Vector3 leftSize = new Vector3(size.x * splitWidth, 0, size.z);
            Vector3 rightSize = new Vector3(size.x * (1 - splitWidth), 0, size.z);

            left = new BSPNode(position, leftSize);
            right = new BSPNode(new Vector3(position.x + leftSize.x, position.y, position.z), rightSize);
        }
    }

    
    // do: 각 방의 크기에 맞게 respawnArea 생성
    public void CreateCubes(Transform parent, List<BSPNode> leafNodes)
    {
        if (IsLeaf)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            cube.transform.position = new Vector3(position.x + size.x / 2, -1, position.z + size.z / 2); // 높이가 한칸 올라가는 현상 발생
            cube.transform.localScale = new Vector3(size.x * 0.8f, 0.5f, size.z * 0.8f);
            // cube.transform.parent = parent;
            
            leafNodes.Add(this);
        }

        left?.CreateCubes(parent, leafNodes);
        right?.CreateCubes(parent, leafNodes);
    }
}