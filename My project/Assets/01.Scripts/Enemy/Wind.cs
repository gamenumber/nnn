using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
	public float windForce = 10f; // 바람이 가해지는 힘의 크기

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player")) // 플레이어 테그를 사용하여 충돌을 확인
		{

			Transform playerTransform = other.transform;
			// 랜덤한 방향으로 플레이어의 위치를 직접 이동시킴
			Vector3 randomWindDirection = Random.insideUnitCircle.normalized;
			Vector3 newPosition = playerTransform.position + randomWindDirection * windForce * Time.fixedDeltaTime;
			playerTransform.position = newPosition;
		}
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
