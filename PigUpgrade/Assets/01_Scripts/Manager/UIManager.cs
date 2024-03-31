using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	private SaveManager saveManager;

	private InfoManager info;

	[SerializeField] private Image pigImage;

	[SerializeField] private TextMeshProUGUI coinTxt;
	[SerializeField] private TextMeshProUGUI levelTxt;
	[SerializeField] private TextMeshProUGUI nameTxt;
	[SerializeField] private TextMeshProUGUI percentageTxt;
	[SerializeField] private TextMeshProUGUI priceTxt;
	[SerializeField] private TextMeshProUGUI purchaseTxt;
	[SerializeField] private TextMeshProUGUI preventTxt;
	[SerializeField] private TextMeshProUGUI preventAmountTxt;
	[SerializeField] private TextMeshProUGUI poorTxt;

	[SerializeField] private GameObject failPanel;
	[SerializeField] private GameObject successShow;

	[SerializeField] private GameObject escPanel;

	[SerializeField] private Button continueBtn;

	private int escClick = 0;

	private void Awake()
	{
		if (Instance != null)
			print("UIManager Error");

		Instance = this;

		info = FindObjectOfType<InfoManager>();
		saveManager = FindObjectOfType<SaveManager>();
	}

	private void Start()
	{
		if(info != null) // 게임 씬에서 쓰고 싶음
		{
			Sprite s = info.pigSprite.sprites[0];
			string name = info.data.pig[0].pigName;
			int percentage = info.data.pig[0].percentage;
			int price = info.data.pig[0].price;
			int pur = info.data.pig[0].purchase;

			ChangeUI(s, 1, GameManager.Instance.Coin, name, percentage, price, pur);
		}

		if (continueBtn != null) // 스타트씬에서 쓰고싶음
		{
			if (saveManager.saveDir == false)
				continueBtn.interactable = false;
			else
				continueBtn.interactable = true;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			escClick++;
			if (!IsInvoking("DoubleClick")) // 한번 누르면
			{
				if(escPanel != null)
					escPanel.SetActive(true);
				Invoke("DoubleClick", 1.0f);
			}

		}
		else if (escClick == 2) // 더블 클릭
		{
			CancelInvoke("DoubleClick");
			QuitGame();
		}
	}

	void DoubleClick()
	{
		escClick = 0;
	}


	public void ChangeUI(Sprite sp, int level, ulong coin, string name, int per, int price, int pur)
	{
		pigImage.sprite = sp;
		levelTxt.text = $"현재 레벨 : {level}";
		coinTxt.text = $"가지고 있는 코인 : {coin}";
		nameTxt.text = $"{name}";
		percentageTxt.text = $"강화 성공 확률 : {per}%";
		priceTxt.text = $"돼지 판매 코인 : {price}";
		purchaseTxt.text = $"업그레이드에 필요한 코인: \n{pur}";
	}

	public void CoinChange(int coin)
	{
		coinTxt.text = $"가지고 있는 코인 : {coin}";
	}

	public void FailPanel(bool value)
	{
		failPanel.SetActive(value);
	}

	public void PreventTxt(int amount)
	{
		preventAmountTxt.text = $"방지권 개수 : {amount}";
	}

	public IEnumerator ShowAndHidePrevent()
	{
		preventTxt.gameObject.SetActive(true);
		yield return new WaitForSeconds(1);
		preventTxt.gameObject.SetActive(false);
	}

	public IEnumerator ShowAndHideSuccess()
	{
		successShow.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		successShow.SetActive(false);
	}

	public IEnumerator PoorShow()
	{
		poorTxt.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.6f);
		poorTxt.gameObject.SetActive(false);
	}

	public void LoadScene(int num)
	{
		SceneManager.LoadScene(num);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}