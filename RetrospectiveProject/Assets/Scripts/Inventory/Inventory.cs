using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{

    [SerializeField]
    private List<Item> Items;
    [SerializeField]
    public GameEvent OnItemChanged;

    public void AddItem(Item item)
    {
        Items.Add(item);
        OnItemChanged.Raise();
    }

    public void ClearInventory()
    {
        Items.Clear();
        OnItemChanged.Raise();
    }

    public bool HasElementWithName(string Name)
    {
        foreach(var item in Items)
        {
            if(item.Name == Name)
            {
               return true;
            }
        }
        return false;
    }

    public void RemoveItem(string name)
    {
        for(var i = 0; i < Items.Count; i++)
        {
            if (Items[i].Name == name)
                Items.RemoveAt(i);
        }
        OnItemChanged.Raise();
    }
}
