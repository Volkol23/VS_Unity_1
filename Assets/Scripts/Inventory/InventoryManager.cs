using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private ItemClass ItemToAdd;
    [SerializeField]
    private ItemClass ItemToRemove;
    [SerializeField]
    private GameObject SlotHolder;

    [SerializeField]
    private SlotClass[] StartingItems;

    private SlotClass[] Items;

    private GameObject[] Slots;

    private SlotClass MovingSlot;
    private SlotClass TemporalSlot;
    private SlotClass OriginalSlot;
    bool isMovingItem;

    private void Start()
    {
        Slots = new GameObject[SlotHolder.transform.childCount];
        Items = new SlotClass[Slots.Length];

        for (int i = 0; i < Items.Length; i++)
        {
            Items[i] = new SlotClass();
        }
        for (int i = 0; i < StartingItems.Length; i++)
        {
            Items[i] = StartingItems[i];
        }

        for (int i = 0; i < SlotHolder.transform.childCount; i++)
            Slots[i] = SlotHolder.transform.GetChild(i).gameObject;

        RefreshUI();
        if(ItemToAdd != null)
            Add(ItemToAdd);
        
        Remove(ItemToRemove);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isMovingItem)
                EndItemMove();
            else BeginItemMove(); 
        }
    }
    #region Inventory Utils
    public void RefreshUI()
    {
        for(int i = 0; i < Slots.Length; i++)
        {
            try
            {
                Slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                Slots[i].transform.GetChild(0).GetComponent<Image>().sprite = Items[i].GetItem().ItemIcon;

                if(Items[i].GetItem().IsStackable)
                    Slots[i].transform.GetChild(1).GetComponent<Text>().text = Items[i].GetQuantity() + "";
                else
                    Slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
            catch
            {
                Slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
                Slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                Slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
            
        }
    }
    public bool Add(ItemClass Item)
    {
        SlotClass Slot = Contains(Item);
        if(Slot != null && Slot.GetItem().IsStackable)
            Slot.AddQuantity(1);
        else
        {
            for(int i = 0; i < Items.Length; i++)
            {
                if (Items[i].GetItem() == null)
                {
                    Items[i] = new SlotClass(Item, 1);
                    break;
                }
            }
        }

        RefreshUI();
        return true;
    }

    public bool Remove(ItemClass Item)
    {
        SlotClass Temp = Contains(Item);
        if(Temp != null)
        {
            if (Temp.GetQuantity() > 1)
                Temp.SubQuantity(1);
            else
            {
                int SlotToRemoveIndex = 0;

                for (int i = 0; i < Items.Length; i++)
                {
                    if(Items[i].GetItem() == Item)
                    {
                        SlotToRemoveIndex = i;
                        break;
                    }
                }

                Items[SlotToRemoveIndex].Clear();
            }
        }
        else
        {
            return false;
        }

        RefreshUI();
        return true;

        
    }

    public SlotClass Contains(ItemClass Item)
    {
        for(int i = 0; i < Items.Length; i++)
        {
            if (Items[i].GetItem() == Item)
                return Items[i];
        }

        return null;
    }
    #endregion Inventory Utils

    #region Moving Items
    private bool BeginItemMove()
    {
        OriginalSlot = GetClosestSlot();
        if (OriginalSlot == null || OriginalSlot.GetItem() == null)
            return false;

        MovingSlot = new SlotClass(OriginalSlot);
        OriginalSlot.Clear();
        isMovingItem = true;
        RefreshUI();
        return true;
    }

    private bool EndItemMove()
    {
        OriginalSlot = GetClosestSlot();
        if(OriginalSlot.GetItem() != null)
        {
            if(OriginalSlot.GetItem() == MovingSlot.GetItem())
            {
                if (OriginalSlot.GetItem().IsStackable)
                {
                    OriginalSlot.AddQuantity(MovingSlot.GetQuantity());
                    MovingSlot.Clear();
                }
                else
                    return false;
            }
            else
            {
                TemporalSlot = new SlotClass(OriginalSlot);
                OriginalSlot.AddItem(MovingSlot.GetItem(), MovingSlot.GetQuantity());
                MovingSlot.AddItem(TemporalSlot.GetItem(), TemporalSlot.GetQuantity());
                RefreshUI();
                return true;
            }
        } 
        else
        {
            OriginalSlot.AddItem(MovingSlot.GetItem(), MovingSlot.GetQuantity());
            MovingSlot.Clear();
        }

        isMovingItem = false;
        RefreshUI();
        return true;

    }
    private SlotClass GetClosestSlot()
    {
        for(int i = 0; i < Slots.Length; i++)
        {
            if (Vector2.Distance(Slots[i].transform.position, Input.mousePosition) <= 32)
                return Items[i];
        }

        return null;
    }
    #endregion Moving Items
}
