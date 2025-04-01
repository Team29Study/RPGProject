using System.Collections;
using UnityEngine;

public class DecoObject : MonoBehaviour
{
    public float Durability;

    public Coroutine transition;
    private Vector3 targetScale = new(0.9f, 0.9f, 0.9f);
    private float duration = 0.2f;
    
    IEnumerator ScaleUpAndDown()
    {
        Vector3 originalScale = transform.localScale;
        
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;

        timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HitBox>())
        {
            Durability -= 10;
            transition = StartCoroutine(ScaleUpAndDown());

            if (Durability <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
