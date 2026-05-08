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
            openedDescription = PopUpMenuManager.Instance.OpenOverlayPopUpMenu(descriptionPrefab);
            openedDescription.transform.position = transform.position;

            ItemSlotScript iss = GetComponent<ItemSlotScript>();
            InventorySlot inventorySlot = GetComponent<InventorySlot>();
            InventoryItemDescription iid = openedDescription.GetComponent<InventoryItemDescription>();

            iid.SetDescription(iss.GetItem(), inventorySlot.inventory);
        }
    }
}
