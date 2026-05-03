using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    private ItemInstance itemInstance;
    public int quantity = 1;
    public int level = 1;

    void Start()
    {
        itemInstance = new ItemInstance { item = itemData, quantity = 1, level = 1};
    }

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
}
