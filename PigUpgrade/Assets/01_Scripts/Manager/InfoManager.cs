using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PigData
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
	public TextAsset json;

	public PigAllData data;

	private void Awake()
	{
		print(json.text);
		data = JsonUtility.FromJson<PigAllData>(json.text);
	}
}
