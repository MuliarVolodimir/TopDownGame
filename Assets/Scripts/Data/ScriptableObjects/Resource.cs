using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameCharacter", menuName = "ScriptableObjects/CharacterRecource")]
public class Resource : ScriptableObject
{
    public string Name;
    public string index;
    public Image icon;
    public bool _canBeOverwritten;
    public GameObject ResourceObject;
}
