using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BurstAttackType : AttackType
{
    [SerializeField] List<Attack> _attacks;

    [SerializeField] int _burstCount;
    [SerializeField] float _burstRate;

    public override void DoAttackType()
    {
        StartCoroutine(ShootBurstWithDelay());
    }

    private IEnumerator ShootBurstWithDelay()
    {
        for (int i = 0; i < _burstCount; i++)
        {

            for (int j = 0; j < _attacks.Count; j++)
            {
                _attacks[j].DoAttack();
            }
            yield return new WaitForSeconds(_burstRate);
        }
    }
}