using UnityEngine;

public class EnemyCharacter : MonoBehaviour, ICharacter
{
    [SerializeField] int _healthPoints;
    [SerializeField] int Speed;

    private int _health;
    private bool _damagable;

    void Start()
    {
        _health = _healthPoints;
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

    private void CheckHealth()
    {
        if (_health <= 0)
        {
            _health = 0;
            _damagable = false;
            Die();
        }
        if (_health >= _healthPoints)
        {
            _health = _healthPoints;
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
