using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryUIPrefab : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject slotRootLeft;
    [SerializeField] private GameObject slotRootRight;
    private List<ItemSlotScript> slots = new List<ItemSlotScript>();
    [SerializeField] private int hotbarLength = 8;

    void PopulateInventory()
    {
        int totalSlots = CalculateTotalSlots();

        for (int i = 0; i < totalSlots; i++)
        {
            Transform currentParent = (i < totalSlots / 2) ? slotRootLeft.transform : slotRootRight.transform;

            GameObject itemSlot = Instantiate(itemSlotPrefab, currentParent);
            InventorySlot IS = itemSlot.GetComponentInChildren<InventorySlot>();
            ItemSlotScript ISS = itemSlot.GetComponentInChildren<ItemSlotScript>();

            IS.inventory = inventory;
            IS.SetIndex(i+ hotbarLength);

            slots.Add(ISS);

            if (i + hotbarLength < inventory.items.Count && inventory.items[i + hotbarLength] != null)
            {
                ISS.SetItem(inventory.items[i + hotbarLength], true);
            }
            else
            {
                ISS.SetItem(null);
            }
            ISS.SetDraggable();
        }
    }

    public void RefreshInventory()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            ItemSlotScript iss = slots[i];
            InventorySlot inventorySlot = iss.GetComponent<InventorySlot>();

            inventorySlot.inventory = inventory;
            inventorySlot.SetIndex(i + hotbarLength);

            if (i + hotbarLength < inventory.items.Count && inventory.items[i + hotbarLength] != null)
            {
                iss.SetItem(inventory.items[i + hotbarLength], true);
            }
            else
            {
                iss.SetItem(null);
            }
            iss.SetDraggable();
        }
    }

    public void SetInventory(Inventory i)
    {
        inventory = i;

        inventory.OnInventoryChanged += RefreshInventory;

        PopulateInventory();
    }

    public int CalculateTotalSlots()
    {
        GridLayoutGroup leftPageGrid = slotRootLeft.GetComponent<GridLayoutGroup>();
        RectTransform leftPageTransform = slotRootLeft.GetComponent<RectTransform>();

        int columns = Mathf.FloorToInt((leftPageTransform.rect.width) / (leftPageGrid.cellSize.x));
        int rows = Mathf.FloorToInt((leftPageTransform.rect.height) / (leftPageGrid.cellSize.y));
        return columns * rows * 2;
    }

    private void OnDestroy()
    {
        if (inventory != null)
        {
            inventory.OnInventoryChanged -= RefreshInventory;
            inventory.ResetUIInstance();
        }
    }
}
