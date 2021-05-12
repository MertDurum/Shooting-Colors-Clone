using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level/Create Level")]
public class LevelInfo : ScriptableObject
{
    public LevelLayoutLine[] LevelLayout;
}

[System.Serializable]
public class LevelLayoutLine
{
    public LevelLayoutItem[] Line;
}

[System.Serializable]
public class LevelLayoutItem
{
    public GameItems Item;
    public float yRotation;
}
