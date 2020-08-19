using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameScene
{
	public string sceneNum;
	public string description;

	public string northValue;
	public string westValue;
	public string eastValue;
	public string southValue;
}

[System.Serializable]
public class GameScenes
{
	public GameScene[] gameScenes;
}

public class JSONReader : MonoBehaviour
{
	public TextAsset jsonFile;

	void Start()
	{
		//GameScenes allScenes = JsonUtility.FromJson<GameScenes>(jsonFile.text);
		Debug.Log(jsonFile.text);

		/*
		foreach (GameScene i in allScenes.gameScenes)
		{
			Debug.Log("Found scenes: " + i.sceneNum + " " + i.description + " " + i.northValue + " " + i.westValue + " " + i.eastValue + " " + i.southValue);
		}
        */
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
