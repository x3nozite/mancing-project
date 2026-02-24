using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFillScript : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private float percent = 1;
    private float maxMiddleWidth;

    [Header("Health")]
    [SerializeField] private RectTransform startRect;
    [SerializeField] private RectTransform middleRect;
    [SerializeField] private RectTransform endRect;

    [Header("EXP")]
    [SerializeField] private Image expCircle;
    [SerializeField] private float currentEXP;
    [SerializeField] private float EXPUntilNextLevel;

    void Start()
    {
        maxMiddleWidth = middleRect.sizeDelta.x;
    }

    void Update()
    {
        percent = currentHealth / maxHealth;
        percent = Math.Min(percent, 1);
        setRemainingHealth();
        setEXPCircle();
    }

    public void setRemainingHealth()
    {
        middleRect.sizeDelta = new Vector2(maxMiddleWidth * percent, middleRect.sizeDelta.y);
        endRect.anchoredPosition = new Vector2(middleRect.anchoredPosition.x + middleRect.sizeDelta.x, endRect.anchoredPosition.y);
    }

    public void setEXPCircle()
    {
        expCircle.fillAmount = currentEXP / EXPUntilNextLevel;
    }
}
