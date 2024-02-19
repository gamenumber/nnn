using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	static public GameManager Instance;

	public MapManager MapManager;
	public EnemySpawnManager EnemySpawnManager;
	public ItemManager ItemManager;
	public SoundManager SoundManager;

	public Canvas StageResultCanvas;
	public TMP_Text CurrentScoreText;
	public TMP_Text TimeText;

	[HideInInspector] public bool bStageCleared = false;

	void Start()
	{
		SoundManager.instance.PlayBGM("BGM1");
		if (MapManager)
			MapManager.Init(this);

		if (EnemySpawnManager)
			EnemySpawnManager.Init(this);

		if (SoundManager)
			SoundManager.Init(this);

	}


	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
			Destroy(this.gameObject);
	}

	public PlayerCharacter GetPlayerCharacter()
	{
		return GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
	}

	public void InitInstance()
	{
		GameInstance.instance.GameStartTime = 0f;
		//GameInstance.instance.Score = 0;
		GameInstance.instance.CurrentStageLevel = 1;
		GameInstance.instance.CurrentPlayerWeaponLevel = 0;
		GameInstance.instance.CurrentPlayerHP = 1000;
		GameInstance.instance.CurrentPlayerFuel = 100f;
		GameInstance.instance.CurrentPlayerAddOnCount = 0;
	}

	public void GameStart()
	{
		SceneManager.LoadScene("Stage1");
	}

	public void EnemyDies()
	{
		AddScore(10);
	}

	public void StageClear()
	{
		SoundManager.instance.PlaySFX("StageClear");
		AddScore(500);

		float gameStartTime = GameInstance.instance.GameStartTime;
		int score = GameInstance.instance.Score;
		int elapsedTime = Mathf.FloorToInt(Time.time - gameStartTime);

		StageResultCanvas.gameObject.SetActive(true);
		CurrentScoreText.text = "CurrentScore : " + score;
		TimeText.text = "ElapsedTime : " + elapsedTime;
		bStageCleared = true;
		StartCoroutine(LoadNextStageAfterDelay(5f));
	}

	IEnumerator LoadNextStageAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);

		switch (GameInstance.instance.CurrentStageLevel)
		{
			case 1:

				GameInstance.instance.CurrentStageLevel = +1;
				SceneManager.LoadScene("Stage2");
				
				break;

			case 2:
				SceneManager.LoadScene("Result");
				break;
		}
	}

	public void AddScore(int score)
	{
		GameInstance.instance.Score += score;
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.F1))
		{
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach (GameObject obj in enemies)
			{
				Enemy enemy = obj?.GetComponent<Enemy>();
				enemy?.Dead();
			}
		}


		if (Input.GetKeyUp(KeyCode.F2))
		{
			GetPlayerCharacter().CurrentWeaponLevel = 5;
			GameInstance.instance.CurrentPlayerWeaponLevel = GetPlayerCharacter().CurrentWeaponLevel;
		}


		if (Input.GetKeyUp(KeyCode.F3))
		{
			GetPlayerCharacter().InitSkillCoolDown();
		}


		if (Input.GetKeyUp(KeyCode.F4))
		{
			GetPlayerCharacter().GetComponent<PlayerHpSystem>().InitHealth();
		}


		if (Input.GetKeyUp(KeyCode.F5))
		{
			GetPlayerCharacter().GetComponent<PlayerFuelSystem>().InitFuel();
		}


		if (Input.GetKeyUp(KeyCode.F6))
		{
			StageClear();
			GameInstance.instance.CurrentStageLevel += 1;
		}
	}

}