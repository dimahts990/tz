using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum Level
{
    Level_1 = 2,
    Level_2 = 5,
    Level_3 = 8
}

public class GameSystem : MonoBehaviour
{
    [SerializeField] private List<AssetValue> _LetterValues = new List<AssetValue>();
    [SerializeField] private List<AssetValue> _NumberValues = new List<AssetValue>();
    [SerializeField] private GameObject _restartButtonGameObject;
    [SerializeField] private GameObject _cellsGameObjectPrefab;
    [SerializeField] private GameObject _blackPanelGameObject;
    [SerializeField] private Transform _gamePanelTransform;

    private List<GameObject> generatedCells = new List<GameObject>();
    private List<AssetValue> assetValues = new List<AssetValue>();
    private AssetValue newValue;
    
    public Level level;

    private void Awake()
    {
        SelectTypeValue();
    }

    private void SelectTypeValue()
    {
        int whatCreated = Random.Range(0, 3);
        if (whatCreated == 0)
            assetValues = _LetterValues;
        else
            assetValues = _NumberValues;
    }

    private void Start() => generateCells();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            NextLevel();
    }

    private void generateCells()
    {
        for (int i = 0; i <= (int) level; i++)
        {
            GameObject newCell = Instantiate(_cellsGameObjectPrefab, _gamePanelTransform.position, Quaternion.identity,
                _gamePanelTransform);
            newValue = newValue.GenerateNewValue(assetValues);
            newCell.SetValueForCell(newValue);
            newCell.GetComponent<Cell>().HitEventOnCreateBeautifully.Invoke();
            generatedCells.Add(newCell);
        }
        theRightLetter.ReadyListValues();
    }

    private void updateCells()
    {
        foreach (GameObject cell in generatedCells)
        {
            newValue = newValue.GenerateNewValue(assetValues);
            cell.SetValueForCell(newValue);
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject newCell = Instantiate(_cellsGameObjectPrefab, _gamePanelTransform.position, Quaternion.identity,
                _gamePanelTransform);
            newValue = newValue.GenerateNewValue(assetValues);
            newCell.SetValueForCell(newValue);
            newCell.GetComponent<Cell>().HitEventOnCreateUgly.Invoke();
            generatedCells.Add(newCell);
        }
        theRightLetter.ReadyListValues();
    }
    
    public void NextLevel()
    {
        CellExtension.ClearCreatedValues();
        theRightLetter.ClearAllValuesList();
        SelectTypeValue();
        switch (level)
        {
            case Level.Level_1:
                level = Level.Level_2;
                updateCells();
                break;
            case Level.Level_2:
                level = Level.Level_3;
                updateCells();
                break;
            default:
                GameOver();
                break;
        }
    }

    public void GameOver()
    {
        foreach (var cell in generatedCells)
        {
            cell.GetComponent<Image>().raycastTarget = false;
        }
        _restartButtonGameObject.SetActive(true);
        _blackPanelGameObject.GetComponent<Effect>().FadeIn();
    }

    public void Restart()
    {
        foreach (GameObject cell in generatedCells)
        {
            Destroy(cell);
        }
        theRightLetter.DestroyText();
        _restartButtonGameObject.GetComponent<Effect>().FadeOut();
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        _blackPanelGameObject.GetComponent<Effect>().FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public static class CellExtension
{
    private static List<AssetValue> createdValues = new List<AssetValue>();
    
    public static void SetValueForCell(this GameObject cell, AssetValue assetValue)
    {
        cell.GetComponent<Cell>().SetAssetLetter(assetValue);
    }

    public static AssetValue GenerateNewValue(this AssetValue assetValue, List<AssetValue> allValues)
    {
        if (createdValues.Count == 0)
        {
            assetValue = allValues[Random.Range(0, allValues.Count)];
            createdValues.Add(assetValue);
            theRightLetter.AddValue(assetValue.value);
        }
        else
        {
            AssetValue check = new AssetValue();
            do
            {
                assetValue = allValues[Random.Range(0, allValues.Count)];
                check = createdValues.Find(value => value.value == assetValue.value);
            } while (assetValue == check);
            createdValues.Add(assetValue);
            theRightLetter.AddValue(assetValue.value);
        }
        return assetValue;
    }

    public static void ClearCreatedValues() => createdValues.Clear();
}
