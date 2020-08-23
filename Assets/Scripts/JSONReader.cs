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
				northText.text = i.northValue;
				westText.text = i.westValue;
				eastText.text = i.eastValue;
				southText.text = i.southValue;
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
