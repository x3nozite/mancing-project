using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightManager : MonoBehaviour
{
    [Header("Time cycle")]
    public float lengthOfDayInSeconds = 120f; 
    [Range(0f, 1f)] public float currentTime = 0.25f; 

    [Header("Color & global light palette")]
    public Gradient dayNightGradient;
    public Light2D globalLight; 

    [Header("Player Light")]
    public Light2D playerLight;
    [Tooltip("Curve for light intesity")]
    public AnimationCurve playerLightIntensityCurve;

    void Update()
    {
        currentTime += Time.deltaTime / lengthOfDayInSeconds;
        if (currentTime >= 1f)
        {
            currentTime = 0f;
        }

        Color currentAmbientColor = dayNightGradient.Evaluate(currentTime);
        if (globalLight != null)
        {
            globalLight.color = currentAmbientColor;
        }

        if (playerLight != null && playerLightIntensityCurve != null)
        {
            playerLight.intensity = playerLightIntensityCurve.Evaluate(currentTime);
        }
    }
}