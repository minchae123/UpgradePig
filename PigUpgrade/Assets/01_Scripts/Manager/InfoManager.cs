using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PigData // 데이터
{
	public int level;
	public int percentage;
	public int purchase;
	public int price;
	public string pigName;
}

[System.Serializable]
public class PigAllData
{
	public PigData[] pig;
}

public class InfoManager : MonoBehaviour
{
	public static InfoManager Instance;

	public TextAsset json; // 데이터 불러오기

	public PigAllData data;
	public PigImageSO pigSprite;

	private void Awake()
	{
		if(Instance != null)
		{
			print("InfoManager Error");
		}
		Instance = this;
		data = JsonUtility.FromJson<PigAllData>(json.text);
	}

	public Sprite ChangeImage(int level)
	{
		return pigSprite.sprites[level];
	}
}