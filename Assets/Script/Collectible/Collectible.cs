using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private SpriteRenderer sprite;
    private ItemInstance itemInstance;
    public int quantity = 1;
    public int level = 1;
    [Header("Hover Settings")]
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float amplitude = 0.25f;
    //[SerializeField] private float rotateSpeed = 50.0f;

    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float newY = startPos.y + (Mathf.Sin(Time.time * floatSpeed) * amplitude);
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        //transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger) return;
        if (col.CompareTag("Player") || col.CompareTag("Ship"))
        {
            Player player = col.GetComponentInChildren<Player>();
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
