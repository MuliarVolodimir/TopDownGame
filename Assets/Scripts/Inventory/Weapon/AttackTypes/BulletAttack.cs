using System.Collections;
using UnityEngine;

public class BulletAttack : Attack
{
    [SerializeField] int _damage;
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _angleRot;
    [SerializeField] int _bulletCount;

    [SerializeField] bool _shootBurst;
    [SerializeField] int _burstCount;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _bulletSpawnerPos;

    private bool _canAttack;
    private float _burstRate = 0.1f; 

    private void Start()
    {
        _canAttack = true;
    }

    public override void DoAttack()
    {
        if (_canAttack)
        {
            _canAttack = false;
            ShootLogic(_damage);
        }
    }

    public void ShootLogic(int damage)
    {
        if (_bulletCount <= 0) return;

        if (_shootBurst)
        {
            StartCoroutine(ShootBurstWithDelay(damage));
        }
        else
        {
            CalculateTrajectory(damage, _bulletCount);
            _canAttack = true;
        }   
    }

    private IEnumerator ShootBurstWithDelay(int damage)
    {
        for (int i = 0; i < _burstCount; i++)
        {
            CalculateTrajectory(damage, _bulletCount);
            yield return new WaitForSeconds(_burstRate);
        }

        _canAttack = true;
    }

    private void CalculateTrajectory(int damage, int bulletCount)
    {
        float angleStep = _angleRot <= 0 ? 0 : _angleRot / (bulletCount - 1);

        for (int j = 0; j < bulletCount; j++)
        {
            float angle = -_angleRot / 2f + (j * angleStep);
            Quaternion rotation = Quaternion.AngleAxis(angle, _bulletSpawnerPos.up);

            Vector3 direction = rotation * _bulletSpawnerPos.forward;

            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnerPos.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Shoot(direction, damage, _bulletSpeed);
        }
    }
}
