using System;
using System.Collections;
using UnityEngine;

public class CastingRodGauge : MonoBehaviour
{
    [SerializeField] private RectTransform Bar;
    [SerializeField] private RectTransform Arrow;

    public float speed;
    private int direction = 1;
    private float bottomY;
    private float topY;
    public Action<float> onCastConfirmed;
    private float accuracy;

    [Header("Judgment Zones")]
    [SerializeField] private RectTransform OKZone;
    [SerializeField] private RectTransform GoodZone;
    [SerializeField] private RectTransform GreatZone;
    [SerializeField] private float JudgmentZoneOffsetY;
    [Header("Judgment Zones Sizes")]
    [SerializeField] private float OKZoneHeight;
    [SerializeField] private float GoodZoneHeight;
    [SerializeField] private float GreatZoneHeight;

    void ZoneSizing()
    {
        OKZone.sizeDelta = new Vector2(OKZone.sizeDelta.x, OKZoneHeight);
        GoodZone.sizeDelta = new Vector2(GoodZone.sizeDelta.x, GoodZoneHeight);
        GreatZone.sizeDelta = new Vector2(GreatZone.sizeDelta.x, GreatZoneHeight);
    }
    void Awake()
    {
        ZoneSizing();
        GenerateRandomJudgmentZone();
        bottomY = -Bar.rect.height  * 0.5f;
        topY = Bar.rect.height * 0.5f;
        Arrow.anchoredPosition = new Vector2(Arrow.anchoredPosition.x, bottomY);
    }

    // Update is called once per frame
    void Update()
    {
        if (Arrow.anchoredPosition.y > topY)
        {
            direction = -1;
        }
        else if(Arrow.anchoredPosition.y < bottomY)
        {
            direction = 1;
        }
        float movement = speed * Time.deltaTime * direction;
        
        Arrow.anchoredPosition = new Vector2(Arrow.anchoredPosition.x, Arrow.anchoredPosition.y + movement);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            CheckAccuracy();
            castRod();
            direction = 0;
            StartCoroutine(Wait());
        }
    }

    void CheckAccuracy()
    {
        float arrowCenter = Arrow.anchoredPosition.y - Arrow.rect.height * 0.5f;

        float GreatUpperLimit = GreatZone.anchoredPosition.y + GreatZone.rect.height *  0.5f;
        float GreatLowerLimit = GreatZone.anchoredPosition.y - GreatZone.rect.height * 0.5f;
        bool inGreat = arrowCenter >= GreatLowerLimit && arrowCenter <= GreatUpperLimit;

        float GoodUpperLimit = GoodZone.anchoredPosition.y + GoodZone.rect.height * 0.5f;
        float GoodLowerLimit = GoodZone.anchoredPosition.y - GoodZone.rect.height * 0.5f;
        bool inGood = arrowCenter >= GoodLowerLimit && arrowCenter <= GoodUpperLimit;

        float OKUpperLimit = OKZone.anchoredPosition.y + OKZone.rect.height * 0.5f;
        float OKLowerLimit = OKZone.anchoredPosition.y - OKZone.rect.height * 0.5f;
        bool inOK = arrowCenter >= OKLowerLimit && arrowCenter <= OKUpperLimit;

        if (inGreat) accuracy = 1f;
        else if (inGood) accuracy = 0.67f;
        else if (inOK) accuracy = 0.33f;
        else accuracy = 0f;
    }

    void castRod()
    {
        onCastConfirmed?.Invoke(accuracy);
    }

    void GenerateRandomJudgmentZone()
    {
        float barMiddle = JudgmentZoneOffsetY;
        float maxY = Bar.rect.height / 2f;
        float randomY = UnityEngine.Random.Range(barMiddle + GreatZone.rect.height / 2f, maxY - GreatZone.rect.height / 2f  - JudgmentZoneOffsetY);

        GreatZone.anchoredPosition = new Vector2(GreatZone.anchoredPosition.x, GreatZone.anchoredPosition.y + randomY);
        GoodZone.anchoredPosition = new Vector2(GoodZone.anchoredPosition.x, GoodZone.anchoredPosition.y + randomY);
        OKZone.anchoredPosition = new Vector2(OKZone.anchoredPosition.x, OKZone.anchoredPosition.y + randomY);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        PopUpMenuManager.Instance.CloseOverlayPopUpMenu(gameObject);
    }
}
