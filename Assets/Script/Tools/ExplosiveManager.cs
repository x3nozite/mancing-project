using UnityEngine;

public class ExplosiveManager : MonoBehaviour
{
    [SerializeField] private ItemInstance explosive;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Player player;
    [SerializeField] private ExplosiveScript explosiveObject;

    public Inventory inventory;

    void Awake()
    {
        transform.SetParent(player.transform);
        transform.localPosition = new Vector2(0.4f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, -45f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && explosive.quantity > 0)
        {
            explosiveObject.ThrowExplosive();
            inventory.ReduceItemQuantity(explosive, 1);
        }
    }

    public void SetItem(ItemInstance item)
    {
        explosive = item;
        explosiveObject.explosive = (Throwable)item.item;
        spriteRenderer.sprite = explosive.item.sprite;
    }
}
