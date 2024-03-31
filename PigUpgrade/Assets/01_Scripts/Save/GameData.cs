using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
	public ulong coin; // 코인
	public int prevent; // 방지권

	public GameData()
	{
		coin = 1000000;
		prevent = 0;
	}
}
