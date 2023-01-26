using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] bool randomSpawning = false;
    [SerializeField] float spawnDelay = 0.2f;
    [SerializeField] Transform[] spawnPoints = default;

    RandomSpawner randomSpawner;
    WaveSpawner waveSpawner;

    GameObject enemyParent;
    const string ENEMY_PARENT_NAME = "Enemies";

    private void Awake()
    {
        randomSpawner = GetComponent<RandomSpawner>();
        waveSpawner = GetComponent<WaveSpawner>();
    }

    private void Start()
    {
        CreateEnemyParent();
    }

    private void CreateEnemyParent()
    {
        enemyParent = GameObject.Find(ENEMY_PARENT_NAME);
        if (!enemyParent)
        {
            enemyParent = new GameObject(ENEMY_PARENT_NAME);
        }
    }

    public IEnumerator StartSpawnProcedure()
    {
        for (int spawnCount = 0; spawnCount < PickNumberOfEnemiesToSpawn(); spawnCount++)
        {
            SpawnNewEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }

        ProcessEndOfWave();
    }

    private void ProcessEndOfWave()
    {
        if (!randomSpawning && waveSpawner.HasMoreWaves())
        {
            waveSpawner.StartNextWave();
        }
        else
        {
            randomSpawning = true;
        }
    }

    private void SpawnNewEnemy()
    {
        GameObject selectedPrefab = null;
        Transform selectedSpawnPoint = spawnPoints[0];

        if (randomSpawning)
        {
            selectedPrefab = randomSpawner.SelectRandomPrefab();
            selectedSpawnPoint = randomSpawner.SelectRandomSpawnPoint(spawnPoints);
        }
        else
        {
            selectedPrefab = waveSpawner.SelectPrefabToSpawn();
            selectedSpawnPoint = randomSpawner.SelectRandomSpawnPoint(spawnPoints);
        }

        SpawnAtLocation(selectedPrefab, selectedSpawnPoint);
    }

    private void SpawnAtLocation(GameObject objectToSpawn, Transform spawnLocation)
    {
        if (objectToSpawn == null) { Debug.LogWarning("Missing prefab to spawn."); }
        if (spawnLocation == null) { Debug.LogWarning("Missing location to spawn."); }

        GameObject spawnedObject = Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation) as GameObject;
        spawnedObject.transform.parent = enemyParent.transform;
    }

    private int PickNumberOfEnemiesToSpawn()
    {
        int numberOfEnemiesToSpawn = 0;

        if (randomSpawning)
        {
            numberOfEnemiesToSpawn = randomSpawner.PickRandomNumberToSpawn();
        }
        else
        {
            numberOfEnemiesToSpawn = waveSpawner.GetNumberOfEnemiesInWave();
        }

        return numberOfEnemiesToSpawn;
    }
}