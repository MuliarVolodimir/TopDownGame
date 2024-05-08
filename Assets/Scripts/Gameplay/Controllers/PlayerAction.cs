using System;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] TopDownController _tdController;
    [SerializeField] PlayerInventory _playerInventory;
    [SerializeField] InteractSystem _interactSystem;

    public event Action<Item> OnEquipItemSwitched; 

    void Update()
    {
        NonPhysixInputer();
    }
    private void FixedUpdate()
    {
        PhysixInputer();
    }

    private void PhysixInputer()
    {
        bool isAiming = false;
        bool isSprinting = false;

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (moveDir == null) moveDir = Vector3.zero; 

        if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse0))
        {
            isAiming = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        

        _tdController.HandleInput(moveDir, isAiming, isSprinting);
    }
    private void NonPhysixInputer()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playerInventory.SwitchEquipItem(0);
            OnEquipItemSwitched?.Invoke(_playerInventory.EquipItem());
            Debug.Log("pressed 1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerInventory.SwitchEquipItem(1);
            OnEquipItemSwitched?.Invoke(_playerInventory.EquipItem());
            Debug.Log("pressed 2");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _interactSystem.TryInteract();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            _playerInventory.RemoveItem();
            Debug.Log("pressed G");
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            _tdController.UseItem();
            Debug.Log("pressed use");
        }
    }
}
