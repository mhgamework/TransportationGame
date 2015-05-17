using System;
using Assets.Model;
using Assets.Scripts;
using UnityEngine;
using System.Collections;
using System.Linq;

public class CartScript : MonoBehaviour
{


    //TODO: persist
    private CartItemScript[] slots;
    private MItem[] items;

    // Use this for initialization
    void Start()
    {
        slots = transform.GetComponentsInChildren<CartItemScript>();
        items = new MItem[slots.Length];
        UpdateSlots();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var item = items[i];
            var slot = slots[i];

            if (item == null)
            {
                slot.gameObject.SetActive(false);
                continue;
            }

            slot.gameObject.SetActive(true);
        }
    }

    public bool CanAdd(params MItem[] newItems)
    {
        /*if (items.Length == 0) return true;
        if (items.Length > 1) throw new NotImplementedException("Multiple add not yet supported");
        return slots.Any(s => !s.IsFull());*/
        return items.Count(i => i == null) >= newItems.Length;
    }
    public void AddItems(params MItem[] newItems)
    {
        if (!CanAdd(newItems)) throw new InvalidOperationException();

        foreach (var mItem in newItems)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null) continue;
                items[i] = mItem;
                break;
            }
        }
        UpdateSlots();

        /*var slot = slots.FirstOrDefault(s => !s.IsFull() && s.ItemType == items[0].Type);
        if (slot == null)slot = slots.FirstOrDefault(s => !s.IsFull() && s.ItemType == null);

        slot.ItemType = items[0].Type;
        slot.Count ++;
        slot.UpdateView();*/
    }


    /*public class Slot
    {
        public Transform RootTransform;
        public MItemType ItemType;
        public int Count { get; set; }
        public int Max { get; private set; }

        public Slot(Transform rootTransform, int max)
        {
            RootTransform = rootTransform;
            Max = max;
        }


        public bool IsFull()
        {
            return Count >= Max;
        }

        public void UpdateView()
        {
            throw new NotImplementedException();
        }
    }*/

    public MItem TakeItem(CartItemScript cartItemScript)
    {
        var i = Array.IndexOf(slots,cartItemScript);
        if (items[i] == null) throw new InvalidOperationException();
        var ret = items[i];
        items[i] = null;

        UpdateSlots();
        return ret;
    }
}
