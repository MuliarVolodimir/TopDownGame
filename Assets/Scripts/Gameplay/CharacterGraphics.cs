using UnityEngine;
using VContainer;

public class CharacterGraphics : MonoBehaviour
{
    [SerializeField] Transform _characterTransform;

    private GameCharacter _character;

    [Inject]
    public void Construct(ApplicationData appData)
    {
        _character = appData.SelectedCharacterInfo.GameCharacter;
    }

    private void Start()
    {
        GameObject charGraphics = Instantiate(_character.CharacterPrefab, _characterTransform);
    }
}
