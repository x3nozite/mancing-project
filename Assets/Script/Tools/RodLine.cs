using System;
using UnityEngine;

public class RodLine : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [Header("Fishing Rod Compartments")]
    [SerializeField] private GameObject rodTip;
    [SerializeField] private GameObject hook;

    public int segments = 20;
    public float maxSag = 1f;
    
    void Awake()
    {
        lineRenderer.positionCount = segments;
    }

    void Update()
    {
        DrawRodLine();
    }
    void DrawRodLine()
    {
        Vector3 p0 = rodTip.transform.position;
        
        Vector3 p2 = hook.transform.position;
        Vector3 p1 = new Vector3(p0.x, Math.Min(p0.y, p2.y), p0.z);

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)segments;
            Vector3 point = Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;
            lineRenderer.SetPosition(i, point);
        }
    }
}
