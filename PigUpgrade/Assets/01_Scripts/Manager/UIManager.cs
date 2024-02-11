using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	private InfoManager info;

	[SerializeField] private Image pigImage;

	[SerializeField] private TextMeshProUGUI coinTxt;
	[SerializeField] private TextMeshProUGUI levelTxt;
	[SerializeField] private TextMeshProUGUI nameTxt;
	[SerializeField] private TextMeshProUGUI percentageTxt;
	[SerializeField] private TextMeshProUGUI priceTxt;
	[SerializeField] private TextMeshProUGUI purchaseTxt;
	[SerializeField] private TextMeshProUGUI preventTxt;

	[SerializeField] private GameObject failPanel;

	private void Awake()
	{
		if(Instance!= null)
			print("UIManager Error");

		Instance = this;

		info = FindObjectOfType<InfoManager>();
	}

	private void Start()
	{
		Sprite s = info.pigSprite.sprites[0];
		string name = info.data.pig[0].pigName;
		int percentage = info.data.pig[0].percentage;
		int price = info.data.pig[0].price;
		int pur = info.data.pig[0].purchase;

		ChangeUI(s, 1, GameManager.Instance.Coin, name, percentage, price, pur);
	}

	public void ChangeUI(Sprite sp, int level, ulong coin, string name, int per, int price, int pur)
	{
		pigImage.sprite = sp;
		levelTxt.text = $"현재 레벨 : {level}";
		coinTxt.text = $"가지고 있는 코인 : {coin}";
		nameTxt.text = $"{name}";
		percentageTxt.text = $"강화 성공 확률 : {per}%";
		priceTxt.text = $"돼지 판매 코인 : {price}";
		purchaseTxt.text = $"업그레이드에 필요한 코인: {pur}";
	}

	public void CoinChange(int coin)
	{
		coinTxt.text = $"가지고 있는 코인 : {coin}";
	}

	public void FailPanel(bool value)
	{
		failPanel.SetActive(value);
	}

	IEnumerator ShowAndHidePrevent()
	{
		preventTxt.gameObject.SetActive(true);
		yield return new WaitForSeconds(1);
		preventTxt.gameObject.SetActive(false);
	}
}
