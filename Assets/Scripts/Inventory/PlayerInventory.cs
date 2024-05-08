using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] Inventory _inventory = new Inventory();
    [SerializeField] GameObject _pickUpable;

    [SerializeField] int _energyAmmoCount;
    [SerializeField] int _inventorySlotsCount;
    [SerializeField] int _currentSlot;

    public event Action<Item, int> OnInventoryAction; // item, ammo

    private void Start()
    {
        _inventory.InitInventory(_inventorySlotsCount);
        _inventory.OnItemAdded += _inventory_OnItemAdded;
        _inventory.OnItemRemoved += _inventory_OnItemRemoved;
        _inventory.OnItemSwitched += _inventory_OnItemSwitched;
        _inventory.OnAmmoChanged += _inventory_OnAmmoChanged;
    }

    private void _inventory_OnAmmoChanged(int ammoCount)
    {
        _energyAmmoCount = ammoCount;
    }

    private void _inventory_OnItemSwitched(Item oldItem, Item newItem)
    {
        GameObject gm = Instantiate(_pickUpable, transform.position, transform.rotation);
        gm.GetComponent<PickUpable>().SetItem(oldItem);

        OnInventoryAction?.Invoke(newItem, _energyAmmoCount);
    }

    private void _inventory_OnItemRemoved(Item item)
    {
        GameObject gm = Instantiate(_pickUpable, transform.position, transform.rotation);
        gm.GetComponent<PickUpable>().SetItem(item);

        OnInventoryAction?.Invoke(null, _energyAmmoCount);
    }

    private void _inventory_OnItemAdded(Item item)
    {
        OnInventoryAction?.Invoke(item, _energyAmmoCount);
    }

    public Item EquipItem()
    {
        return _inventory.GetItem(_currentSlot);
    }
    
    public void AddItem(Item item)
    {
        _inventory.AddItem(item);
    }

    public void RemoveItem()
    {
        _inventory.RemoveItem(_currentSlot);
    }

    public void SwitchEquipItem(int slotindex)
    {
        _currentSlot = slotindex;

        OnInventoryAction?.Invoke(_inventory.GetItem(_currentSlot), _energyAmmoCount);
        Debug.Log(_currentSlot);
    }

}
