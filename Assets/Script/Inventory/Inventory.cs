using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInstance> items = new List<ItemInstance>();
    public ItemInstance[] hotbarItems = new ItemInstance[8];
    public int inventorySize = 100;

    [SerializeField] private ItemData placeholder_common;
    [SerializeField] private ItemData placeholder_uncommon;
    private ItemInstance placeholder_rod;

    [SerializeField] private GameObject inventoryPrefab;
    private GameObject inventoryUIInstance;

    public Action OnInventoryChanged;


    void Awake()
    {
        placeholder_rod = new ItemInstance { item = placeholder_common};
        populate_placeholder();
        placeholder_rod = new ItemInstance { item = placeholder_uncommon };
        populate_placeholder();

        hotbar_items_placeholder();
    }

    void Start() {
        InventoryEvents.instance.OnItemDropped += HandleInventoryItemDrop;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryUIInstance != null)
            {
                return;
            }
            inventoryUIInstance = PopUpMenuManager.Instance.OpenPrimaryPopUpMenu(inventoryPrefab);
            InventoryUIPrefab UIInventory = inventoryUIInstance.GetComponent<InventoryUIPrefab>();
            UIInventory.SetInventory(this);

            for (int i = 4; i < inventorySize; i++)
            {
                items.Add(null);
            }
        }
    }

    public void ResetUIInstance()
    {
        inventoryUIInstance = null;
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

    void HandleInventoryItemDrop(ItemSlotScript from, ItemSlotScript to)
    {
        if(from.inventory == to.inventory)
        {
            Swap(from, to);
        }

        OnInventoryChanged?.Invoke();
    }

    void Swap(ItemSlotScript from, ItemSlotScript to)
    {
        ItemInstance temp = items[from.getIndex()];
        items[from.getIndex()] = items[to.getIndex()];
        items[to.getIndex()] = temp;

        Debug.Log("swapped");

    }
}
