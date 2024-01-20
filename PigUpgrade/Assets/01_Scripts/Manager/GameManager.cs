using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public ulong Coin;
	public int curLevel = 1;

	private UIManager uiManager;
	private InfoManager info;

	private void Awake()
	{
		if (Instance != null)
			print("GameManager Error");

		Instance = this;

		uiManager = FindObjectOfType<UIManager>();
		info = FindObjectOfType<InfoManager>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
			CheckPercentage(50);
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
			curLevel = 1;
			Down();
		}
	}

	private void Up(int level)
	{
		Sprite s = info.pigSprite.sprites[curLevel];
		string name = info.data.pig[curLevel].pigName;
		int percentage = info.data.pig[curLevel].percentage;
		int price = info.data.pig[curLevel].price;
		int pur = info.data.pig[curLevel].purchase;
		if((ulong)pur <= Coin)
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
		int price = info.data.pig[curLevel - 1].price; // 가격을 가져와서
		Coin += (ulong)price; // 더해주고
		curLevel = 1; // 레벨 초기화
		Down();
	}

	private bool CheckPercentage(int percentage)
	{
		int rand = Random.Range(20, 100);
		return rand < percentage ? true : false;
	}
}
