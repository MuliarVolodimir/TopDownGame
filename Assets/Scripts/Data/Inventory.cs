using System;

public class Inventory 
{
    private Item[] _items;

    private int _currentSlot = 0;

    //Actions :)
    public event Action<Item> OnItemRemoved;        // Remove // item, isUsed
    public event Action<Item> OnItemAdded;          // Add    // item
    public event Action<Item, Item> OnItemSwitched; // Switch // itemOld, itemNew,

    public void InitInventory(int slotsCount)
    {
        _items = new Item[slotsCount];

        for (int i = 0; i < _items.Length; i++)
        {
            _items[i] = null;
        }
    }

    public void AddItem(Item item)
    {
        int freeSlot = FindingFreeSlot();

        // switch item if we didn`t find free slot     
        if (freeSlot != -1)
        {
            _items[freeSlot] = item;
        }
        else
        {
            freeSlot = _currentSlot;
            SwitchItem(freeSlot, item);
        }

        OnItemAdded?.Invoke(item);
    }

    private int FindingFreeSlot()
    {
        // finding free slot...
        for (int i = 0; i <= _items.Length - 1; i++)
        {
            if (_items[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public Item GetItem(int slotIndex)
    {
        return _items[slotIndex] != null ? _items[slotIndex] : null;
    }

    public void RemoveItem(int slotIndex)
    {
        var item = _items[slotIndex];
        _items[slotIndex] = null;

        OnItemRemoved?.Invoke(item);
    }

    private void SwitchItem(int slotIndex, Item newItem)
    {
        var oldItem = _items[slotIndex];
        _items[slotIndex] = newItem;

        OnItemSwitched?.Invoke(oldItem, newItem);
    }  
}
