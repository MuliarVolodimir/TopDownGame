using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameCharacter", menuName = "ScriptableObjects/GameCharacter")]
public class GameCharacter : ScriptableObject
{
    public string CharacterName;
    public string CharacterDescription;
    public string CharacterIndex;

    public CharacterRarity Rarity;

    public Image CharacterIcon;  
    public GameObject CharacterPrefab;

    public enum CharacterRarity
    { 
        StatrCharacter,
        Rare,
        Epic,
        Mythic,
        Legendary
    }
}
