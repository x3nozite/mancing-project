using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour
{
    public void Launch(float force)
    {

        float randomYOffset = Random.Range(-0.2f, 0.2f);
        Vector2 start = transform.position;
        float targetX = start.x + force * 10f + 1f;

        float arcHeight = Random.Range(0.1f, 0.5f) + force;
        float startY = start.y;
        float targetY = start.y + randomYOffset - 0.5f;

        Vector2 target = new Vector2(targetX, targetY);
        StartCoroutine(MoveHook(start, target, 0.5f, arcHeight));
    }

    IEnumerator MoveHook(Vector2 start, Vector2 target, float duration, float arcHeight)
    {
        float t = 0f;
        while (t < 1f)
        {
            float castDistance = Mathf.Lerp(start.x, target.x, t);

            float arc = start.y + Mathf.Sin(t * Mathf.PI) * arcHeight;
            float newY = Mathf.Lerp(start.y + arc, target.y, t);
            transform.position = new Vector2(castDistance, newY);

            t += Time.deltaTime / duration;
            yield return null;
        }
    }
}
