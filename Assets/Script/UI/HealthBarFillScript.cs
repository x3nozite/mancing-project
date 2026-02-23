using System;
using UnityEngine;

public class HealthBarFillScript : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private float percent = 1;
    private float maxMiddleWidth;

    [SerializeField] private RectTransform startRect;
    [SerializeField] private RectTransform middleRect;
    [SerializeField] private RectTransform endRect;

    void Start()
    {
        maxMiddleWidth = middleRect.sizeDelta.x;
    }

    void Update()
    {
        percent = currentHealth / maxHealth;
        percent = Math.Min(percent, 1);
        setRemainingHealth();
    }

    public void setRemainingHealth()
    {
        middleRect.sizeDelta = new Vector2(maxMiddleWidth * percent, middleRect.sizeDelta.y);
        endRect.anchoredPosition = new Vector2(middleRect.anchoredPosition.x + middleRect.sizeDelta.x, endRect.anchoredPosition.y);
    }
}
