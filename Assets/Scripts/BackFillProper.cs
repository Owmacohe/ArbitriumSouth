//59 by 25

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackFillProper : MonoBehaviour
{
    public float charNum;
    public GameObject fillPrefab;
    public float brightness;

    void Start() {
        float xPlacement = -8.7f;
        float yPlacement = 4.8f;

        for (int i = 0; i < (1475 / charNum) ; i++)
        {
            var newFill = Instantiate(fillPrefab);
            newFill.transform.SetParent(gameObject.transform);
            newFill.GetComponent<TextMeshProUGUI>().fontSize = (0.5f * charNum);
            newFill.GetComponent<TextMeshProUGUI>().text = getRandomCharacter("all");

            newFill.transform.position = new Vector2(xPlacement, yPlacement);
            xPlacement += (0.3f * charNum);

            if (newFill.transform.position.x > 8.7)
            {
                xPlacement = -8.7f;
                yPlacement -= (0.4f * charNum);
            }

            byte alpha = (byte)((brightness * 100000) * (1 / CalculateDistance(newFill.transform)));
            //Debug.Log(alpha);

            newFill.GetComponent<TextMeshProUGUI>().color = new Color32(10, 255, 10, alpha);
        }
    }

    string getRandomCharacter(string givenType)
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
                characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$%^&*()-=_+`''~[]{}:;,.?/|";
                break;
        }

        var randomNum = Random.Range(0, characters.Length - 1);
        return characters[randomNum].ToString();
    }

    float CalculateDistance(Transform thisPosition)
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        float boxDistance = 0;

        for (int i = 0; i < boxes.Length; i++)
        {
            boxDistance += Vector2.Distance(thisPosition.position, boxes[i].transform.position);
        }

        return Mathf.Pow(boxDistance, 2);
    }
}
