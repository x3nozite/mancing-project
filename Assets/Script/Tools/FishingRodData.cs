using UnityEngine;

[CreateAssetMenu(fileName = "FishingRod", menuName = "Scriptable Objects/FishingRod")]
public class FishingRodData : ItemData
{
    
    [SerializeField] private float luck;
    

    public float Luck => luck;
    public Sprite FishingRodSprite => sprite;
}
