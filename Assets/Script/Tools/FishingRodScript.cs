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
    private StartFishing gauge;

    [Header("Fishing Rod Compartments")]
    [SerializeField] private GameObject rodTip;
    [SerializeField] private GameObject hook;

    void Awake()
    {
        spriteRenderer.sprite = fishingRod.FishingRodSprite;
        transform.SetParent(player.transform);
        transform.localPosition = new Vector2(0.4f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F) && currentGauge == null)
        {
            currentGauge = PopUpMenuManager.Instance.OpenOverlayPopUpMenu(castingGauge, worldCanvas);
            gauge = currentGauge.GetComponent<StartFishing>();
            gauge.onCastConfirmed += HandleCastConfirmed;
        }
    }

    void HandleCastConfirmed(float force)
    {
        // TODO cast the rod
        CastRod(force);

        // cleanup
        gauge.onCastConfirmed -= HandleCastConfirmed;
        
        currentGauge = null;
    }

    void CastRod(float force)
    {
        Debug.Log("rod casted" + force);

        hook.transform.SetParent(null);
        hook.transform.position = rodTip.transform.position;
        Hook hookScript = hook.GetComponentInChildren<Hook>();
        hookScript.Launch(force);
    }


}
