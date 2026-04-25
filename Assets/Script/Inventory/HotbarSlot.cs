using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    [SerializeField] private Sprite unfocused;
    [SerializeField] private Sprite focused;
    [SerializeField] private int Index;
    private int itemIndex;
    public Image icon;
    public Image border;
    public InventoryHotbar hotbar;

    public int GetItemIndex()
    {
        Debug.Log("item index: " + itemIndex);
        return itemIndex;
    }

    public void SetItem(int i, Inventory inventory)
    {
        if (i == -1)
        {
            icon.enabled = false;
            return;
        }
        icon.enabled = true;
        itemIndex = i;
        icon.sprite = inventory.items[i].item.sprite;
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

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ItemSlotScript dragged = eventData.pointerDrag.GetComponent<ItemSlotScript>();
            //InventoryEvents.instance.OnItemDropped?.Invoke(dragged, this);
        }
    }
}


