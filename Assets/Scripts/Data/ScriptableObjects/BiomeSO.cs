using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "ScriptableObjects/Enviroment")]
public class BiomeSO : ScriptableObject
{
    public string Name;
    public string Index;

    [Header("Room prefabs")]
    public List<GameObject> _mainRoomPrefabs;
    public List<GameObject> _normalRoomPrefabs;
    public List<GameObject> _shopRoomPrefabs;
    public List<GameObject> _bossRoomPrefabs;
}
