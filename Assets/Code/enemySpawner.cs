using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab; // Assign your enemy prefab in the Inspector
    [SerializeField] private Transform[] _spawnPointsList; // Assign your spawn point GameObjects in the Inspector
    [SerializeField] private float spawnInterval = 3f; // Time between spawns

    void Start()
    {
        StartCoroutine(SpawnEnemiesRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        while (true) // Loop indefinitely for continuous spawning
        {
            yield return new WaitForSeconds(spawnInterval);

            if (_spawnPointsList.Length > 0)
            {
                Instantiate(_enemyPrefab, _spawnPointsList[0].position, _spawnPointsList[0].rotation);
            }
            else
            {
                Debug.LogWarning("No spawn points assigned to the EnemySpawner!");
            }
        }
    }
}
