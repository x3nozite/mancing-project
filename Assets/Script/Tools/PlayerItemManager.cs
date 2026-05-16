using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    private ItemInstance equippedItem;

    [Header("Item Managers")]
    [SerializeField] private GameObject FishingRodManager;
    [SerializeField] private GameObject explosiveManager;
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
            explosiveManager.SetActive(false);
            return;
        }
        else if (equippedItem.item is FishingRodData)
        {
            FishingRodManager.SetActive(true);
            explosiveManager.SetActive(false);
            FishingRodScript frs = FishingRodManager.GetComponent<FishingRodScript>();
            frs.SetItem((FishingRodData)equippedItem.item);
        }
        else if(equippedItem.item is Throwable)
        {
            FishingRodManager.SetActive(false);
            explosiveManager.SetActive(true);
            ExplosiveManager em = explosiveManager.GetComponent<ExplosiveManager>();
            em.SetItem(equippedItem);
        }
    }
}
