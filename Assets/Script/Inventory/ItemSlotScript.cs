using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    private ItemInstance item;
    [SerializeField] private Image image;
    [SerializeField] private Image border;
    [SerializeField] private GameObject descriptionPrefab;
    private GameObject openedDescription;
    private InventoryItemDescription popup;
    void Update()
    {
        
    }

    public void SetItem(ItemInstance item)
    {
        this.item = item;
        image.sprite = item.item.sprite;
        border.color = item.item.RankColor; // 939393
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
}
