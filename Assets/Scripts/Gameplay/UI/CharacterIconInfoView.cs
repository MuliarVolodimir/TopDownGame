using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIconInfoView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] Button _button;
    [SerializeField] Image _icon;

    public event Action OnIconClick;

    private CharacterIconInfoDataObject _characterIconData;

    private void Start()
    {
        _button.onClick.AddListener(_button_onClick);
    }

    private void _button_onClick()
    {
        OnIconClick?.Invoke();
    }

    public void SetCharacterInfo(CharacterIconInfoDataObject characterIconInfoDataObject)
    {
        _characterIconData = characterIconInfoDataObject;   
        var character = characterIconInfoDataObject.Character;

        _name.text = character.CharacterName;
    }
}
