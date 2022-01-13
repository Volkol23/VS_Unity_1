using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotClass
{
    [SerializeField]
    private ItemClass item = null;
    [SerializeField]
    private int quantity = 0;

    public SlotClass()
    {
        item = null;
        quantity = 0;
    }
    public SlotClass(ItemClass Item, int Quantity)
    {
        item = Item;
        quantity = Quantity;
    }
    public SlotClass (SlotClass Slot)
    {
        item = Slot.GetItem();
        quantity = Slot.GetQuantity();
    }
    public void Clear()
    {
        item = null;
        quantity = 0;
    }

    public ItemClass GetItem() { return item; }
    public int GetQuantity() { return quantity; }
    public void AddQuantity(int _Quantity) { quantity += _Quantity; }
    public void SubQuantity(int _Quantity) { quantity -= _Quantity; }
    public void AddItem(ItemClass Item, int Quantity)
    {
        item = Item;
        quantity = Quantity;
    }
}
