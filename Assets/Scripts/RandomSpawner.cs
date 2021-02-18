using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] int minEnemiesToSpawn = 1;
    [SerializeField] int maxEnemiesToSpawn = 3;
    [SerializeField] GameObject[] prefabsToSpawn = default;

    int lastSpawnPointIndex = 0;

    public Transform SelectRandomSpawnPoint(Transform[] spawnPoints)
    {
        int selectedIndex = lastSpawnPointIndex;

        while (selectedIndex == lastSpawnPointIndex)
        {
            selectedIndex = Random.Range(0, spawnPoints.Length);
        }

        lastSpawnPointIndex = selectedIndex;

        return spawnPoints[selectedIndex];
    }

    public GameObject SelectRandomPrefab()
    {
        int selectedIndex = Random.Range(0, prefabsToSpawn.Length);

        return prefabsToSpawn[selectedIndex];
    }

    public int PickRandomNumberToSpawn()
    {
        int numberOfEnemiesToSpawn = Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1);
        return numberOfEnemiesToSpawn;
    }
}
