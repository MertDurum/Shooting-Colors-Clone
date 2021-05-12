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

    public List<GameObject> LevelObjects;

    [Header("Prefabs")]
    public GameObject ExpectedPatternTile;
    public GameObject DefaultTile;
    public GameObject Canon;

    void Start()
    {
        LevelObjects = new List<GameObject>();
    }

    public void LoadLevel(int _level)
    {
        // Clear previous level pattern
        foreach (Transform child in ExpectedPatternTransform.transform)
        {
            Destroy(child.gameObject);
        }

        // Clear previous level objects
        foreach (GameObject item in LevelObjects)
        {
            Destroy(item);
        }
        LevelObjects.Clear();

        CurrentLevel = _level;

        // @TODO: Center the expected pattern while creating it
        int rowCount = Levels[CurrentLevel - 1].LevelLayout.Length;
        int columnCount = Levels[CurrentLevel - 1].LevelLayout[0].Line.Length;

        float rowCenter = (rowCount - 1) / 2f;
        float columnCenter = (columnCount - 1) / 2f;

        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                if (Levels[CurrentLevel - 1].LevelLayout[row].Line[column].Item == null)
                    continue;

                GameItems item = Levels[CurrentLevel - 1].LevelLayout[row].Line[column].Item;
                if (item.ItemType == ObjectTypes.DefaultTile)
                {
                    RawImage image = Instantiate(ExpectedPatternTile, ExpectedPatternTransform).GetComponent<RawImage>();
                    image.rectTransform.anchoredPosition = new Vector2(column * 100, -row * 100);
                    image.color = item.ItemColor;

                    GameObject tile = Instantiate(DefaultTile, new Vector3(column - columnCenter, 0, rowCenter - row), DefaultTile.transform.rotation);
                    tile.GetComponent<DefaultTile>().ExpectedColor = item.ItemColor;
                    LevelObjects.Add(tile);
                }
                else if (item.ItemType == ObjectTypes.Canon)
                {
                    GameObject canon = Instantiate(Canon, new Vector3(column - columnCenter, 0, rowCenter - row), DefaultTile.transform.rotation * Quaternion.Euler(new Vector3(0, Levels[CurrentLevel - 1].LevelLayout[row].Line[column].yRotation, 0))); // change rotation due to the layout[row].line[column].yRotation
                    canon.GetComponent<Canon>().ProjectileColor = item.ItemColor;
                    LevelObjects.Add(canon);
                }
            }
        }

        CurrentLevelText.text = "LEVEL " + _level;
        LevelSelectionPanel.SetActive(false);
    }
    
    public void OpenSelectLevelScreen()
    {
        LevelSelectionPanel.SetActive(true);
    }
}
