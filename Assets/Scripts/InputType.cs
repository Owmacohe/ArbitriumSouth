using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputType : MonoBehaviour
{
    private TextMeshProUGUI inputText;

    public JSONReader jsonScript;
    public BackFillProper fillScript;
    public Color32 brightGreen;

    void Start()
    {
        inputText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (gameObject.GetComponent<TextMeshProUGUI>().text.Length < 5)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputText.text += " ";
                lightUpLetters();
            }

            int[] values;

            values = (int[])System.Enum.GetValues(typeof(KeyCode));

            for (int i = 0; i < values.Length; i++)
            {
                if (Input.GetKeyDown((KeyCode)values[i]) && values[i] > 96 && values[i] < 123)
                {
                    gameObject.GetComponent<TextMeshProUGUI>().text += (KeyCode)values[i];

                    lightUpLetters();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (inputText.text)
            {
                case "NORTH":
                    setNewScene("1");
                    break;
                case "WEST":
                    setNewScene("2");
                    break;
                case "EAST":
                    setNewScene("3");
                    break;
                case "SOUTH":
                    //TODO
                    break;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Backspace) && inputText.text.Length > 0)
        {
            inputText.text = inputText.text.Remove(inputText.text.Length - 1);
            lightUpLetters();
        }
    }

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

    void lightUpLetters()
    {
        string inputText = gameObject.GetComponent<TextMeshProUGUI>().text;

        if (inputText.StartsWith("N") || inputText.StartsWith("W") || inputText.StartsWith("E") || inputText.StartsWith("S"))
        {
            int inputLength = inputText.Length;
            int lightNum = inputLength * 50;

            for (int k = 0; k < fillScript.backBoxes.Length; k++)
            {
                fillScript.setBrightness(fillScript.backBoxes[k]);
            }

            for (int i = 0; i < lightNum; i++)
            {
                fillScript.backBoxes[Random.Range(0, fillScript.backBoxes.Length - 1)].GetComponent<TextMeshProUGUI>().color = brightGreen;
            }
        }
    }
}
