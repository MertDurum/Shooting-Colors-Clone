using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject LevelSelectionPanel;
    public LevelInfo[] Levels;
    public int CurrentLevel;


    public void LoadLevel(int _level)
    {
        CurrentLevel = _level;

        // read Levels[CurrentLevel - 1] and generate the map from it.
        // update UI text

        LevelSelectionPanel.SetActive(false);
    }
}
