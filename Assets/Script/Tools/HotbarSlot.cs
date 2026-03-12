using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    [SerializeField] private Sprite unfocused;
    [SerializeField] private Sprite focused;
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
        if(isFocusedItem) border.sprite = focused;
        else border.sprite = unfocused;
    }
}


