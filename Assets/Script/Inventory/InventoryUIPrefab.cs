using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryUIPrefab : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject slotRootLeft;
    [SerializeField] private GameObject slotRootRight;

    void PopulateInventory()
    {
        int totalSlots = CalculateTotalSlots();

        for(int i=0; i < totalSlots; i++)
        {
            Transform currentParent = (i < totalSlots / 2) ? slotRootLeft.transform : slotRootRight.transform;

            GameObject itemSlot = Instantiate(itemSlotPrefab, currentParent);

            if(i < inventory.items.Count)
            {
                itemSlot.GetComponent<ItemSlotScript>().SetItem(inventory.items[i]);
            }
        }
    }
    void Start()
    {
        PopulateInventory();
    }
    public void SetInventory(Inventory i)
    {
        inventory = i;
    }

    public int CalculateTotalSlots()
    {
        GridLayoutGroup leftPageGrid = slotRootLeft.GetComponent<GridLayoutGroup>();
        RectTransform leftPageTransform = slotRootLeft.GetComponent<RectTransform>();

        int columns = Mathf.FloorToInt((leftPageTransform.rect.width) / (leftPageGrid.cellSize.x));
        int rows = Mathf.FloorToInt((leftPageTransform.rect.height) / (leftPageGrid.cellSize.y));
        return columns * rows * 2;
    }
}
