using UnityEngine;

public class FishingRodScript : MonoBehaviour
{
    [SerializeField] private FishingRodData fishingRod;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Awake()
    {
        spriteRenderer.sprite = fishingRod.FishingRodSprite;
        transform.SetParent(player.transform);
        transform.localPosition = new Vector2(0.4f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, -45f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
