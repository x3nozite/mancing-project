using System.Collections;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public GameObject rodTip;
    public float Gravity = 1f;
    public void Launch(float force)
    {
        Vector2 start = transform.position;

        float arcHeight = Random.Range(0.1f, 0.5f) * Gravity;
        float startY = start.y;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 target = new Vector2(mousePos.x, mousePos.y);


        StartCoroutine(MoveHook(start, target, 1f, arcHeight));
    }

    IEnumerator MoveHook(Vector2 start, Vector2 target, float duration, float arcHeight)
    {
        float t = 0f;
        while (t < duration)
        {
            float castDistance = Mathf.Lerp(start.x, target.x, t);
            float newY = Mathf.Lerp(start.y, target.y, t) + Mathf.Sin(t * Mathf.PI) * arcHeight;
            transform.position = new Vector2(castDistance, newY);

            t += Time.deltaTime / duration;
            yield return null;
        }
    }
}
