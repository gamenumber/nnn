using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern4 : MonoBehaviour
{
	public bool isice = false;
	public float StopDuration = 0.2f; // 움직임을 멈추는 기간

	// 이동 속도
	public float MoveSpeed;

	// 공격 중지 시간
	public float AttackStopTime;

	// 이동 시간
	public float MoveTime;

	// 발사할 총알
	public GameObject Projectile;

	// 총알의 이동 속도
	public float ProjectileMoveSpeed;

	// 공격 중 여부
	private bool _isAttack = false;

	// 이동 방향 (1: 오른쪽, -1: 왼쪽)
	private int _moveDirection = 1;

	void Update()
	{
			Move();
	}

	

	// 적의 이동 메서드
	void Move()
	{
		if (!isice)
		{
			// 현재 x 좌표를 기준으로 새로운 x 좌표 계산
			float newX = transform.position.x + _moveDirection * MoveSpeed * Time.deltaTime;

			// 화면 경계를 체크하여 반전
			float halfWidth = transform.localScale.x / 2f; // 적의 크기를 고려하여 가로 길이의 반을 계산
			float screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
			float maxX = screenWidth / 2f - halfWidth;
			float minX = -maxX;

			// 화면을 벗어날 위험이 있다면 이동 방향을 반전
			if (newX >= maxX || newX <= minX)
			{
				_moveDirection *= -1;
			}

			// 계산된 위치로 이동
			transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		}

	}

	IEnumerator Stop()
	{
		isice = true;
		// SpriteRenderer 컴포넌트를 얻습니다.
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		// 색을 하늘색으로 변경합니다. RGB 값으로 (135, 206, 235)를 사용합니다.
		spriteRenderer.color = new Color(135f / 255f, 206f / 255f, 235f / 255f);

		yield return new WaitForSeconds(StopDuration);

		isice = false;
		spriteRenderer.color = Color.white;

		yield return null;
	}

	public void Ice()
	{
		StartCoroutine(Stop());
	}
}