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
        DrawLine();
    }
    void DrawRodLine()
    {
        Vector3 p0 = rodTip.transform.position;
        
        Vector3 p2 = hook.transform.position;
        Vector3 p1 = new Vector3(p0.x, Mathf.Min(p0.y, p2.y), p0.z);

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)segments;
            Vector3 point = Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;
            lineRenderer.SetPosition(i, point);
        }
    }

    void DrawLine()
    {
        Vector3[] points = new Vector3[segments];

        Vector3 relativeRodPos = rodTip.transform.position - hook.transform.position;

        float xSq = relativeRodPos.x * relativeRodPos.x;

        float a = (xSq != 0) ? relativeRodPos.y / xSq : 0;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            float x = t * relativeRodPos.x;

            float y = a * (x * x);

            points[i] = new Vector3(x, y, 0) + hook.transform.position;
        }

        lineRenderer.SetPositions(points);
    }
}
