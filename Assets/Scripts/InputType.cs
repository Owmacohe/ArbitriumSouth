using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputType : MonoBehaviour
{
    private TextMeshProUGUI inputText; //the text field that the player types into

    public JSONReader jsonScript; //access to JSON reader script
    public BackFill fillScript; //access to the backboxes fill script

    public Color32 brightGreen; //standard bright green colour
    public int probabNum; //probability for a backbox to light up when the player is typing (the lower the number, the higher the chance)
    public Transform northBox, westBox, eastBox, southBox; //transforms of the four directions, to know where to light up near

    private void Start()
    {
        inputText = gameObject.GetComponent<TextMeshProUGUI>();

        //Makes the cursor invisible and locks it in place
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (inputText.text.Length < 5) //putting a cap on the player's input length
        {
            //getting an array of all the keys' number codes
            int[] values;
            values = (int[])System.Enum.GetValues(typeof(KeyCode));

            //loops through all possible keys that could be being pressed, and if a letter key is, it types it in the input
            for (int i = 0; i < values.Length; i++)
            {
                if (Input.GetKeyDown((KeyCode)values[i]) && values[i] > 96 && values[i] < 123)
                {
                    inputText.text += (KeyCode)values[i];

                    lightUpLetters();
                }
            }
        }

        //when the player confirms their choice, it loads the new scene based on what's in the input (see setNewScene method below)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (inputText.text)
            {
                case "NORTH":
                    setNewScene("1");
                    lightUpLetters();
                    break;
                case "WEST":
                    setNewScene("2");
                    lightUpLetters();
                    break;
                case "EAST":
                    setNewScene("3");
                    lightUpLetters();
                    break;
                case "SOUTH":
                    //TODO
                    break;
            }
        }
        
        //gotta make sure the player can delete mistakes
        if (Input.GetKeyDown(KeyCode.Backspace) && inputText.text.Length > 0)
        {
            inputText.text = inputText.text.Remove(inputText.text.Length - 1);
            lightUpLetters();
        }
    }

    //sets the current scene number based on the target direction, then loads the scene
    void setNewScene(string givenNum)
    {
        if (jsonScript.sceneNum == "0")
        {
            jsonScript.sceneNum = givenNum;
        }
        else
        {
            jsonScript.sceneNum += "_" + givenNum;
        }

        jsonScript.loadScene();
        inputText.text = "";
    }

    //when called, lights up the backboxes bright green around a certain direction
    void lightUpLetters()
    {
        //clears all previous lit backboxes
        for (int k = 0; k < fillScript.backBoxes.Length; k++)
        {
            fillScript.setBrightness(fillScript.backBoxes[k]);
        }

        if (inputText.text.StartsWith("N") || inputText.text.StartsWith("W") || inputText.text.StartsWith("E") || inputText.text.StartsWith("S")) //only lights up if the player is typing a direction
        {
            int inputLength = inputText.text.Length + 1;

            //loops through every backbox, and if any are near the typed direction, and the RNG hits, they are lit up
            for (int i = 0; i < fillScript.backBoxes.Length; i++)
            {
                if (Random.Range(0, probabNum) == 0)
                {
                    if (inputText.text.StartsWith("N") && (Vector2.Distance(fillScript.backBoxes[i].transform.position, northBox.position) <= inputLength))
                    {
                        fillScript.backBoxes[i].GetComponent<TextMeshProUGUI>().color = brightGreen;
                    }
                    else if (inputText.text.StartsWith("W") && (Vector2.Distance(fillScript.backBoxes[i].transform.position, westBox.position) <= inputLength))
                    {
                        fillScript.backBoxes[i].GetComponent<TextMeshProUGUI>().color = brightGreen;
                    }
                    else if (inputText.text.StartsWith("E") && (Vector2.Distance(fillScript.backBoxes[i].transform.position, eastBox.position) <= inputLength))
                    {
                        fillScript.backBoxes[i].GetComponent<TextMeshProUGUI>().color = brightGreen;
                    }
                    else if (inputText.text.StartsWith("S") && (Vector2.Distance(fillScript.backBoxes[i].transform.position, southBox.position) <= inputLength))
                    {
                        fillScript.backBoxes[i].GetComponent<TextMeshProUGUI>().color = brightGreen;
                    }
                }
            }
        }
    }
}
