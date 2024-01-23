using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
	public void UpgradeBtn()
	{
		GameManager.Instance.Upgrade();
	}

	public void SellBtn()
	{
		GameManager.Instance.Sell();
	}

	
}
