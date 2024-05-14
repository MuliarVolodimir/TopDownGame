using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class Character : MonoBehaviour, ICharacter
{
    [SerializeField] CharacterCharacteristics _characterCharacteristics = new CharacterCharacteristics();
    [SerializeField] GameCharacter _gameCharacter;
    [SerializeField] List<CharacterResource> _resources = new List<CharacterResource>();

    private int _health;
    private bool _damagable;

    [Inject]
    public void Construct(ApplicationData appData)
    {
        _gameCharacter = appData.SelectedCharacterInfo.GameCharacter;
    }

    void Start()
    {
        _characterCharacteristics = _gameCharacter.CharacterCharacteristics;
        _health = _characterCharacteristics.HealthPoint;

        for (int i = 0; i < _gameCharacter.CharacterResources.Count; i++)
        {
            _resources.Add(_gameCharacter.CharacterResources[i]);
        }

        _damagable = true;
    }

    public void TakeDamage(int damage)
    {
        if (_damagable)
        {
            _health -= damage;
            CheckHealth();
        }   
    }

    public void TakeHealing(int healpoint)
    {
        _health += healpoint;
        CheckHealth();
    }

    public List<CharacterResource> GetResources()
    {
        return (_resources);
    }

    private void CheckHealth()
    {
        if (_health <= 0)
        {
            _health = 0;
            _damagable = false;
            Die();
        }
        if (_health >= _characterCharacteristics.HealthPoint)
        {
            _health = _characterCharacteristics.HealthPoint;
        }
    }

    private void Die()
    {
        // skeleton...
        GameObject dead = Instantiate(this.gameObject, transform.position, transform.rotation);
        dead.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        dead.transform.rotation = Quaternion.Euler(90, 0, 0);
        dead.GetComponent<CapsuleCollider>().enabled = false;

        Destroy(gameObject);
    }
}
