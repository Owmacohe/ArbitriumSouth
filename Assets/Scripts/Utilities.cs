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

        for (int i = 0; i < interiorText.Length; i++)
        {
            interiorText += interiorText[i];
            //waitSeconds(1);
        }
    }

    public static IEnumerator waitSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
