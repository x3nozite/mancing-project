using UnityEngine;
using static UnityEditor.Progress;

public class ExplosiveManager : MonoBehaviour
{
    [SerializeField] private ItemInstance explosive;
    [SerializeField] private Player player;
    [SerializeField] private GameObject explosivePrefab;
    private ExplosiveScript explosiveObject;
    [SerializeField] private SpriteRenderer ownSpriteRenderer;
    [SerializeField] private FishSpawner fishSpawner;

    public Inventory inventory;

    void Awake()
    {
        transform.SetParent(player.transform);
        transform.localPosition = new Vector2(0.0f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, -45f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && explosive.quantity > 0)
        {
            GameObject newExplosive = Instantiate(explosivePrefab, player.transform);
            explosiveObject = newExplosive.GetComponent<ExplosiveScript>();
            explosiveObject.explosive = (Throwable)explosive.item;
            explosiveObject.spriteRenderer.sprite = explosive.item.sprite;
            explosiveObject.fishSpawner = fishSpawner;

            explosiveObject.ThrowExplosive();
            inventory.ReduceItemQuantity(explosive, 1);
        }
    }

    public void SetItem(ItemInstance item)
    {
        explosive = item;
        ownSpriteRenderer.sprite = explosive.item.sprite;
    }
}
