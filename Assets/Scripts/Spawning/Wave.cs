using UnityEngine;

namespace Spawning
{
    [CreateAssetMenu(fileName = "Wave", menuName = "Spawner/Wave", order = 0)]
    public class Wave : ScriptableObject
    {
        [System.Serializable]
        public struct Spawnable
        {
            public GameObject gameObject;
            public float weight;
        }

        [SerializeField] public int numberOfEnemies = 1;
        [SerializeField] public float spawnRate = 10f;
        [SerializeField] public Spawnable[] spawnList = default;

        public float _totalSpawnWeight;

        public void OnValidate()
        {
            _totalSpawnWeight = 0f;
            foreach (var spawnable in spawnList)
                _totalSpawnWeight += spawnable.weight;
        }
    }
}