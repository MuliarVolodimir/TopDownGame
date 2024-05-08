using UnityEngine;
using VContainer;

public class CharacterGraphics : MonoBehaviour
{
    [SerializeField] Transform _characterTransform;

    private ApplicationData _applicationData;
    private GameCharacter _character;

    [Inject]
    public void Construct(ApplicationData appData)
    {
        _applicationData = appData;
        _character = _applicationData.SelectedCharacterInfo.GameCharacter;
    }

    private void Start()
    {
        GameObject charGraphics = Instantiate(_character.CharacterPrefab, _characterTransform);
    }
}
