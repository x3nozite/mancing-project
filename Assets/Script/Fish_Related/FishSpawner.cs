
using UnityEngine;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour
{
    public SeaEnvironment seaEnvironment;
    [SerializeField] private GameObject collectiblePrefab;
    [System.Serializable]
    public struct RankChance
    {
        public ItemRank rank;
        [Range(0, 100)] public float chance;
    }

    [Header("Drop Settings")]
    [SerializeField] private List<RankChance> rankChances;
    [SerializeField] private List<Fish> fishPool;
    [SerializeField] private int minFishSpawn = 1;
    [SerializeField] private int maxFishSpawn = 5;

    public void SpawnSchoolOfFish(Vector3 spawnPos, float blastRadius)
    {
        int spawnCount = Random.Range(minFishSpawn, maxFishSpawn + 1);

        if (seaEnvironment.fishPopulation <= 50f)
        {
            float healthPercent = seaEnvironment.fishPopulation / seaEnvironment.maxFishPopulation;

            float randomRoll = Random.value;

            if (randomRoll > healthPercent) spawnCount = 0;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            Fish chosenFish = GetRandomFish();
            ItemInstance newFish = new ItemInstance { item = chosenFish, level = 1, quantity = 1 };

            Vector3 randomOffset = RandomizeFishSpawnOffset(blastRadius);
            GameObject fishObj = Instantiate(collectiblePrefab, spawnPos + randomOffset, Quaternion.identity);
            fishObj.GetComponent<Collectible>().SetItem(newFish);
        }
    }

    Vector3 RandomizeFishSpawnOffset(float blastRadius)
    {
        float xOffset = UnityEngine.Random.Range(-blastRadius, blastRadius);
        float yOffset = UnityEngine.Random.Range(-blastRadius, blastRadius);
        Vector3 offset = new Vector3(xOffset, yOffset, 0);
        offset = offset.normalized;
        offset *= blastRadius;

        return offset;
    }

    public Fish GetRandomFish()
    {
        if (fishPool == null || fishPool.Count == 0) return null;

        // determine rarity
        float randomRoll = Random.Range(1f, 100f);
        float currentChance = 0f;
        ItemRank selectedFishRank = ItemRank.Common;
        foreach (RankChance ranking in rankChances)
        {
            currentChance += ranking.chance;
            if (randomRoll <= currentChance)
            {
                selectedFishRank = ranking.rank;
                break;
            }
        }

        List<Fish> validFishes = fishPool.FindAll(f => f.rank == selectedFishRank);

        Debug.Log(validFishes.Count);

        Fish chosenFish = validFishes[Random.Range(0, validFishes.Count)];
        return chosenFish;
    }

}
