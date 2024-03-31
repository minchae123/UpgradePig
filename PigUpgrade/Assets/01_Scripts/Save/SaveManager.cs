using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	private GameData gameData;
	private string savePath;
	private string saveFileName = "/SaveFile.txt";

	public bool saveDir = false;

	private void Awake()
	{
		gameData = new GameData();
		savePath = Application.dataPath + "/SaveData/";
		saveDir = Directory.Exists(savePath);

		if (!saveDir)
		{
			Directory.CreateDirectory(savePath);
		}
	}

	private void Start()
	{
		
	}

	public void Save() // jSOn 저장
	{
		string json = JsonUtility.ToJson(gameData);
		File.WriteAllText(savePath + saveFileName, json);
	}


	public void Load()
	{
		if (File.Exists(savePath + saveFileName))
		{
			string loadJson = File.ReadAllText(savePath + saveFileName);
			gameData = JsonUtility.FromJson<GameData>(loadJson);
			Debug.Log("저장 성공 !");
		}
		else
		{
			Debug.Log("저장 오류 !!!");
		}
	}


	public GameData LoadGame()
	{
		Load();
		return gameData;
	}

	public void SaveGame(ulong coin, int prevent)
	{
		gameData.coin = coin;
		gameData.prevent = prevent;

		Save();
	}

	public void ResetData()
	{
		print("re");
		gameData.coin = 1000000;
		gameData.prevent = 0;
		Save();
	}

	public void DeleteData()
	{
		Directory.Delete(savePath, true);
	}
}
