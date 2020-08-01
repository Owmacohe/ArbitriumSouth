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

    public static bool doesContain(int givenInt, ArrayList givenArray)
    {
        var checker = false;

        for (int i = 0; i < givenArray.Count; i++)
        {
            if (givenArray[i] == givenInt)
            {
                checker = true;
            }
        }

        if (checker != true)
        {
            checker = false;
        }

        return checker;
    }
}
