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
        border.color = item.item.RankColor; // 939393
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
