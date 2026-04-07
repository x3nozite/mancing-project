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

    void PopulateInventory()
    {
        int totalSlots = CalculateTotalSlots();

        for (int i = 0; i < totalSlots; i++)
        {
            Transform currentParent = (i < totalSlots / 2) ? slotRootLeft.transform : slotRootRight.transform;

            GameObject itemSlot = Instantiate(itemSlotPrefab, currentParent);
            ItemSlotScript iss = itemSlot.GetComponent<ItemSlotScript>();
            iss.inventory = inventory;
            iss.SetIndex(i);

            slots.Add(iss);

            if (i < inventory.items.Count && inventory.items[i] != null)
            {
                iss.SetItem(inventory.items[i]);
            }
            else
            {
                iss.SetItem(null);
            }
            iss.SetDraggable();
        }
    }

    public void RefreshInventory()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            ItemSlotScript iss = slots[i];

            iss.inventory = inventory;
            iss.SetIndex(i);

            if (i < inventory.items.Count && inventory.items[i] != null)
            {
                iss.SetItem(inventory.items[i]);
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
