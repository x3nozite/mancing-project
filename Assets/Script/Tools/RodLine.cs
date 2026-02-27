using UnityEngine;

public class RodLine : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [Header("Fishing Rod Compartments")]
    [SerializeField] private GameObject rodTip;
    [SerializeField] private GameObject hook;

    // Update is called once per frame
    void Update()
    {
        DrawRodLine();
    }
    void DrawRodLine()
    {
        lineRenderer.SetPosition(0, rodTip.transform.position);
        lineRenderer.SetPosition(1, hook.transform.position);
    }
}
