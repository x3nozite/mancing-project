using UnityEngine;
using UnityEngine.EventSystems;

public class ItemContainer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject descriptionPrefab;
    private GameObject openedDescription;
    private InventoryItemDescription popup;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ItemSlotScript iss = GetComponent<ItemSlotScript>();
            if (iss.GetItem() == null) return;
            openedDescription = PopUpMenuManager.Instance.OpenOverlayPopUpMenu(descriptionPrefab);
            openedDescription.transform.position = transform.position;

            InventorySlot inventorySlot = GetComponent<InventorySlot>();
            InventoryItemDescription iid = openedDescription.GetComponent<InventoryItemDescription>();

            iid.SetDescription(iss.GetItem(), inventorySlot.inventory);
        }
    }
}
