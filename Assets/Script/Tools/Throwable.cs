using UnityEngine;

[CreateAssetMenu(fileName = "Throwable", menuName = "Scriptable Objects/Throwable")]
public class Throwable : ItemData
{
    public float blastRadius;
    public float throwDistance;
    public float speed;
    public float environmentDamage;
}
