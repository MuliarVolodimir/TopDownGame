using System.Collections.Generic;
using UnityEngine;

public class AutoAttackType : AttackType
{
    [SerializeField] List<Attack> _attacks;
    
    public override void DoAttackType()
    {
        for (int i = 0; i < _attacks.Count; i++)
        {
            _attacks[i].DoAttack();
        }
    }
}
