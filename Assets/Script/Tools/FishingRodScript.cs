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
    private GameObject currentMinigame;

    public FishingState state = FishingState.Idle;

    void Awake()
    {
        spriteRenderer.sprite = fishingRod.FishingRodSprite;
        transform.SetParent(player.transform);
        transform.localPosition = new Vector2(0.4f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        
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
        if(state == FishingState.Waiting)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PopUpMenuManager.Instance.CloseOverlayPopUpMenu(currentMinigame);
                state = FishingState.Idle;
                ResetRod();
            }
        }
    }

    void HandleCastConfirmed(float accuracy)
    {
        CastRod(accuracy);
        gauge.onCastConfirmed -= HandleCastConfirmed; 
        currentGauge = null;
    }

    void CastRod(float force)
    {
        hook.transform.SetParent(null);
        hook.transform.position = rodTip.transform.position;
        Hook hookScript = hook.GetComponentInChildren<Hook>();
        hookScript.Launch(force, OnHookCastFinished);
    }

    void ResetRod()
    {
        hook.transform.SetParent(gameObject.transform);
        hook.transform.position = rodTip.transform.position;
    }

    void OnHookCastFinished()
    {
        if (state != FishingState.Casting) return;

        state = FishingState.Waiting;
        StartWaitingMinigame();
    }

    void StartWaitingMinigame()
    {
        currentMinigame = PopUpMenuManager.Instance.OpenOverlayPopUpMenu(IdleMinigame);
    }
}

public enum FishingState
{
    Idle,
    Casting,
    Waiting,
    Reeling
}
