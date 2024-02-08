using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private GameData gameData;
    private string savePath;
    private string saveFileName = "/SaveFile.txt";

    private void Awake()
    {
        gameData = new GameData();
        savePath = Application.dataPath + "/SaveData/";

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(savePath + saveFileName, json);
        Debug.Log(json);
    }


    public void Load()
    {
        if (File.Exists(savePath + saveFileName))
        {
            string loadJson = File.ReadAllText(savePath + saveFileName);
            gameData = JsonUtility.FromJson<GameData>(loadJson);
            Debug.Log("로드 성공 !");
        }
        else
        {
            Debug.Log("저장 안댐 !!!");
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
}
