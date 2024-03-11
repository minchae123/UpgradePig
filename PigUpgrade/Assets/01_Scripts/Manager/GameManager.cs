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
		prevent = gameData.prevent;

		uiManager.PreventTxt(prevent);
	}

	public void Upgrade() // 업그레이드 버튼 누르면 확률에 따라 성공 OR 실패
	{
		int per = info.data.pig[curLevel - 1].percentage;

		if(curLevel < 18)
		{
			if (CheckPercentage(per))
			{
				Up(curLevel);
			}
			else
			{
				FailUpgrade();
			}
		}
		else
		{
			print("마지막 레벨입니다");
		}
	}

	private void FailUpgrade() // 실패 하면 실패 패널 띄우기 
	{
		uiManager.FailPanel(true);
	}

	public void RetryPass() // 다시 1렙으로 가기
	{
		Down();
		curLevel = 1;
		uiManager.FailPanel(false);
	}

	public void Retry() // 그 레벨에서 다시 도전하기(단, 방지권이 있는 경우에만)
	{
		if (prevent > 0)
		{
			uiManager.FailPanel(false);
			prevent--;
		}
	}

	private void Up(int level) // 1단계 레벨업 하면 글씨 셋팅해서 ui 변경하기
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
			PreventDrop();
			StartCoroutine(uiManager.ShowAndHideSuccess());
			uiManager.ChangeUI(s, curLevel, Coin, name, percentage, price, pur);
		}
	}

	private void Down() // 실패하면 1레벨로 ui 세팅 변경
	{
		Sprite s = info.pigSprite.sprites[0];
		string name = info.data.pig[0].pigName;
		int percentage = info.data.pig[0].percentage;
		int price = info.data.pig[0].price;
		int pur = info.data.pig[0].purchase;
		uiManager.ChangeUI(s, curLevel, Coin, name, percentage, price, pur);
	}


 	public void Sell() // 팔면 돈 들어오고 1레벨로 변경
	{
		int price = info.data.pig[curLevel - 1].price;
		Coin += (ulong)price;
		curLevel = 1;
		Down();
	}

	private bool CheckPercentage(int percentage) // 확률 검사
	{
		int rand = Random.Range(0, 100);
		return rand < percentage ? true : false;
	}

	private void OnApplicationQuit() // 게임 종료 시 저장
	{
		saveManager.SaveGame(Coin, prevent);
	}

	private void PreventDrop() // 5프로 확률로 방지권 드랍하기
	{
		int rand = Random.Range(1, 100);
		
		if(rand > 95)
		{
			StartCoroutine(uiManager.ShowAndHidePrevent());
			prevent++;	
			uiManager.PreventTxt(prevent);
		}
	}
}
