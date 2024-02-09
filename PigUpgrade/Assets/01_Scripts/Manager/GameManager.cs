using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public ulong Coin;
	public int curLevel = 1;
	public int prevent;

	private UIManager uiManager;
	private SaveManager saveManager;
	private InfoManager info;

	private GameData gameData;

	private void Awake()
	{
		if (Instance != null)
			print("GameManager Error");

		Instance = this;

		uiManager = FindObjectOfType<UIManager>();
		saveManager = FindObjectOfType<SaveManager>();
		info = FindObjectOfType<InfoManager>();
	}

	private void Start()
	{
		gameData = saveManager.LoadGame();

		Coin = gameData.coin;
	}

	private void Update()
	{

	}

	public void Upgrade()
	{
		int per = info.data.pig[curLevel - 1].percentage;

		if (CheckPercentage(per))
		{
			Up(curLevel);
		}
		else
		{
			FailUpgrade();
		}
	}

	private void FailUpgrade()
	{
		uiManager.FailPanel(true);
	}

	public void RetryPass()
	{
		Down();
		curLevel = 1;
		uiManager.FailPanel(false);
	}

	public void Retry()
	{
		print(curLevel);
		if (prevent > 0)
		{
			uiManager.FailPanel(false);
			prevent--;
		}
	}


	private void Up(int level)
	{
		Sprite s = info.pigSprite.sprites[curLevel];
		string name = info.data.pig[curLevel].pigName;
		int percentage = info.data.pig[curLevel].percentage;
		int price = info.data.pig[curLevel].price;
		int pur = info.data.pig[curLevel].purchase;
		if ((ulong)pur <= Coin)
		{
			curLevel++;
			Coin -= (ulong)pur;
			uiManager.ChangeUI(s, curLevel, Coin, name, percentage, price, pur);
		}
	}

	private void Down()
	{
		Sprite s = info.pigSprite.sprites[0];
		string name = info.data.pig[0].pigName;
		int percentage = info.data.pig[0].percentage;
		int price = info.data.pig[0].price;
		int pur = info.data.pig[0].purchase;
		uiManager.ChangeUI(s, curLevel, Coin, name, percentage, price, pur);
	}


	public void Sell()
	{
		int price = info.data.pig[curLevel - 1].price;
		Coin += (ulong)price;
		curLevel = 1;
		Down();
	}

	private bool CheckPercentage(int percentage)
	{
		int rand = Random.Range(0, 100);
		return rand < percentage ? true : false;
	}

	private void OnApplicationQuit()
	{
		saveManager.SaveGame(Coin, prevent);
	}

	public void PurchasePrevent()
	{
		// 뭐로 구매할까요
		prevent++;
	}

	private void PreventDrop()
	{
		int rand = Random.Range(1, 100);


	}
}
