using UnityEngine;

public class CubeClipper: MonoBehaviour
{
    public Vector3 cubeSize = new(10, 1, 10);
    public float cutPosition = 3f;
    
    void Start()
    {
        CutCube();
    }

    void CutCube()
    {
        // 첫 번째 부분 (3x1x10)
        Vector3 partOneSize = new Vector3(cutPosition, cubeSize.y, cubeSize.z);
        GameObject partOne = CreateCube(partOneSize, new Vector3(cutPosition / 2, 0, 0)); // 가운데 위치

        // 두 번째 부분 (7x1x10)
        Vector3 partTwoSize = new Vector3(cubeSize.x - cutPosition, cubeSize.y, cubeSize.z);
        GameObject partTwo = CreateCube(partTwoSize, new Vector3(cutPosition + (cubeSize.x - cutPosition) / 2, 0, 0)); // 가운데 위치
    }
    
    GameObject CreateCube(Vector3 size, Vector3 position)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = size;
        cube.transform.position = position;
        return cube;
    }
}