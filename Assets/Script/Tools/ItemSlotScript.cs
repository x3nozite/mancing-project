using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ItemInstance item;
    [SerializeField] private Image image;
    [SerializeField] private Image border;
    [SerializeField] private GameObject descriptionPrefab;
    private GameObject openedDescription;
    void Update()
    {
        
    }

    public void SetItem(ItemInstance item)
    {
        this.item = item;
        image.sprite = item.item.sprite;
        if (item.item.rank == 1) border.color = new Color32(147, 147, 147, 255); // 939393
        else if (item.item.rank == 2) border.color = new Color32(156, 255, 124, 255); // 9CFF7C
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        openedDescription = PopUpMenuManager.Instance.OpenOverlayPopUpMenu(descriptionPrefab);
        openedDescription.transform.position = transform.position;

        openedDescription.GetComponent<InventoryItemDescription>().SetDescription(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PopUpMenuManager.Instance.CloseOverlayPopUpMenu(openedDescription);
    }
}
