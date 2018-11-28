using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager {

	public static SFXPlayer player;

	public static void PlayEffect(float time)
	{
		if (player != null)
			{
			player.PlayNew(time);
		}
	}

	public static void SetPlayer(SFXPlayer p)
	{
		player = p;
	}
}
