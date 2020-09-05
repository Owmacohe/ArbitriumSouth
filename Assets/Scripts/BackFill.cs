//59 by 25

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackFill : MonoBehaviour
{
    public float charNum; //utility variable used to increase/decrease the size of backboxes
    public GameObject fillPrefab; //backbox prefab
    public float brightness; //overall brightness of backboxes

    public GameObject[] backBoxes; //array of all the backboxes

    private void Start()
    {
        backBoxes = new GameObject[1475 / (int)charNum]; // sets the length of the backbox array

        //initial backbox placement
        float xPlacement = -8.7f;
        float yPlacement = 4.8f;

        //adds basic components to each created backbox, increments its placement and/or line, sets its brightness, and adds it to the array
        for (int i = 0; i < (1475 / charNum); i++)
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

            setBrightness(newFill);

            backBoxes[i] = newFill;
        }
    }

    //gets a random character from sets of either letters, numbers, both, or both plus extra characters
    public string getRandomCharacter(string givenType)
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

    //sets the brightness of the given backbox
    public void setBrightness(GameObject givenBox)
    {
        byte alpha = (byte)((brightness * 100000) * (1 / CalculateDistance(givenBox.transform)));
        givenBox.GetComponent<TextMeshProUGUI>().color = new Color32(10, 255, 10, alpha);
    }

    //returns an exponentially decreasing number based on distance from objects tagged "Box"
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