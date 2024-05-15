using UnityEngine;

public class BulletAttack : Attack
{
    [SerializeField] int _damage;
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _angleRot;
    [SerializeField] int _bulletCount;

    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _bulletSpawnerPos;
    public override void DoAttack()
    {
        if (_bulletCount <= 0) return;

        float angleStep = _angleRot <= 0 ? 0 : _angleRot / (_bulletCount - 1);

        for (int j = 0; j < _bulletCount; j++)
        {
            float angle = -_angleRot / 2f + (j * angleStep);
            Quaternion rotation = Quaternion.AngleAxis(angle, _bulletSpawnerPos.up);

            Vector3 direction = rotation * _bulletSpawnerPos.forward;

            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnerPos.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Shoot(direction, _damage, _bulletSpeed);
        }
    }
}
