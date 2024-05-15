using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    [SerializeField] int _attacksToCombo;
    private int _attacksLeft;

    [SerializeField] Weapon _weapon;
    [SerializeField] Attack _comboAttack;

    private void Start()
    {
        _weapon.OnAttack += _weapon_DoAttack;
    }

    private void _weapon_DoAttack()
    {
        if (_comboAttack != null)
        {
            _attacksLeft++;
            if (_attacksLeft >= _attacksToCombo)
            {
                _attacksLeft = 0;
                _comboAttack.DoAttack();
            }
        }
    }
}
