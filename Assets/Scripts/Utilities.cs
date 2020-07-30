using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Utilities : MonoBehaviour
{
    public static void typeText(string givenTag)
    {
        var textObject = GameObject.FindWithTag(givenTag).GetComponent<TextMeshProUGUI>();
        var interiorText = textObject.text;

        textObject.text = "";

        //StartCoroutine(writeWait(interiorText));
    }

    IEnumerator writeWait(string givenText)
    {
        Debug.Log("test");

        for (int i = 0; i < givenText.Length; i++)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1);
            //interiorText += interiorText[i];
        }
    }
}
