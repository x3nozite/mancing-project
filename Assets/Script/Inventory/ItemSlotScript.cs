using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlotScript : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IDropHandler
{
    private ItemInstance item;
    private int slotIndex;
    public Inventory inventory;
    [SerializeField] private Image image;
    [SerializeField] private Image border;
    [SerializeField] private GameObject descriptionPrefab;
    [SerializeField] private Sprite noItemImage;
    private GameObject openedDescription;
    private InventoryItemDescription popup;

    public int getIndex()
    {
        return slotIndex;
    }

    public void SetItem(ItemInstance item)
    {
        this.item = item;

        if (item == null)
        {
            image.sprite = noItemImage;
            border.color = new Color32(120, 86, 32, 255);
            return;
        }
        image.sprite = item.item.sprite;
        border.color = item.item.RankColor; // 939393
    }

    public void SetIndex(int i)
    {
        slotIndex = i;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right && !openedDescription)
        {
            openedDescription = PopUpMenuManager.Instance.OpenOverlayPopUpMenu(descriptionPrefab);
            openedDescription.transform.position = transform.position;

            popup = openedDescription.GetComponent<InventoryItemDescription>();
            popup.SetDescription(item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PopUpMenuManager.Instance.CloseOverlayPopUpMenu(openedDescription);
        openedDescription = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ItemSlotScript dragged = eventData.pointerDrag.GetComponent<ItemSlotScript>();
            Debug.Log("from:" + dragged.inventory);
            Debug.Log("to:" + this.inventory);
            InventoryEvents.instance.OnItemDropped?.Invoke(dragged, this);
        }
    }
}
