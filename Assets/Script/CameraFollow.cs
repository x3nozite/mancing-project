using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public float smoothIntensity = 0.3f;
    [SerializeField] public Vector3 offset = new Vector3(0, 0, -10);
    

    private Vector3 velocity = Vector3.zero;
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothIntensity);
        }
    }
}
