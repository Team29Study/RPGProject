using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class BSPGenerator : MonoBehaviour
{
    public Vector3 mapSize = new(100, 0, 100);
    private BSPNode rootNode;
    public int depth;
    
    private List<BSPNode> leafNodes = new();
    
    private NavMeshSurface navMeshSurface;

    void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    void Start()
    {
        rootNode = new BSPNode(Vector3.zero, mapSize); 
        GenerateBSP(rootNode, depth);
        rootNode.CreateCubes(transform, leafNodes);
        CreatePaths();
        navMeshSurface.BuildNavMesh();
    }

    void GenerateBSP(BSPNode node, int depth)
    {
        if (depth <= 0) return;
        
        node.Split();

        if (node.left != null) GenerateBSP(node.left, depth - 1);
        if (node.right != null) GenerateBSP(node.right, depth - 1);
    }

    public void CreatePaths()
    {
        for (int index = 0; index < leafNodes.Count - 1; index++)
        {
            BSPNode currNode = leafNodes[index];
            BSPNode nextNode = leafNodes[index + 1];

            Vector3 currCenter = new(currNode.position.x + currNode.size.x / 2, -1, currNode.position.z + currNode.size.z / 2);
            Vector3 nextCenter = new(nextNode.position.x + nextNode.size.x / 2, -1, nextNode.position.z + nextNode.size.z / 2);
            
            Vector3 bridgePos = (currCenter + nextCenter) / 2;

            Vector3 bridgeSize;
            if (Mathf.Approximately(currCenter.z, nextCenter.z)) { bridgeSize= new(Mathf.Abs(currCenter.x - nextCenter.x), 1f ,1f); }
            else { bridgeSize = new(1f, 1f, Mathf.Abs(currCenter.z - nextCenter.z)); }
                
            GameObject bridge = GameObject.CreatePrimitive(PrimitiveType.Cube);
            bridge.name = "bridge";
            
            bridge.transform.position = bridgePos;
            bridge.transform.localScale = bridgeSize;
            
        }
    }
}