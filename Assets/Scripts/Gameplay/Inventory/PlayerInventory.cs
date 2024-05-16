using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] Inventory _inventory = new Inventory();
    [SerializeField] GameObject _pickUpable;
    
    [SerializeField] int _inventorySlotsCount;
    [SerializeField] int _currentSlot;

    public event Action<ItemSO> OnInventoryAction; // item

    private void Start()
    {
        _inventory.InitInventory(_inventorySlotsCount);
        _inventory.OnItemAdded += _inventory_OnItemAdded;
        _inventory.OnItemRemoved += _inventory_OnItemRemoved;
        _inventory.OnItemSwitched += _inventory_OnItemSwitched;
    }


    private void _inventory_OnItemSwitched(ItemSO oldItem, ItemSO newItem)
    {
        GameObject gm = Instantiate(_pickUpable, transform.position, transform.rotation);
        gm.GetComponent<PickUpable>().SetItem(oldItem);

        OnInventoryAction?.Invoke(newItem);
    }

    private void _inventory_OnItemRemoved(ItemSO item)
    {
        GameObject gm = Instantiate(_pickUpable, transform.position, transform.rotation);
        gm.GetComponent<PickUpable>().SetItem(item);

        OnInventoryAction?.Invoke(null);
    }

    private void _inventory_OnItemAdded(ItemSO item)
    {
        OnInventoryAction?.Invoke(item);
    }

    public ItemSO EquipItem()
    {
        return _inventory.GetItem(_currentSlot);
    }
    
    public void AddItem(ItemSO item)
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

        OnInventoryAction?.Invoke(_inventory.GetItem(_currentSlot));
    }
}
