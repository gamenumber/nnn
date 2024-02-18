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
	public float StopDuration = 3f; // �������� ���ߴ� �Ⱓ

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

				yield return new WaitForSeconds(AttackStopTime); // 1�� ��ٸ�

				_isAttack = false;

				yield return new WaitForSeconds(MoveTime); // 3�� ���� ������
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
		// SpriteRenderer ������Ʈ�� ����ϴ�.
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		// SpriteRenderer�� ������, �� ��ũ��Ʈ�� �����մϴ�.
		if (spriteRenderer == null)
		{
			Debug.LogError("No SpriteRenderer component found on " + gameObject.name);

		}
		// ���� �ϴû����� �����մϴ�. RGB ������ (135, 206, 235)�� ����մϴ�.
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