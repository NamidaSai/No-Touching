using UnityEngine;

namespace Spawning
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] Wave[] waves = default;

        int currentWaveIndex = 0;

        Wave currentWave;

        private void Awake()
        {
            foreach (Wave wave in waves)
            {
                wave.OnValidate();
            }
            currentWave = waves[currentWaveIndex];
        }

        public bool HasMoreWaves()
        {
            return currentWaveIndex < waves.Length - 1;
        }

        public void StartNextWave()
        {
            currentWaveIndex++;
            currentWave = waves[currentWaveIndex];
        }

        public GameObject SelectPrefabToSpawn()
        {
            // Generate a random position in the list.
            float pick = Random.value * currentWave._totalSpawnWeight;
            int chosenIndex = 0;
            float cumulativeWeight = currentWave.spawnList[0].weight;

            // Step through the list until we've accumulated more weight than this.
            // The length check is for safety in case rounding errors accumulate.
            while (pick > cumulativeWeight && chosenIndex < currentWave.spawnList.Length - 1)
            {
                chosenIndex++;
                cumulativeWeight += currentWave.spawnList[chosenIndex].weight;
            }

            return currentWave.spawnList[chosenIndex].gameObject;
        }

        public int GetNumberOfEnemiesInWave()
        {
            return currentWave.numberOfEnemies;
        }

        public int GetCurrentWaveIndex()
        {
            return currentWaveIndex;
        }
    }
}