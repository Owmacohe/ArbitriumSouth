//***************************************************************************
//
//Design, programming, and art by: Owen Hellum
//Alpha "completed" as of 17/09/2020
//Visit Arbitrium South's itch.io page at: https://omch.itch.io/arbitrium
//
//***************************************************************************

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
	public string sceneNum; //current scene number ***VERY IMPORTANT VARIABLE***
	public string startingNum = "0"; //lets the game know what scene to start on
	public float typeSpeed; //the speed at which the letters of the scenes type themselves

	private GameScenes allScenes; //access to object classes so that we can read from them

	private bool isApplicationQuitting = false;

	//when the game stops for real, it clears the saved sceneNum
	private void OnApplicationQuit()
	{
		isApplicationQuitting = true;
		PlayerPrefs.SetString("sceneNum", startingNum);
	}

	//saves the sceneNum between scene reloads mid game
	private void OnDisable()
	{
		if (isApplicationQuitting) { return; }

		PlayerPrefs.SetString("sceneNum", sceneNum);
	}

	//gets the sceneNum when the scene is reloaded mid game
	private void OnEnable()
	{
		if (PlayerPrefs.GetString("sceneNum") == null)
		{
			PlayerPrefs.SetString("sceneNum", startingNum);
		}

		sceneNum = PlayerPrefs.GetString("sceneNum");
	}

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
