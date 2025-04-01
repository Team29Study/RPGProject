using System.Collections;
using UnityEngine;

public static class MotionSystem
{
    public static IEnumerator Jump(this Transform transform, float maxHeight = 1f, float duration = 1f)
    {
        Vector3 startPos = transform.position;
        float currTime = 0f;
        
        while (currTime < duration)
        {
            float height = Mathf.Sin((currTime / duration) * Mathf.PI) * maxHeight;
            transform.position = new Vector3(startPos.x, startPos.y + height, startPos.z);
            
            currTime += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
    }
    
    public static IEnumerator RotateObject(this Transform transform, float rotationSpeed = 360f, float rotationDuration = 1f)
    {
        float currTime = 0f;
        Vector3 startRotation = transform.rotation.eulerAngles;

        // 시간에 맞춰 회전하는 동안의 회전 값 계산
        while (currTime < rotationDuration)
        {
            float rotationAmount = Mathf.Lerp(0, rotationSpeed, currTime / rotationDuration);
            transform.rotation = Quaternion.Euler(startRotation.x + rotationAmount, startRotation.y + rotationAmount, startRotation.z + rotationAmount);

            currTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(startRotation);
    }
}