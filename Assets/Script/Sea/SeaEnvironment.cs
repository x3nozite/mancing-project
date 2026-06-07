using UnityEngine;

public class SeaEnvironment : MonoBehaviour
{
    [Header("Environment Attributes")]
    public float maxFishPopulation = 100f;
    public float fishPopulation = 100f;

    [Header("Environment Healing")]
    [SerializeField] private float healAmount;
    [SerializeField] private float healInterval;
    [SerializeField] private float healTimer;

    public void decreaseFishPopulation(float amount)
    {
        fishPopulation = Mathf.Max(0, fishPopulation - amount);
    }

    private void Start()
    {
        healTimer = healInterval;
    }

    void Update()
    {
        if(fishPopulation < maxFishPopulation)
        {
            healTimer -= Time.deltaTime;

            if(healTimer <= 0f)
            {
                Heal();
            }
        }
    }

    private void Heal()
    {
        fishPopulation = Mathf.Min(fishPopulation + healAmount, maxFishPopulation);

        healTimer = healInterval;
    }
}
