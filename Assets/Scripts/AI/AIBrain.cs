using Level;
using UnityEngine;

namespace AI
{
    public class AIBrain : MonoBehaviour
    {
        [SerializeField] private AIStats stats;
        [SerializeField] private GameObject nextPrefab;
        
        const string ENEMY_PARENT_NAME = "Enemies";
        GameObject enemyParent;
        
        private void Awake()
        {
            FindObjectOfType<LevelController>().AddEnemyToList(this);
            GetComponent<Collider2D>().enabled = false;
        }

        private void Start()
        {
            Initialise();
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
        
        private void Initialise()
        {
            transform.localScale = new Vector3(stats.scale, stats.scale, 1f);
            GetComponent<AIMover>().SetMoveSpeed(stats.speed);
        }

        public void ActivateCollider()
        {
            GetComponent<Collider2D>().enabled = true;
        }

        public void SpawnChildren()
        {
            switch (stats.size)
            {
                case AISize.LARGE:
                    Spawn(2);
                    break;
                case AISize.MEDIUM:
                    Spawn(4);
                    break;
                case AISize.SMALL:
                default:
                    return;
            }
        }

        private void Spawn(int numberToSpawn)
        {
            if (nextPrefab == null) { return; }
            
            for (int i = 0; i < numberToSpawn; i++)
            {
                Vector2 positionOffset = Random.insideUnitCircle.normalized * stats.scale / 2;
                Vector2 positionToSpawn = (Vector2) transform.position + positionOffset;
                GameObject spawnedObject = Instantiate(nextPrefab, positionToSpawn, Quaternion.identity) as GameObject;
                spawnedObject.transform.parent = enemyParent.transform;
            }
        }
    }
}