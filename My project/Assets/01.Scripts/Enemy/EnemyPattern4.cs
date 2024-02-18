using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern4 : MonoBehaviour
{
	public bool isice = false;
	public float StopDuration = 0.2f; // �������� ���ߴ� �Ⱓ

	// �̵� �ӵ�
	public float MoveSpeed;

	// ���� ���� �ð�
	public float AttackStopTime;

	// �̵� �ð�
	public float MoveTime;

	// �߻��� �Ѿ�
	public GameObject Projectile;

	// �Ѿ��� �̵� �ӵ�
	public float ProjectileMoveSpeed;

	// ���� �� ����
	private bool _isAttack = false;

	// �̵� ���� (1: ������, -1: ����)
	private int _moveDirection = 1;

	void Update()
	{
			Move();
	}

	

	// ���� �̵� �޼���
	void Move()
	{
		if (!isice)
		{
			// ���� x ��ǥ�� �������� ���ο� x ��ǥ ���
			float newX = transform.position.x + _moveDirection * MoveSpeed * Time.deltaTime;

			// ȭ�� ��踦 üũ�Ͽ� ����
			float halfWidth = transform.localScale.x / 2f; // ���� ũ�⸦ ����Ͽ� ���� ������ ���� ���
			float screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
			float maxX = screenWidth / 2f - halfWidth;
			float minX = -maxX;

			// ȭ���� ��� ������ �ִٸ� �̵� ������ ����
			if (newX >= maxX || newX <= minX)
			{
				_moveDirection *= -1;
			}

			// ���� ��ġ�� �̵�
			transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		}

	}

	IEnumerator Stop()
	{
		isice = true;
		// SpriteRenderer ������Ʈ�� ����ϴ�.
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
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