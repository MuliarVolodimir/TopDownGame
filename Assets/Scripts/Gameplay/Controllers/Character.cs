using UnityEngine;

public class Character : MonoBehaviour, ICharacter
{
    [SerializeField] int _maxHealth;

    private int _health;

    private bool _damagable;
    void Start()
    {
        _health = _maxHealth;
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
        if (_health >= _maxHealth)
        {
            _health = _maxHealth;
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
