using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightManager : MonoBehaviour
{
    [Header("Siklus Waktu")]
    public float lengthOfDayInSeconds = 120f; 
    [Range(0f, 1f)] public float currentTime = 0.25f; 

    [Header("Palet Warna & Cahaya Global")]
    public Gradient dayNightGradient;
    public Light2D globalLight; 

    [Header("Cahaya Player (Malam Hari)")]
    public Light2D playerLight;
    [Tooltip("Kurva untuk mengatur intensitas lampu player (0 = mati, 1 = terang)")]
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