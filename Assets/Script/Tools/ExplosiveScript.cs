using UnityEngine;
using System;
using System.Collections;

public class ExplosiveScript : MonoBehaviour
{
    public float Gravity = 1.0f;
    public Throwable explosive;
    public GameObject explosionPrefab;
    public SpriteRenderer spriteRenderer;
    public void ThrowExplosive()
    {
        Vector2 start = transform.position;

        float archHeight = UnityEngine.Random.Range(0.1f, 0.8f) * Gravity;
        float startY = start.y;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 target = new Vector2(mousePos.x, mousePos.y);

        float currentDistance = Vector2.Distance(start, target);

        if (currentDistance > explosive.throwDistance)
        {
            Vector2 dir = (target - start).normalized;
            target = start + (dir * explosive.throwDistance);
        }

        StartCoroutine(MoveExplosive(start, target, 1f, archHeight));
    }

    IEnumerator MoveExplosive(Vector2 start, Vector2 target, float duration, float arcHeight)
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
        //trigger explosion
        StartExplosion();
        Destroy(gameObject);
    }

    void StartExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;

        Destroy(explosion, 2f);
    }
}
