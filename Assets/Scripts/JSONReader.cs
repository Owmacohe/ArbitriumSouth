﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class GameScene
{
	public string sceneNum, description, northValue, westValue, eastValue, southValue;
}

[System.Serializable]
public class GameScenes
{
	public GameScene[] gameScenes;
}

public class JSONReader : MonoBehaviour
{
	public TextAsset jsonFile;
	public TextMeshProUGUI descriptionText, inputText, northText, westText, eastText, southText;

	private GameScenes allScenes;

	void Start()
	{
		allScenes = JsonUtility.FromJson<GameScenes>(jsonFile.text);

		loadScene("0");
		//Debug.Log(allScenes);

		/*
		foreach (GameScene i in allScenes.gameScenes)
		{
			Debug.Log("Found scenes: " + i.sceneNum + " " + i.description + " " + i.northValue + " " + i.westValue + " " + i.eastValue + " " + i.southValue);
		}
        */
	}

	void loadScene(string givenSceneNum)
	{
		foreach (GameScene i in allScenes.gameScenes)
		{
			if (i.sceneNum == givenSceneNum)
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