using System;
using TMPro;
using UnityEngine;

public class RandomFill : MonoBehaviour
{
    [SerializeField] int numChars;
    
    void Start()
    {
        TMP_Text text = GetComponent<TMP_Text>();
        
        for (int i = 0; i < numChars; i++)
        {
            text.text += RandomChar.Get(true, false, true, true);
        }
    }
}