using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    private ItemInstance equippedItem;

    [Header("Item Managers")]
    [SerializeField] private GameObject FishingRodManager;
    public void SetEquippedItem(ItemInstance item) {
        if (item != null) equippedItem = item;
        else equippedItem = null;
            EnableItemManager();
    }

    void EnableItemManager()
    {
        if (equippedItem == null)
        {
            FishingRodManager.SetActive(false);
            return;
        }else if (equippedItem.item is FishingRodData)
        {
            FishingRodManager.SetActive(true);
            FishingRodScript frs = FishingRodManager.GetComponent<FishingRodScript>();
            frs.SetItem((FishingRodData)equippedItem.item);
        }
    }
}
