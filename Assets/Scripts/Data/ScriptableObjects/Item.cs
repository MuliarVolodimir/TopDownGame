using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public ItemType Type;
    public ItemRarity Rarity;
    public string Index;

    public GameObject ItemPrefab;

    public enum ItemType
    { 
        Weapon,
        BoostItem,
        EnergyAmmo,
        Other
    }

    public enum ItemRarity
    { 
        Common,
        Rare,
        Epic,
        Legendary,
        Cursed,
        Special
    }
}
