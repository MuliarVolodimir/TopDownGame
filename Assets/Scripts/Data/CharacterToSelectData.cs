using System.Collections.Generic;
using UnityEngine;

public class CharacterToSelectData : MonoBehaviour
{
    [SerializeField] List<GameCharacterSO> _gameCharacters;

    public List<GameCharacterSO> GetCharacters()
    {
        return _gameCharacters;
    }
}
