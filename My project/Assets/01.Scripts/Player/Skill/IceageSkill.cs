using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 몬스터를 얼리는 스킬을 구현한 클래스
public class IceageSkill : BaseSkill
{
	public float freezeDuration = 3f;

	// 실제로 구현된 코드
	public override void Activate()
	{
		Debug.Log("아이스에이지 activated");
		base.Activate();

		// 모든 Enemy 찾기
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		Debug.Log("Number of enemies found: " + enemies.Length);

		foreach (GameObject obj in enemies)
		{
			if (obj != null)
			{
				Debug.Log("Calling IceEnemy for enemy: " + obj.name);
				IceEnemy(obj);
			}
		}
	}

	public void IceEnemy(GameObject enemy)
	{
		EnemyPattern1 e1 = enemy.GetComponent<EnemyPattern1>();
		EnemyPattern2 e2 = enemy.GetComponent<EnemyPattern2>();
		EnemyPattern3 e3 = enemy.GetComponent<EnemyPattern3>();
		EnemyPattern4 e4 = enemy.GetComponent<EnemyPattern4>();

		if (e1 != null)
		{
			Debug.Log("IceEnemy: Freezing " + enemy.name + " with EnemyPattern1");
			e1.Ice();
		}
		else if (e2 != null)
		{
			Debug.Log("IceEnemy: Freezing " + enemy.name + " with EnemyPattern2");
			e2.Ice();
		}
		else if (e3 != null)
		{
			Debug.Log("IceEnemy: Freezing " + enemy.name + " with EnemyPattern3");
			e3.Ice();
		}
		else if (e4 != null)
		{
			Debug.Log("IceEnemy: Freezing " + enemy.name + " with EnemyPattern4");
			e4.Ice();
		}
		else
		{
			Debug.Log("IceEnemy: No matching enemy pattern found for " + enemy.name);
		}
	}



}