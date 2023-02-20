using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Potion
{
    public int type;
    public float value;
}


[CreateAssetMenu(fileName = "Item Data", menuName = "ScriptableObject/Item Data", order = -1)]
public class ItemData : ScriptableObject
{
    public Potion[] potions = new Potion[3];
}
