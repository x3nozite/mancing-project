using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInstance> items = new List<ItemInstance>();
    [SerializeField] private int inventorySize = 108;

    [SerializeField] private ItemData placeholder_common;
    [SerializeField] private ItemData placeholder_uncommon;
    private ItemInstance placeholder_rod;

    [SerializeField] private GameObject inventoryPrefab;
    private GameObject inventoryUIInstance;

    public Action OnInventoryChanged;


    void Awake()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            items.Add(null);
        }
            

        placeholderPopulate();
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

    void placeholderPopulate()
    {
        placeholder_rod = new ItemInstance { item = placeholder_common };
        items[8] = placeholder_rod;
        items[9] = placeholder_rod;
        items[10] = placeholder_rod;
        items[11] = placeholder_rod;
        placeholder_rod = new ItemInstance { item = placeholder_uncommon };
        items[20] = placeholder_rod;
        items[13] = placeholder_rod;
        items[14] = placeholder_rod;
        items[15] = placeholder_rod;
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
