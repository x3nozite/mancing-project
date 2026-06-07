using System;
using System.Collections;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public GameObject rodTip;
    public float Gravity = 1f;
    public Action onCastFinished;

    [SerializeField] private float maxDistance;
    [SerializeField] private float maxDeviationRadius;
    public SpriteRenderer spriteRenderer;

    private Vector2 startPoint;
    private Vector2 failPoint;
    private Vector2 successPoint;

    public Action<bool> OnFishingFinished;
    public void Launch(float accuracy, Action hookFinish)
    {
        onCastFinished = hookFinish;
        Vector2 start = transform.position;

        float arcHeight = UnityEngine.Random.Range(0.1f, 0.5f) * Gravity;
        float startY = start.y;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 target = new Vector2(mousePos.x, mousePos.y);

        float currentDistance = Vector2.Distance(start, target);

        if (currentDistance > maxDistance)
        {
            Vector2 dir = (target - start).normalized;
            target = start + (dir * maxDistance);
        }

        float errorRange = (1f - accuracy) * maxDeviationRadius;

        Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * errorRange;
        target += randomOffset;

        SetMovementBoundaries(start, target);

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
        onCastFinished?.Invoke();
    }

    void SetMovementBoundaries(Vector2 start, Vector2 target)
    {
        startPoint = target;
        successPoint = start;

        Vector2 directionToFish = (target - start).normalized;
        float distanceToFish = Vector2.Distance(start, target);

        failPoint = target + (directionToFish * distanceToFish);
    }

    public void UpdateHookPosition(float progress)
    {
        transform.position = Vector2.Lerp(failPoint, successPoint, progress);
        CheckHookPosition(progress);
    }

    void CheckHookPosition(float progress)
    {
        // unsuccessful
        if(progress <= 0)
        {
            OnFishingFinished.Invoke(false);
        }
        else if(progress >= 1)
        {
            OnFishingFinished.Invoke(true);
        }
    }
}
