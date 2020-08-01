using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharFlashIn : MonoBehaviour
{
    private float distance;
    private TextMeshProUGUI textObject;

    public int charSpeed;
    public TMP_FontAsset titleFont;

    void Start()
    {
        distance = Vector2.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Title").transform.position);
        textObject = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textObject.text == "")
        {
            if (distance >= 8 && Random.Range(0, 5 * charSpeed) == 0)
            {
                textObject.text = getRandomCharacter("all").ToString();
            }
            else if (distance >= 6 && distance < 8 && Random.Range(0, 10 * charSpeed) == 0)
            {
                textObject.text = getRandomCharacter("all").ToString();
            }
            else if (distance >= 4 && distance < 6 && Random.Range(0, 12 * charSpeed) == 0)
            {
                textObject.text = getRandomCharacter("all").ToString();
            }
            else if (distance < 4 && Random.Range(0, 15 * charSpeed) == 0)
            {
                textObject.text = getRandomCharacter("all").ToString();
            }
        }

        if (Random.Range(0, 30000) == 0 && textObject.font != titleFont)
        {
            textObject.text = getRandomCharacter("all").ToString();
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
}
