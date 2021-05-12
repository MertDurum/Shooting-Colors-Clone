using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Transform ExpectedPatternTransform;
    public GameObject LevelSelectionPanel;
    public Text CurrentLevelText;

    public LevelInfo[] Levels;
    public int CurrentLevel;
    [Header("Prefabs")]
    public GameObject ExpectedPatternTile;


    public void LoadLevel(int _level)
    {
        // Clear previous level items
        foreach (Transform child in ExpectedPatternTransform.transform)
        {
            Destroy(child.gameObject);
        }

        CurrentLevel = _level;

        // read Levels[CurrentLevel - 1] and generate the map and expected pattern from it.
        int rowCount = Levels[CurrentLevel - 1].LevelLayout.Length;
        int columnCount = Levels[CurrentLevel - 1].LevelLayout[0].Line.Length;

        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                if (Levels[CurrentLevel - 1].LevelLayout[row].Line[column].Item == null)
                    continue;

                GameItems item = Levels[CurrentLevel - 1].LevelLayout[row].Line[column].Item;
                if (item.ItemType == ObjectTypes.DefaultTile)
                {
                    RawImage GO = Instantiate(ExpectedPatternTile, ExpectedPatternTransform).GetComponent<RawImage>();
                    GO.rectTransform.anchoredPosition = new Vector2(column * 100, row * 100);
                    GO.color = item.ItemColor;
                }
            }
        }

        CurrentLevelText.text = "LEVEL " + _level;
        LevelSelectionPanel.SetActive(false);
    }
}
