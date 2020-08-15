using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharFlashIn : MonoBehaviour
{
    //private float distance;
    private TextMeshProUGUI textObject;

    public int charSpeed;
    public TMP_FontAsset titleFont;
    public Color baseColor;
    public Color glowColor;
    public Color glitchColor;

    void Start()
    {
        //distance = Vector2.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Title").transform.position);
        textObject = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (textObject.text == "")
        {
            if (distance >= 8 && Random.Range(0, 5 * charSpeed) == 0)
            {
                textObject.text = getRandomCharacter("all").ToString();
                checkTag();
            }
            else if (distance >= 6 && distance < 8 && Random.Range(0, 15 * charSpeed) == 0)
            {
                textObject.text = getRandomCharacter("all").ToString();
                checkTag();
            }
            else if (distance >= 4 && distance < 6 && Random.Range(0, 25 * charSpeed) == 0)
            {
                textObject.text = getRandomCharacter("all").ToString();
                checkTag();
            }
            else if (distance < 4 && Random.Range(0, 30 * charSpeed) == 0)
            {
                textObject.text = getRandomCharacter("all").ToString();
                checkTag();
            }
        }
        */

        if (Random.Range(0, 150 * charSpeed) == 0 && textObject.text == "")
        {
            textObject.text = getRandomCharacter("all").ToString();
            checkTag();
        }

        if (Random.Range(0, 100000) == 0 && gameObject.tag == "Untagged")
        {
            textObject.text = getRandomCharacter("all").ToString();
        }

        if (Random.Range(0, 500000) == 0 && gameObject.tag == "Untagged")
        {
            StartCoroutine(showGlitch());
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
            case "num":
                characters = "0123456789";
                break;
            case "alphaNum":
                characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                break;
            case "all":
                characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-=_+`~[]{}:;,.?/|";
                break;
        }

        var randomNum = Random.Range(0, characters.Length - 1);
        return characters[randomNum];
    }

    void checkTag()
    {
        if (gameObject.tag != "Untagged" && gameObject.tag != "Title" && gameObject.tag != "MenuParent")
        {
            textObject.font = titleFont;
            textObject.color = glowColor;

            gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("MenuParent").transform);
        }

        switch (gameObject.tag)
        {
            case "STag":
                textObject.text = "S";
                break;
            case "tTag":
                textObject.text = "t";
                break;
            case "aTag":
                textObject.text = "a";
                break;
            case "rTag":
                textObject.text = "r";
                break;
            case "_Tag":
                textObject.text = "_";
                StartCoroutine(flashUnderscore());
                break;
            case "ETag":
                textObject.text = "E";
                break;
            case "xTag":
                textObject.text = "x";
                break;
            case "iTag":
                textObject.text = "i";
                break;
            case "eTag":
                textObject.text = "e";
                break;
            case "nTag":
                textObject.text = "n";
                break;
            case "gTag":
                textObject.text = "g";
                break;
            case "sTag":
                textObject.text = "s";
                break;
        }
    }

    IEnumerator showGlitch()
    {
        textObject.color = glitchColor;

        yield return new WaitForSeconds(Random.Range(1, 3));

        textObject.color = baseColor;
    }

    IEnumerator flashUnderscore()
    {
        for (int i = 0; i < i+1; i++)
        {
            textObject.text = " ";
            yield return new WaitForSeconds(1);
            textObject.text = "_";
            yield return new WaitForSeconds(1);
        }
    }
}
