using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/BiomeEnemies")]
public class BiomeEnemiesSO : ScriptableObject
{
    public List<GameObject> Enemies;

    public List<GameObject> MiniBosses;

    public List<GameObject> Bosses;
}
