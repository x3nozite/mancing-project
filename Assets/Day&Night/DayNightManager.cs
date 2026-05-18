using UnityEngine;

public class DayNightManager : MonoBehaviour 
{
    [Header("Day & Night Cycle")]
    [Tooltip("Durasi 1 hari penuh dalam hitungan detik di dunia nyata")]
    public float lengthOfDayInSeconds = 120f; 
    
    [Range(0f, 1f)]
    public float currentTime = 0.25f;

    [Header("Palet Warna Waktu")]
    public Gradient dayNightGradient;

    private int globalColorID;

    void Start()
    {
        globalColorID = Shader.PropertyToID("_AmbientColor");
    }

    void Update()
    {
        currentTime += Time.deltaTime / lengthOfDayInSeconds;

        if (currentTime >= 1f)
        {
            currentTime = 0f;
        }

        Color currentAmbientColor = dayNightGradient.Evaluate(currentTime);

        Shader.SetGlobalColor(globalColorID, currentAmbientColor);
    }
}
