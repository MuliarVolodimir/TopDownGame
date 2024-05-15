using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    [SerializeField] private LayerMask _collisionMask;

    private Vector3 _direction;
    private int _damage;
    private float _speed;

    public void Shoot(Vector3 direction, int damage, float speed)
    {
        _direction = direction.normalized;
        _damage = damage;
        _speed = speed;
        Destroy(gameObject, _lifeTime);
    }

    private void FixedUpdate()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        float distanceThisFrame = _speed * Time.deltaTime;
        RaycastHit hit; 

        if (Physics.Raycast(transform.position, _direction, out hit, distanceThisFrame, _collisionMask))
        {
            ICharacter character = hit.transform.GetComponent<ICharacter>();
            if (character != null)
            {
                character.TakeDamage(_damage);
            }
            Debug.Log(hit.transform.name);
            Destroy(gameObject);
            return;
        }

        transform.Translate(_direction * distanceThisFrame, Space.World);
    }
}
