using System.Collections.Generic;
using UnityEngine;

// 머티리얼 교체하는 방식으로, 카메라에서 레이캐스트
public class Transpa : MonoBehaviour
{
    public float transparency = 0.5f; // 0은 완전 투명, 1은 불투명

    void Start()
    {
        // MeshRenderer 가져오기
        Renderer renderer = GetComponent<Renderer>();

        if (renderer)
        {
            // 현재 머티리얼 복사
            Material material = renderer.material;

            material.SetFloat("_Mode", 3);

            Color color = material.GetColor("_Color");
            color.a = transparency; // 투명도 설정
            material.SetColor("_Color", color);

            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
        }
    }
}