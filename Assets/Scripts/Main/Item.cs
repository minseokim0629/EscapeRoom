using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    notebook, laptop, newspaper, vitamin, none
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable/Item")]
public class Item : ScriptableObject
{
    public string name;
    [TextArea]
    public string desc;
    public ItemType type;
    public GameObject model;
}
