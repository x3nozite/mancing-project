using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FishingRodScript : MonoBehaviour
{
    [SerializeField] private FishingRodData fishingRod;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Player player;

    public Canvas worldCanvas;

    public GameObject castingGauge;
    private GameObject currentGauge;
    private CastingRodGauge gauge;

    [Header("Fishing Rod Compartments")]
    [SerializeField] private GameObject rodTip;
    [SerializeField] private GameObject hook;

    [Header("Minigames")]
    [SerializeField] private GameObject IdleMinigame;
    [SerializeField] private GameObject ReelingMinigame;
    private GameObject currentMinigame;

    public FishingState state = FishingState.Idle;
    public float duration;
    private float currentTime;

    private Hook hookScript;
    void Awake()
    {
        spriteRenderer.sprite = fishingRod.RodOnlySprite;
        transform.SetParent(player.transform);
        transform.localPosition = new Vector2(0.4f, 0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentGauge == null && state == FishingState.Idle)
        {
            currentGauge = PopUpMenuManager.Instance.OpenOverlayPopUpMenu(castingGauge, worldCanvas);
            gauge = currentGauge.GetComponent<CastingRodGauge>();
            gauge.onCastConfirmed += HandleCastConfirmed;
            state = FishingState.Casting;
        }

        // TEMPORARY. ONLY FOR TESTING
        if (state == FishingState.Waiting)
        {
            if(currentTime < duration)
            {
                currentTime += Time.deltaTime;
                if(currentTime >= duration)
                {
                    StopWaitingMinigame();
                    StartReelingMinigame();
                    currentTime = 0f;
                }
            }
        }

        if( state != FishingState.Idle)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PopUpMenuManager.Instance.CloseOverlayPopUpMenu(currentMinigame);
                state = FishingState.Idle;
                ResetRod();
            }
        }
    }

    public void SetItem(FishingRodData item)
    {
        fishingRod = item;
        spriteRenderer.sprite = fishingRod.RodOnlySprite;
    }

    void HandleCastConfirmed(float accuracy)
    {
        CastRod(accuracy);
        gauge.onCastConfirmed -= HandleCastConfirmed;
        currentGauge = null;
    }

    void CastRod(float accuracy)
    {
        
        hook.transform.SetParent(null);
        hook.transform.position = rodTip.transform.position;
        hookScript = hook.GetComponentInChildren<Hook>();
        hookScript.spriteRenderer.enabled = true;
        hookScript.Launch(accuracy, OnHookCastFinished);
    }

    void ResetRod()
    {
        hook.transform.SetParent(gameObject.transform);
        hook.transform.position = rodTip.transform.position;
        hookScript = hook.GetComponentInChildren<Hook>();
        hookScript.spriteRenderer.enabled = false;
    }

    void OnHookCastFinished()
    {
        if (state != FishingState.Casting) return;

        duration = UnityEngine.Random.Range(5f, 10f);
        StartWaitingMinigame();
    }

    void StartWaitingMinigame()
    {
        state = FishingState.Waiting;

        currentMinigame = PopUpMenuManager.Instance.OpenOverlayPopUpMenu(IdleMinigame, player.transform);
        currentMinigame.transform.localPosition = new Vector3(0, 0.5f, 0);

        IdleFishingMinigame idleMinigame = currentMinigame.GetComponent<IdleFishingMinigame>();
        idleMinigame.fishingRod = this;
    }

    void StopWaitingMinigame()
    {
        PopUpMenuManager.Instance.CloseOverlayPopUpMenu(currentMinigame);
    }

    void StartReelingMinigame()
    {
        state = FishingState.Reeling;
        currentMinigame = PopUpMenuManager.Instance.OpenOverlayPopUpMenu(ReelingMinigame, player.transform);
        currentMinigame.transform.localPosition = new Vector3(0, 1.5f, 0);

        Reeling reelingMinigame = currentMinigame.GetComponent<Reeling>();
        reelingMinigame.OnProgressChanged += hookScript.UpdateHookPosition;

        hookScript.OnFishingFinished += FishingFinished;
    }

    void StopReelingMinigame()
    {
        Reeling reelingMinigame = currentMinigame.GetComponent<Reeling>();
        reelingMinigame.OnProgressChanged -= hookScript.UpdateHookPosition;
        hookScript.OnFishingFinished -= FishingFinished;

        PopUpMenuManager.Instance.CloseOverlayPopUpMenu(currentMinigame);
    }

    void FishingFinished(bool success)
    {
        if (success)
        {
            Debug.Log("success");
        }
        else
        {
            Debug.Log("fail");
        }

        StopReelingMinigame();
        state = FishingState.Idle;
        ResetRod();
    }
}

public enum FishingState
{
    Idle,
    Casting,
    Waiting,
    Reeling
}
