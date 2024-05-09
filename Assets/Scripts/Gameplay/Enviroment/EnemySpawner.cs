using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Transform> _spawnPoints;
    [SerializeField] BiomeEnemies _enemyData;

    private void Start()
    {
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            var enemy = _enemyData.Enemies[Random.Range(0, _enemyData.Enemies.Count - 1)];
            GameObject enemyObj = Instantiate(_enemyData.Enemies[Random.Range(0, _enemyData.Enemies.Count - 1)], _spawnPoints[i].transform.position, Quaternion.identity);
        }
    }
}
