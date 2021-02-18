using UnityEngine;
using System.Collections.Generic;

public class LevelController : MonoBehaviour
{
    [SerializeField] int maxTrapsActive = 2;
    [SerializeField] float trapSwitchCD = 10f;
    [SerializeField] int maxEnemiesInScene = 50;
    [SerializeField] float spawnCD = 5f;

    List<Trap> trapsInScene = new List<Trap>();
    List<AIBrain> enemiesInScene = new List<AIBrain>();
    int numberOfEnemiesKilled = 0;

    SpawnManager spawnManager;
    float trapTimer = 0;
    float spawnTimer = 0;

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void Start()
    {
        SwitchTraps();
    }

    private void Update()
    {
        Tick();
    }

    public void AddEnemyToList(AIBrain enemy)
    {
        enemiesInScene.Add(enemy);
    }

    public void AddTrapToList(Trap trap)
    {
        trapsInScene.Add(trap);
    }

    public void RemoveEnemyFromList(AIBrain enemy)
    {
        enemiesInScene.Remove(enemy);

        if (enemiesInScene.Count == 0 && spawnManager != null)
        {
            StartCoroutine(spawnManager.StartSpawnProcedure());
        }
    }

    public List<AIBrain> GetAllEnemies()
    {
        return enemiesInScene;
    }

    public void IncrementEnemiesKilled()
    {
        numberOfEnemiesKilled += 1;
    }

    private void Tick()
    {
        trapTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        if (trapTimer > trapSwitchCD)
        {
            SwitchTraps();
            trapTimer = 0f;
        }

        if (spawnTimer > spawnCD && enemiesInScene.Count <= maxEnemiesInScene)
        {
            StartCoroutine(spawnManager.StartSpawnProcedure());
            spawnTimer = 0f;
        }
    }

    private void SwitchTraps()
    {
        int newTrapActive = 0;

        while (newTrapActive < maxTrapsActive)
        {
            foreach (Trap trap in trapsInScene)
            {
                if (trap.gameObject.activeSelf)
                {
                    trap.gameObject.SetActive(false);
                }
                else if (Random.Range(0, 2) > 0)
                {
                    trap.gameObject.SetActive(true);
                    newTrapActive++;
                }
            }
        }
    }
}