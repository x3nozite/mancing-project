using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotVisuals : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image border;
    [SerializeField] private Sprite noItemImage;
    [SerializeField] private TextMeshProUGUI quantityText;
    private Color32 originalColor;

    private void Awake()
    {
        originalColor = border.color;        
    }

    public void SetVisuals(ItemInstance item, bool showRankColor = false)
    {
        if (item == null)
        {
            itemIcon.sprite = noItemImage;
            //border.color = new Color32(120, 86, 32, 255);
            border.color = originalColor;
            if (quantityText != null)
            {
                quantityText.text = $"";
            }
            
            return;
        }
        itemIcon.sprite = item.item.sprite;
        border.color = showRankColor ? item.item.RankColor : originalColor; // 939393

        if (quantityText != null)
        {
            quantityText.text = (item.quantity == 1) ? "" : $"{item.quantity}";
        }
    }
}
