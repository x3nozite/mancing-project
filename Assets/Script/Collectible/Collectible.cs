using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    private ItemInstance itemInstance;

    void Start()
    {
        itemInstance = new ItemInstance { item = itemData, quantity = 1, rank = itemData.rank }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player = col.GetComponent<Player>();
            // player.collect(this);
        }
    }
}
