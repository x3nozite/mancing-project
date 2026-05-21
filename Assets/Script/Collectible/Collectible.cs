using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private SpriteRenderer sprite;
    private ItemInstance itemInstance;
    public int quantity = 1;
    public int level = 1;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player player = col.GetComponent<Player>();
            player.inventory.AddItem(itemInstance);
            player.inventory.OnInventoryChanged.Invoke();
            Destroy(gameObject);
        }
    }

    public void SetItem(ItemInstance item)
    {
        itemInstance = item;
        sprite.sprite = item.item.sprite;
        quantity = item.quantity;
        level = item.level;
    }
}
