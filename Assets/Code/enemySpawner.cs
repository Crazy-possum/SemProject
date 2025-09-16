using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Assign your enemy prefab in the Inspector
    [SerializeField] private Transform[] spawnPoints; // Assign your spawn point GameObjects in the Inspector
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

            if (spawnPoints.Length > 0)
            {
                // Choose a random spawn point
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Transform randomSpawnPoint = spawnPoints[randomIndex];

                // Instantiate the enemy at the chosen spawn point's position and rotation
                Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
            }
            else
            {
                Debug.LogWarning("No spawn points assigned to the EnemySpawner!");
            }
        }
    }
}
