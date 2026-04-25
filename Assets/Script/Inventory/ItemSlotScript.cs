using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlotScript : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IDropHandler
{
    private ItemInstance item;
    [SerializeField] private GameObject descriptionPrefab;
    private GameObject openedDescription;
    private InventoryItemDescription popup;
    [SerializeField] private ItemSlotVisuals visuals;

    public void SetItem(ItemInstance item, bool showRankColor = false)
    {
        this.item = item;

        visuals.SetVisuals(item, showRankColor);
    }

    public void SetDraggable()
    {
        DragDrop dd = GetComponent<DragDrop>();
        if (item == null)
        {
            dd.enabled = false;
        }
        else dd.enabled = true;

    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (eventData.button == PointerEventData.InputButton.Right && !openedDescription)
    //    {
    //        openedDescription = PopUpMenuManager.Instance.OpenOverlayPopUpMenu(descriptionPrefab);
    //        openedDescription.transform.position = transform.position;

    //        popup = openedDescription.GetComponent<InventoryItemDescription>();
    //        popup.SetDescription(item);
    //    }
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    PopUpMenuManager.Instance.CloseOverlayPopUpMenu(openedDescription);
    //    openedDescription = null;
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        return;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        return;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            InventorySlot dragged = eventData.pointerDrag.GetComponent<InventorySlot>();
            InventorySlot to = GetComponent<InventorySlot>();
            InventoryEvents.instance.OnItemDropped?.Invoke(dragged, to);
        }
    }
}
