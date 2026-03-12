using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInstance> items = new List<ItemInstance>();
    public ItemInstance[] hotbarItems = new ItemInstance[8];
    [SerializeField] private ItemData placeholder_common;
    [SerializeField] private ItemData placeholder_uncommon;
    private ItemInstance placeholder_rod;
    [SerializeField] GameObject inventoryPrefab;
    private GameObject inventoryUIInstance;
    void Awake()
    {
        placeholder_rod = new ItemInstance { item = placeholder_common};
        populate_placeholder();
        placeholder_rod = new ItemInstance { item = placeholder_uncommon };
        populate_placeholder();

        hotbar_items_placeholder();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUIInstance = PopUpMenuManager.Instance.OpenPrimaryPopUpMenu(inventoryPrefab);
            InventoryUIPrefab UIInventory = inventoryUIInstance.GetComponent<InventoryUIPrefab>();
            UIInventory.SetInventory(this);

        }
    }

    void populate_placeholder()
    {
        items.Add(placeholder_rod);
        items.Add(placeholder_rod);
        items.Add(placeholder_rod);
        items.Add(placeholder_rod);
    }

    void hotbar_items_placeholder()
    {
        hotbarItems[0] = items[0];
        hotbarItems[1] = items[5];
        hotbarItems[4] = items[2];
    }
}
