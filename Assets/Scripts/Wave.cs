using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Spawner/Wave", order = 0)]
public class Wave : ScriptableObject
{
    // Here's where we define our weighted object structure,
    // and flag it Serializable so it can be edited in the Inspector.
    [System.Serializable]
    public struct Spawnable
    {
        public GameObject gameObject;
        public float weight;
    }

    [SerializeField] public int numberOfEnemies = 1;
    [SerializeField] public float spawnRate = 10f;
    [SerializeField] public Spawnable[] spawnList = default;

    // Track the total weight used in the whole spawnables array.
    public float _totalSpawnWeight;

    // Update the total weight when the user modifies Inspector properties,
    // and on initialization at runtime.
    public void OnValidate()
    {
        _totalSpawnWeight = 0f;
        foreach (var spawnable in spawnList)
            _totalSpawnWeight += spawnable.weight;
    }
}