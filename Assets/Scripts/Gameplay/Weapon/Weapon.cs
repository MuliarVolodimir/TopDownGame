using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [SerializeField] List<CharacterResource> _resources;
    [SerializeField] float _attackRate;
    [SerializeField] AttackType _attackType;

    private float _nextFire;
    public event Action OnAttack;

    public override void DoAction(PlayerCharacter playerCharacter)
    {
        if (Time.time >= _nextFire)
        {
            _nextFire = Time.time + _attackRate;

            if (_resources.Count > 0 && _resources != null)
            {
                if (playerCharacter.RemoveResources(_resources))
                {
                    _attackType.DoAttackType();
                    OnAttack?.Invoke();
                }
            }  
            else
            {
                _attackType.DoAttackType();
                OnAttack?.Invoke();
            }
        }
    }
}
