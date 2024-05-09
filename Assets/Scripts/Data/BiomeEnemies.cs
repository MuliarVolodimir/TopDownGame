using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/BiomeEnemies")]
public class BiomeEnemies : ScriptableObject
{
    public List<GameObject> Enemies;

    public List<GameObject> MiniBosses;

    public List<GameObject> Bosses;
}
