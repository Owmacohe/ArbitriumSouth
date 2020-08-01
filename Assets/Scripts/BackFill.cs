using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackFill : MonoBehaviour
{
    public float typeSpeed;
    public int charLength;
    public GameObject fillPrefab;

    void Start()
    {
        //Utilities.typeText("backfill");

        StartCoroutine(writeWait(typeSpeed));
    }

    IEnumerator writeWait(float speed)
    {
        float xPlacement = -8.5f;
        float yPlacement = 4.5f;

        for (int i = 0; i < charLength; i++)
        {
            var newFill = Instantiate(fillPrefab);
            newFill.transform.SetParent(gameObject.transform);

            newFill.transform.position = new Vector2(xPlacement, yPlacement);
            xPlacement += 0.5f;

            ArrayList array = new string[] { 34, 69, 104, 139, 174, 209, 244, 279, 314, 349, 384, 419, 454 };

            if (Utilities.doesContain(i, array) == true)
            {
                xPlacement = -8.5f;
                yPlacement -= 0.75f;
            }

            var fillTMP = newFill.GetComponent<TextMeshProUGUI>();
            fillTMP.text += getRandomCharacter("alphaNum");

            yield return new WaitForSeconds(speed);
        }
    }

    char getRandomCharacter(string givenType)
    {
        var characters = "";

        switch (givenType)
        {
            case "alpha":
                characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                break;
            case "alphaNum":
                characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                break;
            case "num":
                characters = "0123456789";
                break;
        }

        var randomNum = Random.Range(0, characters.Length - 1);
        return characters[randomNum];
    }
}
