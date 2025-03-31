using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 머티리얼 교체하는 방식으로
public class Transpa : MonoBehaviour
{
    private Renderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        Color color = renderer.material.color;
        color.a = 0f;  // 완전 투명
        renderer.material.color = color;
    }

    // Update is called once per frame
}
