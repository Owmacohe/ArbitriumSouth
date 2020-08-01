using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharFlashIn : MonoBehaviour
{
    private float distance;
    private TextMeshProUGUI textObject;

    public int charSpeed;

    void Start()
    {
        distance = Vector2.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Title").transform.position);
        textObject = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BackFill.fillsAdded == true)
        {
            if (distance >= 8 && Random.Range(0, 1000) == 0 && textObject.text == "")
            {
                textObject.text = getRandomCharacter("alphaNum").ToString();
            }
            else if (distance > 6 && distance < 8 && Random.Range(0, 3000) == 0 && textObject.text == "")
            {
                textObject.text = getRandomCharacter("alphaNum").ToString();
            }
            else if (distance > 4 && distance < 6 && Random.Range(0, 10000) == 0 && textObject.text == "")
            {
                textObject.text = getRandomCharacter("alphaNum").ToString();
            }
            else if (distance > 2 && distance < 4 && Random.Range(0, 17000) == 0 && textObject.text == "")
            {
                textObject.text = getRandomCharacter("alphaNum").ToString();
            }
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
        }

        var randomNum = Random.Range(0, characters.Length - 1);
        return characters[randomNum];
    }
}
