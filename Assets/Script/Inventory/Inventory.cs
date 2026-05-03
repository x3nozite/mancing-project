using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInstance> items = new List<ItemInstance>();
    [SerializeField] private int inventorySize = 108;
    [SerializeField] private int maxItemStack;

    [SerializeField] private ItemData placeholder_common;
    [SerializeField] private ItemData placeholder_uncommon;
    [SerializeField] private ItemData placeholder_potion;
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

    void Start()
    {
        InventoryEvents.instance.OnItemDropped += HandleInventoryItemDrop;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUIInstance != null)
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
        placeholder_rod = new ItemInstance { item = placeholder_common, quantity = 1 };
        items[8] = placeholder_rod;
        items[9] = placeholder_rod;
        items[10] = placeholder_rod;
        items[11] = placeholder_rod;
        placeholder_rod = new ItemInstance { item = placeholder_uncommon, quantity = 1 };
        items[20] = placeholder_rod;
        items[13] = placeholder_rod;
        items[14] = placeholder_rod;
        items[15] = placeholder_rod;

        placeholder_rod = new ItemInstance { item = placeholder_potion, quantity = 10 };
        items[50] = placeholder_rod;
    }

    void HandleInventoryItemDrop(InventorySlot from, InventorySlot to)
    {
        if (from.inventory == to.inventory)
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

    void SplitExcessItemStack(ItemInstance item)
    {
        int maxStack = Math.Min(maxItemStack, item.item.maxStack);
        int overflow = item.quantity - maxStack;
        item.quantity -= overflow;

        if (overflow > 0)
        {
            ItemInstance overflowStack = new ItemInstance { item = item.item, level = item.level, quantity = overflow };
            int firstEmptySlot = items.IndexOf(null);
            items.Insert(firstEmptySlot, overflowStack);
        }
    }

    public void AddItem(ItemInstance incoming)
    {
        int maximumAmount = Mathf.Min(maxItemStack, incoming.item.maxStack);

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null || items[i].item != incoming.item) continue;
            int space = maximumAmount - items[i].quantity;
            if (space < 0) continue;

            int transferred = Mathf.Min(space, incoming.quantity);
            items[i].quantity += transferred;
            incoming.quantity -= transferred;

            if (incoming.quantity == 0) return;
        }

        // Item is not in the inventory yet
        while (incoming.quantity > 0)
        {
            int emptyIndex = items.IndexOf(null);
            if (emptyIndex != -1)
            {
                int transferred = Mathf.Min(maximumAmount, incoming.quantity);
                Debug.Log("Transferring: " + transferred);
                items[emptyIndex] = new ItemInstance { item = incoming.item, level = incoming.level, quantity = transferred };
                incoming.quantity -= transferred;
            }
            else
            {
                return;
            }
        }
    }       
}
