using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemies")]
public class EnemyData : ScriptableObject
{
    public List<GameObject> EasyEnemies;
    public List<GameObject> MiddleEnemies;
    public List<GameObject> HardEnemies;

    public List<GameObject> MiniBosses;

    public List<GameObject> Bosses;
}
