using System.Collections.Generic;
using UnityEngine;

public class CharacterToSelectData : MonoBehaviour
{
    [SerializeField] List<GameCharacter> _gameCharacters;

    public List<GameCharacter> GetCharacters()
    {
        return _gameCharacters;
    }
}
