using TMPro;
using UnityEngine;
using VContainer;

public class CharacterGraphics : MonoBehaviour
{
    [SerializeField] Transform _characterTransform;
    [SerializeField] TopDownController _topDownController;

    private GameCharacterSO _character;
    private Transform _armTransform;
    private GameObject _curItemObj;

    [Inject]
    public void Construct(ApplicationData appData)
    {
        _character = appData.SelectedCharacterInfo.GameCharacter;
    }

    private void Start()
    {
        GameObject charGraphics = Instantiate(_character.CharacterPrefab, _characterTransform);
        _armTransform = charGraphics.GetComponent<CharacterAvatar>().GetArmPosition();
    }
}
