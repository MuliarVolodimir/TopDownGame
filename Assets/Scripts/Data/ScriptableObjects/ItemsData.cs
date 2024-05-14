using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData",menuName = "ScriptableObjects/ItemsDataBase")]

public class ItemsData : ScriptableObject
{
    public List<Item> Weapons;
    public List<Item> BoostItems;
}
