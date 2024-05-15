using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class PlayerCharacter : MonoBehaviour, ICharacter
{
    [SerializeField] CharacterCharacteristics _characterCharacteristics = new CharacterCharacteristics();
    [SerializeField] GameCharacterSO _gameCharacter;
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

    public void AddResources(CharacterResource characterResource)
    {
        for (int i = 0; i < _resources.Count; i++)
        {
            if (_resources[i].Resource == characterResource.Resource && _resources[i].Resource._canBeOverwritten)
            {
                _resources[i].Count += characterResource.Count;
            }
        }
    }

    public bool RemoveResources(List<CharacterResource> resources)
    {
        var availableResources = new List<CharacterResource>(_resources);

        foreach (var requiredResource in resources)
        {
            var availableResource = availableResources.Find(r => r.Resource == requiredResource.Resource);

            if (availableResource == null || availableResource.Count < requiredResource.Count)
            {
                return false;
            }

            availableResource.Count -= requiredResource.Count;
        }

        _resources = availableResources;

        return true;
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
