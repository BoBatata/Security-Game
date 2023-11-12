using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoints;
    [SerializeField] private List<EnemyBehavior> activeEnemys;
    [SerializeField] private List<EnemyBehavior> pooledEnemys;

    private void Awake()
    {
        foreach (EnemyBehavior enemy in pooledEnemys)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    private void PoolEnemy()
    {
        EnemyBehavior enemy;

        if (pooledEnemys.Count() < 1)
        {
            //Instantiate
        }
        else
        {
            int randoTrunk = Random.Range(0, pooledEnemys.Count());
            enemy = pooledEnemys[randoTrunk];
            pooledEnemys.Remove(enemy);
        }

        enemy.gameObject.SetActive(true);
        activeEnemys.Add(enemy);

    }


}
