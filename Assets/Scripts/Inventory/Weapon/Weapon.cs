using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IItem
{
    [SerializeField] List<Attack> _attacks;
    [SerializeField] List<CharacterResource> _resources;
    [SerializeField] float _attackRate;

    public event Action DoAttack;
    private float _nextFire;
    public bool CanAttack;

    public void DoAction()
    {
        if (Time.time >= _nextFire)
        {
            CanAttack = false;
            _nextFire = Time.time + _attackRate;

            for (int i = 0; i < _attacks.Count; i++)
            {
                _attacks[i].DoAttack();
                DoAttack?.Invoke();
            }
            CanAttack = true;
        }
    }

    public List<CharacterResource> GetResources()
    {
        return _resources;
    }
}
