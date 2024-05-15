using UnityEngine;

public class OverlapAttack : Attack
{
    [SerializeField] int _damage;
    [SerializeField] float _attackRange;
    [SerializeField] LayerMask _targetLayers;
    [SerializeField] Transform _attackStartPos;

    public override void DoAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(_attackStartPos.position, _attackRange, _targetLayers);

        foreach (Collider collider in colliders)
        {
            var character = collider.gameObject.GetComponent<ICharacter>();

            if (character != null)
            {
                character.TakeDamage(_damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackStartPos.position, _attackRange);
    }
}
