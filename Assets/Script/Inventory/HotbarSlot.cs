using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite unfocused;
    [SerializeField] private Sprite focused;
    public int Index;
    public Image border;
    public InventoryHotbar hotbar;
    
    public void SetIndex(int i, Inventory inventory)
    {
        Index = i;
        InventorySlot inventorySlot = GetComponent<InventorySlot>();
        inventorySlot.SetIndex(i);
        inventorySlot.inventory = inventory;
    }

    public void SetSlotItem(ItemInstance item)
    {
        ItemSlotScript slot = GetComponent<ItemSlotScript>();
        slot.SetItem(item);
    }

    public void SetFocused(bool isFocusedItem)
    {
        if (isFocusedItem) border.sprite = focused;
        else border.sprite = unfocused;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        hotbar.changeSelecteditem(Index);
    }
}


