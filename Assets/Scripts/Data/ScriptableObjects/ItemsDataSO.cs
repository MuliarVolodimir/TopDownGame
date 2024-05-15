using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData",menuName = "ScriptableObjects/ItemsDataBase")]

public class ItemsDataSO : ScriptableObject
{
    public List<ItemSO> Weapons;
    public List<ItemSO> BoostItems;
}
