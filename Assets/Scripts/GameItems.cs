using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Game Items/Create New Item")]
public class GameItems : ScriptableObject
{
    public ObjectTypes ItemType;
    public Color ItemColor;
}