using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CoinSpawn : MonoBehaviour
{
    public static CoinSpawn instance;

    [SerializeField] private Transform[] coinSpawnPosition;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int coinInScene;

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion
    }

    private void Update()
    {
        CoinSpawner();
    }

    public void CoinSpawner()
    {
        Transform spawnPoint;

        int randoSpawn = Random.Range(0, coinSpawnPosition.Count());
        spawnPoint = coinSpawnPosition[randoSpawn];

        if (coinInScene <= 2)
        {
            CoinAdd();
            Instantiate(coinPrefab, new Vector2(spawnPoint.position.x, spawnPoint.position.y), Quaternion.identity);
        }

    }

    public void CoinAdd()
    {
        coinInScene++;
    }

    public void CoinTake()
    {
        coinInScene--;
    }

}
