using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//object class for individual scenes
[System.Serializable]
public class GameScene
{
	public string sceneNum, description, northValue, westValue, eastValue, southValue;
}

//object class for array of scenes
[System.Serializable]
public class GameScenes
{
	public GameScene[] gameScenes;
}

public class JSONReader : MonoBehaviour
{
	public TextAsset jsonFile; //JSON file to read from
	public TextMeshProUGUI descriptionText, inputText, northText, westText, eastText, southText; //text boxes to write to
	public string sceneNum = "0"; //current scene number ***VERY IMPORTANT VARIABLE***
	public float typeSpeed;

	private GameScenes allScenes; //access to object classes so that we can read from them

	private void Start()
	{
		allScenes = JsonUtility.FromJson<GameScenes>(jsonFile.text); //getting the info from the file

		loadScene(); //pretty self-explanitory...
	}

	//loads the correct text from the JSON file based on the current scene number
	public void loadScene()
	{
		foreach (GameScene i in allScenes.gameScenes)
		{
			if (i.sceneNum == sceneNum)
			{
				descriptionText.text = i.description;
				StartCoroutine(typeText(descriptionText, true));

				northText.text = i.northValue;
				StartCoroutine(typeText(northText, false));

				westText.text = i.westValue;
				StartCoroutine(typeText(westText, false));

				eastText.text = i.eastValue;
				StartCoroutine(typeText(eastText, false));

				southText.text = i.southValue;
				StartCoroutine(typeText(southText, false));
			}
		}
	}

	//clears the given TMP text, then retypes it
	IEnumerator typeText(TextMeshProUGUI givenTMPro, bool isDescription)
	{
		string tempString = givenTMPro.text;
		givenTMPro.text = "";

		for (int i = 0; i < tempString.Length; i++)
		{
			givenTMPro.text += tempString[i].ToString();

			//if the TMP happens to be the description box, it types it 5 times as fast
			if (isDescription == true)
			{
				yield return new WaitForSeconds(typeSpeed / 5);
			}
			else
			{
				yield return new WaitForSeconds(typeSpeed);
			}
		}
	}

	/*
    {
        "sceneNum": "",
        "description": "",
        "northValue": "",
        "westValue": "",
        "eastValue": "",
        "southValue": ""
    }
    */
}
