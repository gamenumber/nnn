using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : BaseManager
{
	public GameObject[] Enemys;
	public Transform[] EnemySpawnTransform;
	public float CoolDownTime;
	public int MaxSpawnEnemyCount;

	private int _spawnCount = 0;
	public int BossSpawnCount = 10;

	private bool _bSpawnBoss = false;

	public GameObject BossA;

	public override void Init(GameManager gameManager)
	{
		base.Init(gameManager);
		StartCoroutine(SpawnEnemy());
	}

	IEnumerator SpawnEnemy()
	{
		while (!_bSpawnBoss)
		{
			yield return new WaitForSeconds(CoolDownTime);
			int spawnCount = Random.Range(0, EnemySpawnTransform.Length);
			List<int>avaliablePosition = new List<int>(EnemySpawnTransform.Length);

			for (int i = 0; i < EnemySpawnTransform.Length; i++)
			{
				avaliablePosition.Add(i);
			}

			for (int i = 0; i < spawnCount; i++)
			{
				int randome = Random.Range(0, Enemys.Length);
				int randomindex = Random.Range(0, avaliablePosition.Count);
				int randomp = avaliablePosition[randomindex];
				avaliablePosition.RemoveAt(randomindex);
				Instantiate(Enemys[randome], EnemySpawnTransform[randomp].position, Quaternion.identity);
			}

			_spawnCount += spawnCount;

			if (_spawnCount > BossSpawnCount)
			{
				_bSpawnBoss = true;
				Instantiate(BossA, new Vector3(EnemySpawnTransform[1].position.x, EnemySpawnTransform[1].position.y + 1, 0), Quaternion.identity);
			}
		}
	}
}