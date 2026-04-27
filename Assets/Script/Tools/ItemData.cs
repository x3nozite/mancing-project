using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemData : ScriptableObject
{
  public string itemName;
  public string description;
  public int maxStack;
  public ItemRank rank;
  public Sprite sprite;
  public GameObject uiPrefab;

  public Color32 RankColor
  {
    get
    {
      switch (rank)
      {
        case ItemRank.Common: return new Color32(147, 147, 147, 255);
        case ItemRank.Uncommon: return new Color32(89, 178, 31, 255);
        case ItemRank.Rare: return new Color32(40, 126, 224, 255);
        default: return new Color32(255, 255, 255, 255);
      }
    }
  }
}

public enum ItemRank
{
  Common,
  Uncommon,
  Rare,
  Mythical
}
