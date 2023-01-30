using UnityEngine;

namespace Spawning
{
    public class RandomSpawner : MonoBehaviour
    {
        [SerializeField] private int minEnemiesToSpawn = 1;
        [SerializeField] private int maxEnemiesToSpawn = 3;
        [SerializeField] private GameObject[] prefabsToSpawn = default;

        private int lastSpawnPointIndex = 0;

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
}
