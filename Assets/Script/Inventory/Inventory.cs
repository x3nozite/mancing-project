using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInstance> items = new List<ItemInstance>();
    [SerializeField] private int inventorySize = 100;

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

    void HandleInventoryItemDrop(InventorySlot from, InventorySlot to)
    {
        if(from.inventory == to.inventory)
        {
            Swap(from, to);
        }

        OnInventoryChanged?.Invoke();
    }

    void Swap(InventorySlot from, InventorySlot to)
    {
        ItemInstance temp = items[from.getIndex()];
        items[from.getIndex()] = items[to.getIndex()];
        items[to.getIndex()] = temp;
    }
}
