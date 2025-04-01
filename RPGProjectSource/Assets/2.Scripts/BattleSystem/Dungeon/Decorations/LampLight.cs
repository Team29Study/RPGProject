using UnityEngine;

public class LampLight : MonoBehaviour
{
    private Light lampLight;
    public AnimationCurve intensityCurve;
    public float duration = 5f;
    public float brightness = 3f;

    void Awake()
    {
        lampLight = GetComponent<Light>();
    }
    void Update()
    {
        if (lampLight && intensityCurve != null)
        {
            float timeElapsed = Time.time % duration;
            float intensity = intensityCurve.Evaluate(timeElapsed / duration);
            lampLight.intensity = intensity * brightness;
        }
    }
}
