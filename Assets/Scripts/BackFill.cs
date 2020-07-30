using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackFill : MonoBehaviour
{
    public float typeSpeed;
    public int charLength;

    private TextMeshProUGUI textObject;

    void Start()
    {
        //Utilities.typeText("backfill");

        textObject = GetComponent<TextMeshProUGUI>();
        StartCoroutine(writeWait(typeSpeed));
    }

    IEnumerator writeWait(float speed)
    {
        for (int i = 0; i < charLength; i++)
        {
            textObject.text += getRandomCharacter("alphaNum");
            yield return new WaitForSeconds(speed);
        }
    }

    char getRandomCharacter(string givenType)
    {
        var characters = "";

        switch (givenType)
        {
            case "alpha":
                characters = "abcdefghijklmnopqrstuvwxyz";
                break;
            case "alphaNum":
                characters = "aabbccddeeffgghhiijjkkllmmnnooppqqrrssttuuvvwwxxyyzz0123456789";
                break;
            case "num":
                characters = "0123456789";
                break;
        }

        var randomNum = Random.Range(0, characters.Length - 1);
        return characters[randomNum];
    }
}
