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
    public int probabNum;

    public Transform northBox, westBox, eastBox, southBox;

    void Start()
    {
        inputText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (inputText.text.Length < 5)
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
                    inputText.text += (KeyCode)values[i];

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
        for (int k = 0; k < fillScript.backBoxes.Length; k++)
        {
            fillScript.setBrightness(fillScript.backBoxes[k]);
        }

        if (inputText.text.StartsWith("N") || inputText.text.StartsWith("W") || inputText.text.StartsWith("E") || inputText.text.StartsWith("S"))
        {
            int inputLength = inputText.text.Length + 1;

            for (int i = 0; i < fillScript.backBoxes.Length; i++)
            {
                if (inputText.text.StartsWith("N") && (Vector2.Distance(fillScript.backBoxes[i].transform.position, northBox.position) <= inputLength) && (Random.Range(0, probabNum) == 0))
                {
                    fillScript.backBoxes[i].GetComponent<TextMeshProUGUI>().color = brightGreen;
                }
                else if (inputText.text.StartsWith("W") && (Vector2.Distance(fillScript.backBoxes[i].transform.position, westBox.position) <= inputLength) && (Random.Range(0, probabNum) == 0))
                {
                    fillScript.backBoxes[i].GetComponent<TextMeshProUGUI>().color = brightGreen;
                }
                else if (inputText.text.StartsWith("E") && (Vector2.Distance(fillScript.backBoxes[i].transform.position, eastBox.position) <= inputLength) && (Random.Range(0, probabNum) == 0))
                {
                    fillScript.backBoxes[i].GetComponent<TextMeshProUGUI>().color = brightGreen;
                }
                else if (inputText.text.StartsWith("S") && (Vector2.Distance(fillScript.backBoxes[i].transform.position, southBox.position) <= inputLength) && (Random.Range(0, probabNum) == 0))
                {
                    fillScript.backBoxes[i].GetComponent<TextMeshProUGUI>().color = brightGreen;
                }
            }
        }
    }
}
