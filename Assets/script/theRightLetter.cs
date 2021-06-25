using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class theRightLetter : MonoBehaviour
{
    private static List<string> allValues = new List<string>();
    private static List<string> usedValues = new List<string>();
    private static Transform panel;
    
    public static string RightLetterString;
    public UnityEvent HitEventOnStart;
    
    private void Awake()
    {
        panel = transform;
    }
    
    private void Start()
    {
        HitEventOnStart.Invoke();
    }

    public static void AddValue(string letter) => allValues.Add(letter);

    public static void ReadyListValues()
    {
        if (usedValues.Count == 0)
            RightLetterString = allValues[Random.Range(0, allValues.Count)];
        else
        {
            bool isCheck;
            do
            {
                RightLetterString = allValues[Random.Range(0, allValues.Count)];
                isCheck = usedValues.Contains(RightLetterString) ? false : true;
            } while (!isCheck);
        }
        
        usedValues.Add(RightLetterString);
        panel.GetComponent<Text>().text = $"Find {RightLetterString}";
    }

    public static void ClearAllValuesList() => allValues.Clear();

    public static void DestroyText() => panel.gameObject.SetActive(false);
}
