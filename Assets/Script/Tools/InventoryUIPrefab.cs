using UnityEngine;

public class InventoryUIPrefab : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] GameObject slotRoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (ItemInstance item in inventory.items)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, slotRoot.transform);
            itemSlot.GetComponent<ItemSlotScript>().SetItem(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInventory(Inventory i)
    {
        inventory = i;
    }
}
