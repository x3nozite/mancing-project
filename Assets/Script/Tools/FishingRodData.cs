using UnityEngine;

[CreateAssetMenu(fileName = "FishingRod", menuName = "Scriptable Objects/FishingRod")]
public class FishingRodData : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private float luck;
    [SerializeField] private Sprite sprite;

    public float Luck => luck;
    public Sprite FishingRodSprite => sprite;
}
