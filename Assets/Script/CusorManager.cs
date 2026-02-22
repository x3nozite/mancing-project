using UnityEngine;

public class CusorManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 mouseToWorldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f)); 
        gameObject.transform.position = mouseToWorldPosition;
    }
}
