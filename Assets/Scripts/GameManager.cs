using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public LevelInfo[] Levels;
    private int CurrentLevel = -1;
    public bool Paused;
    public GameObject MainCamera;
    public GameObject[] LevelButtons;
    private List<GameObject> LevelObjects;

    [Header("Prefabs")]
    public GameObject ExpectedPatternTile;
    public GameObject DefaultTile;
    public GameObject SpikeTile;
    public GameObject CornerTile;
    public GameObject Canon;

    [Header("UI")]
    public Transform ExpectedPatternTransform;
    public GameObject LevelSelectionPanel;
    public GameObject LevelCompletePanel;
    public Text CurrentLevelText;

    void Start()
    {
        LevelObjects = new List<GameObject>();
        UpdateLevelButtons();
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

        int rowCount = Levels[CurrentLevel - 1].LevelLayout.Length;
        int columnCount = Levels[CurrentLevel - 1].LevelLayout[0].Line.Length;

        float rowCenter = (rowCount - 1) / 2f;
        float columnCenter = (columnCount - 1) / 2f;

        // Generate level objects
        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                if (Levels[CurrentLevel - 1].LevelLayout[row].Line[column].Item == null)
                    continue;

                GameItems item = Levels[CurrentLevel - 1].LevelLayout[row].Line[column].Item;
                if (item.ItemType == ObjectTypes.DefaultTile || item.ItemType == ObjectTypes.SpikeTile || item.ItemType == ObjectTypes.CornerTile)
                {
                    RawImage image = Instantiate(ExpectedPatternTile, ExpectedPatternTransform).GetComponent<RawImage>();
                    image.rectTransform.anchoredPosition = new Vector2((column - columnCenter) * 100, (rowCenter - row) * 100);
                    image.color = item.ItemColor;

                    if (item.ItemType == ObjectTypes.DefaultTile)
                    {
                        GameObject tile = Instantiate(DefaultTile, new Vector3(column - columnCenter, 0, rowCenter - row), DefaultTile.transform.rotation);
                        tile.GetComponent<DefaultTile>().ExpectedColor = item.ItemColor;
                        LevelObjects.Add(tile);
                    }
                    else if (item.ItemType == ObjectTypes.SpikeTile)
                    {
                        GameObject tile = Instantiate(SpikeTile, new Vector3(column - columnCenter, 0, rowCenter - row), SpikeTile.transform.rotation);
                        tile.GetComponent<SpikeTile>().ExpectedColor = item.ItemColor;
                        LevelObjects.Add(tile);
                    }
                    else if (item.ItemType == ObjectTypes.CornerTile)
                    {
                        GameObject tile = Instantiate(CornerTile, new Vector3(column - columnCenter, 0, rowCenter - row), CornerTile.transform.rotation * Quaternion.Euler(new Vector3(0, Levels[CurrentLevel - 1].LevelLayout[row].Line[column].yRotation, 0)));
                        tile.GetComponent<CornerTile>().ExpectedColor = item.ItemColor;
                        LevelObjects.Add(tile);
                    }
                }
                else if (item.ItemType == ObjectTypes.Canon)
                {
                    GameObject canon = Instantiate(Canon, new Vector3(column - columnCenter, 0, rowCenter - row), DefaultTile.transform.rotation * Quaternion.Euler(new Vector3(0, Levels[CurrentLevel - 1].LevelLayout[row].Line[column].yRotation, 0))); // change rotation due to the layout[row].line[column].yRotation
                    canon.GetComponent<Canon>().ProjectileColor = item.ItemColor;
                    canon.GetComponent<Canon>().GM = this;
                    LevelObjects.Add(canon);
                }
            }
        }

        // Set camera position
        int longerAxis = (rowCount > columnCount) ? rowCount : columnCount;

        Vector3 cameraPos = new Vector3();
        cameraPos.x = 0;
        cameraPos.y = (longerAxis + 1) * 2;
        cameraPos.z = cameraPos.y / -2f;

        MainCamera.transform.position = cameraPos;
        
        // Update UI
        CurrentLevelText.text = "LEVEL " + _level;
        LevelSelectionPanel.SetActive(false);
        LevelCompletePanel.SetActive(false);

        Paused = false;
    }

    public void NextLevel()
    {
        if (CurrentLevel + 1 <= Levels.Length)
        {
            LoadLevel(CurrentLevel + 1);
        }
        else
        {
            LevelSelectionPanel.SetActive(true);
            LevelCompletePanel.SetActive(false);
        }
    }

    public IEnumerator CheckWinConditions()
    {
        bool win = true;
        foreach (GameObject item in LevelObjects)
        {
            if (item.TryGetComponent(out DefaultTile tile))
            {
                if (!tile.isCorrect())
                {
                    win = false;
                    break;
                }
            }
        }

        if (win)
        {
            yield return new WaitForSecondsRealtime(0.1f);

            bool winDoubleCheck = true;

            foreach (GameObject item in LevelObjects)
            {
                if (item.TryGetComponent(out DefaultTile tile))
                {
                    if (!tile.isCorrect())
                    {
                        winDoubleCheck = false;
                        break;
                    }
                }
            }

            if (winDoubleCheck)
            {
                if (PlayerPrefs.GetInt("LevelProgress") < CurrentLevel + 1)
                {
                    PlayerPrefs.SetInt("LevelProgress", CurrentLevel + 1);
                }
                LevelCompletePanel.SetActive(true);
                Paused = true;

                // Clear previous level projectiles
                foreach (Projectile projectile in GameObject.FindObjectsOfType<Projectile>())
                {
                    Destroy(projectile.gameObject);
                }
            }
        }
    }

    public void ShowLevelScreen()
    {
        UpdateLevelButtons();
        LevelSelectionPanel.SetActive(true);
    }

    private void UpdateLevelButtons()
    {
        int levelProgress = PlayerPrefs.GetInt("LevelProgress", -1);

        if (levelProgress == -1)
        {
            PlayerPrefs.SetInt("LevelProgress", 1);
            levelProgress = 1;
        }

        for (int i = 0; i < LevelButtons.Length; i++)
        {
            Button B = LevelButtons[i].GetComponent<Button>();
            if (i < levelProgress)
                B.interactable = true;
            else
                B.interactable = false;
        }
    }

    public void DeleteProgress()
    {
        PlayerPrefs.DeleteKey("LevelProgress");
        UpdateLevelButtons();
    }
}
