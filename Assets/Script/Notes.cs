using UnityEngine;

public class Notes : MonoBehaviour
{
    public RectTransform notesTransform;
    public KeyCode notesType;
    private float speed;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    void Update()
    {
        Vector3 nextPosition = new Vector3(notesTransform.position.x + speed, notesTransform.position.y);
        notesTransform.SetPositionAndRotation(nextPosition, notesTransform.rotation); 
    }
}
