using System.Collections;
using System.Collections.Generic;
using AI;
using Combat;
using Spawning;
using UnityEngine;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private float startDelay = 1f;
        [SerializeField] private SpawnManager spawnManager;

        private readonly List<AIBrain> enemiesInScene = new List<AIBrain>();

        private void Start()
        {
            StartCoroutine(LateStart());
        }

        private IEnumerator LateStart()
        {
            yield return new WaitForSeconds(startDelay);
            StartCoroutine(spawnManager.StartSpawnProcedure());
        }

        public void AddEnemyToList(AIBrain enemy)
        {
            enemiesInScene.Add(enemy);
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

        public void DisableAllEnemyHealth()
        {
            foreach (AIBrain enemy in enemiesInScene)
            {
                enemy.GetComponent<Health>().isInvulnerable = true;
            }
        }
    }
}