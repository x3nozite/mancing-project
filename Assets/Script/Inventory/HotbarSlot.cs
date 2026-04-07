using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    [SerializeField] private Sprite unfocused;
    [SerializeField] private Sprite focused;
    [SerializeField] private int Index;
    public ItemInstance item;
    public Image icon;
    public Image border;
    public void SetItem(ItemInstance i)
    {
        if (i == null)
        {
            icon.enabled = false;
            return;
        }
        icon.enabled = true;
        item = i;
        Debug.Log("icon set");
        icon.sprite = item.item.sprite;
    }

    public void SetFocused(bool isFocusedItem)
    {
        if (isFocusedItem) border.sprite = focused;
        else border.sprite = unfocused;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryHotbar hotbar = GetComponentInParent<InventoryHotbar>();
        hotbar.changeSelecteditem(Index);

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ItemSlotScript dragged = eventData.pointerDrag.GetComponent<ItemSlotScript>();
            InventoryEvents.instance.OnItemDropped?.Invoke(dragged, this);
        }

    }
}


