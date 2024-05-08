using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class ApplicationController : LifetimeScope
{
    [SerializeField] CharacterToSelectData _characterToSelectData;
    [SerializeField] BiomesData _biomesData;

    [SerializeField] ItemsData _itemsData;

    private ApplicationData _applicationData; 
    private ApplicationDataSaver _applicationDataSaver = new ApplicationDataSaver();

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_characterToSelectData);
        builder.RegisterComponent(_applicationDataSaver);
        builder.RegisterComponent(_biomesData);

        _applicationData = _applicationDataSaver.LoadData();    
        builder.RegisterComponent(_applicationData);
    }

    private void Start()
    {
        _applicationData.SelectedCharacterInfo = new SelectedCharacterInfo();
        _applicationData.SelectedCharacterInfo.GameCharacter = GetCharacterPrefab(_characterToSelectData);

        DontDestroyOnLoad(this);
        SceneManager.LoadScene(1);
    }

    private GameCharacter GetCharacterPrefab(CharacterToSelectData characterToSelectData)
    {
        List<GameCharacter> characters = characterToSelectData.GetCharacters();

        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].CharacterIndex == _applicationData.SelectedCharacterIndex)
            {
                return characters[i];
            }
        }

        return null;
    }

    private void OnApplicationQuit()
    {
        _applicationDataSaver.SaveData();
    }
}