using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPosition;
    [SerializeField] private GameObject enemyPrefab;

    private void Update()
    {
        EnemySpawnLocation();
    }

    private void EnemySpawnLocation()
    {
        Transform randomSpawnPoint = spawnPosition[Random.Range(0, spawnPosition.Length)];
        Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
    }
}
