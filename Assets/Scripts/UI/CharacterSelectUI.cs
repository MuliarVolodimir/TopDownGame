using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

public class CharacterSelectUI : MonoBehaviour
{
    private const int GAME_SCENE_INDEX = 3;
    private const int MAIN_MENU_SCENE = 1;

    [Header("SceneUI")]
    [SerializeField] Button _stratGame;
    [SerializeField] Button _mainMenu;

    [SerializeField] TextMeshProUGUI _characterCount;
    [SerializeField] TextMeshProUGUI _curCharacterName;
    [SerializeField] TextMeshProUGUI _curCharacterDescription;

    [Space(10)]
    [Header("Character icon")]
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

    private void Start()
    {
        _stratGame.onClick.AddListener(StartGame);
        //_mainMenu.onClick.AddListener(MainMenu);
    }

    private void InitializeCharacterIcons()
    {
        List<GameCharacterSO> characters = _characterToSelectData.GetCharacters();

        _characterCount.text = $"Characters {characters.Count}/{characters.Count}";

        for (int i = 0; i < characters.Count; i++)
        {
            var character = characters[i];
            var newCharacterIcon = Instantiate(_characterInfoPrefab, _charactersContent.transform);
            newCharacterIcon.SetCharacterInfo(new CharacterIconInfoDataObject(character));

            newCharacterIcon.OnIconClick += () =>
            {
                NewCharacterIcon_OnIconClick(character);
            };
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
        _curCharacterName.text = gameCharacter.CharacterName;
        _curCharacterDescription.text = gameCharacter.CharacterDescription;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(GAME_SCENE_INDEX);
    }
    private void MainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }
}
