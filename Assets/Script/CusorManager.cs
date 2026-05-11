using UnityEngine;

public class CursorManager : MonoBehaviour
{
  void Start()
  {
    Cursor.visible = false;
  }

  void Update()
  {
    Vector3 mousePos = Input.mousePosition;
    mousePos.z = 10f;
    transform.position = Input.mousePosition;
  }
}
