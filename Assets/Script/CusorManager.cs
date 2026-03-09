using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Update()
    {
        Cursor.visible = false;
        transform.position = Input.mousePosition;
    }
}
