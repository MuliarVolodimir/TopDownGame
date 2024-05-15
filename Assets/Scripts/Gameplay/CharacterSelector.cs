using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class CharacterSelector : MonoBehaviour
{
    private const int GAME_SCENE_INDEX = 3;

    [SerializeField] CharacterIconInfoView _characterInfoPrefab;
    [SerializeField] GameObject _charactersContent;

    private CharacterToSelectData _characterToSelectData;
    private ApplicationData _applicationData;

    [Inject]
    public void Construct(CharacterToSelectData characterToSelectData, ApplicationData applicationData)
    {
        _characterToSelectData = characterToSelectData;
        _applicationData = applicationData;
        InitializeCharacterIcons();
    }

    private void InitializeCharacterIcons()
    {
        List<GameCharacterSO> characters = _characterToSelectData.GetCharacters();

        Debug.Log(characters.Count);
        for (int i = 0; i < characters.Count; i++)
        {
            var character = characters[i];
            var newCharacterIcon = Instantiate(_characterInfoPrefab, _charactersContent.transform);
            newCharacterIcon.SetCharacterInfo(new CharacterIconInfoDataObject(character));

            newCharacterIcon.OnIconClick += ( ) => 
            { 
                NewCharacterIcon_OnIconClick(character); 
            };        

            Debug.Log($"{i + 1} name - {characters[i].CharacterName}, " +
                $"description - {characters[i].CharacterDescription}, " +
                $"rarity - {characters[i].Rarity}");
        }
    }

    private void NewCharacterIcon_OnIconClick(GameCharacterSO gameCharacter)
    {
        if (_applicationData.SelectedCharacterInfo == null)
        {
            _applicationData.SelectedCharacterInfo = new SelectedCharacterInfo();
        }

        _applicationData.SelectedCharacterInfo.GameCharacter = gameCharacter;
        _applicationData.SelectedCharacterIndex = gameCharacter.CharacterIndex;

        SceneManager.LoadScene(GAME_SCENE_INDEX);
    }
}
