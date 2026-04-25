using UnityEngine;
using UnityEngine.UI;

public class ItemSlotVisuals : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image border;
    [SerializeField] private Sprite noItemImage;

    public void SetVisuals(ItemInstance item)
    {
        if (item == null)
        {
            image.sprite = noItemImage;
            border.color = new Color32(120, 86, 32, 255);
            return;
        }
        image.sprite = item.item.sprite;
        border.color = item.item.RankColor; // 939393
    }
}
