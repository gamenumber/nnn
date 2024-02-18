using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern2 : MonoBehaviour
{
	public float MoveSpeed;
	public float AttackStopTime;
	public float MoveTime;
	public GameObject Projectile;
	public float ProjectileMoveSpeed;

	public bool isice = false;
	public float StopDuration = 3f; // 움직임을 멈추는 기간

	private bool _isAttack = false;

	void Start()
	{
		StartCoroutine(Attack());
	}

	void Update()
	{
		if (false == _isAttack)
			Move();
	}

	IEnumerator Attack()
	{
		if (!isice)
		{
			while (true)
			{
				yield return new WaitForSeconds(1f); 

				Vector3 playerPos = GameManager.Instance.GetPlayerCharacter().GetComponent<Transform>().position;
				Vector3 direction = playerPos - transform.position;
				direction.Normalize();

				var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
				projectile.GetComponent<Projectile>().SetDirection(direction);
				projectile.GetComponent<Projectile>().MoveSpeed = ProjectileMoveSpeed;

				_isAttack = true;

				yield return new WaitForSeconds(AttackStopTime); // 1초 기다림

				_isAttack = false;

				yield return new WaitForSeconds(MoveTime); // 3초 동안 움직임
			}
		}

	}

	void Move()
	{
		if (!isice)
		{
			transform.position -= new Vector3(0f, MoveSpeed * Time.deltaTime, 0f);
		}

	}
	IEnumerator Stop()
	{
		isice = true;
		// SpriteRenderer 컴포넌트를 얻습니다.
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		// SpriteRenderer가 없으면, 이 스크립트를 종료합니다.
		if (spriteRenderer == null)
		{
			Debug.LogError("No SpriteRenderer component found on " + gameObject.name);

		}
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