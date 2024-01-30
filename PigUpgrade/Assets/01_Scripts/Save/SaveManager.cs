using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private GameData gameData;
    private string savePath;
    private string saveFileName = "/SaveFile.txt";

    private void Start()
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
            Debug.Log("�ε� ���� !");
        }
        else
        {
            Debug.Log("���� �ȴ� !!!");
        }
    }


    public void LoadGame()
	{

	}

	public void SaveGame()
	{

	}
}
