using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [SerializeField] List<Attack> _attacks;
    [SerializeField] List<CharacterResource> _resources;
    [SerializeField] float _attackRate;

    public event Action DoAttack;
    private float _nextFire;
    public bool CanAttack;

    public override void DoAction(PlayerCharacter playerCharacter)
    {
        if (Time.time >= _nextFire)
        {
            _nextFire = Time.time + _attackRate;

            for (int i = 0; i < _attacks.Count; i++)
            {
                if (playerCharacter.RemoveResources(_resources))
                {
                    _attacks[i].DoAttack();
                    DoAttack?.Invoke();
                }
            }
        }
    }
}
