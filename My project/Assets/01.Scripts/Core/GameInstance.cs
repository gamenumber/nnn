using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameInstance : MonoBehaviour
{
	public static GameInstance instance;
	public float GameStartTime = 0f;
	public int Score = 0;
	public int CurrentStageLevel = -1;

	public int CurrentPlayerWeaponLevel = 0;
	public int CurrentPlayerHP = 3;
	public float CurrentPlayerFuel = 100f;
	public int CurrentPlayerAddOnCount = 0;


	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(gameObject);
		}
		GameStartTime = Time.time;
	}
}
