using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public int Id;
    public Sprite Sprite;
    public bool IsCollectable;
}
