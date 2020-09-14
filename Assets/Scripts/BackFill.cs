//59 by 25

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BackFill : MonoBehaviour
{
    public float charNum; //utility variable used to increase/decrease the size of backboxes
    public GameObject fillPrefab; //backbox prefab
    public float brightness; //overall brightness of backboxes
    public Transform glowSource; // where the background glow should be coming from
    public Color32 mediumGreen; //the bright (but not oversaturated) green used for the scene info in the top left corner
    public GameObject[] backBoxes; //array of all the backboxes
    public JSONReader jsonScript; //access to JSON reader script
    public string infoLetters; //the info to be displayed in the top left corner

    private void Start()
    {
        infoLetters = "Scene " + charConvert(jsonScript.sceneNum, "_", "/"); //setting the info

        backBoxes = new GameObject[1475 / (int)charNum]; // sets the length of the backbox array

        //initial backbox placement
        float xPlacement = -8.7f;
        float yPlacement = 4.8f;

        //creating a new backbox
        for (int i = 0; i < (1475 / charNum); i++)
        {
            var newFill = Instantiate(fillPrefab);
            //adding basic components...
            newFill.transform.SetParent(gameObject.transform);
            newFill.GetComponent<TextMeshProUGUI>().fontSize = (0.5f * charNum);
            newFill.GetComponent<TextMeshProUGUI>().text = getRandomCharacter("all");

            //setting its position...
            newFill.transform.position = new Vector2(xPlacement, yPlacement);
            xPlacement += (0.3f * charNum);

            if (newFill.transform.position.x > 8.7)
            {
                xPlacement = -8.7f;
                yPlacement -= (0.4f * charNum);
            }

            //setting its brightness...
            setBrightness(newFill);

            //adding it to the array...
            backBoxes[i] = newFill;

            //and finally setting the info in the corner (colour and letter)
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainScreen"))
            {
                if (i < infoLetters.Length)
                {
                    backBoxes[i].GetComponent<TextMeshProUGUI>().color = mediumGreen;
                }

                for (int j = 0; j < infoLetters.Length; j++)
                {
                    if (i == j)
                    {
                        backBoxes[i].GetComponent<TextMeshProUGUI>().text = infoLetters[j].ToString();
                    }
                }
            }
        }
    }

    //replaces all instances of a character in a string with another character, then returns the string
    string charConvert(string givenString, string givenOld, string givenNew)
    {
        string newString = "";

        for (int i = 0; i < givenString.Length; i++)
        {
            if (givenString[i].ToString() == givenOld)
            {
                newString += givenNew;
            }
            else
            {
                newString += givenString[i];
            }
        }

        return newString;
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

    //sets the brightness of the given backbox based on its distance from the glow source
    public void setBrightness(GameObject givenBox)
    {
        byte alpha = (byte)(brightness * Vector2.Distance(givenBox.transform.position, glowSource.position));
        givenBox.GetComponent<TextMeshProUGUI>().color = new Color32(10, 255, 10, alpha);

        //re-lights in corner info
        for (int i = 0; i < backBoxes.Length; i++)
        {
            if (i < infoLetters.Length && givenBox == backBoxes[i])
            {
                givenBox.GetComponent<TextMeshProUGUI>().color = mediumGreen;
            }
        }
    }

}