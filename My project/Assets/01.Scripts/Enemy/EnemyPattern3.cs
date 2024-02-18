using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern3 : MonoBehaviour
{
	// �� �̵� �ӵ�
	public float MoveSpeed;

	public bool isice = false;
	public float StopDuration = 3f; // �������� ���ߴ� �Ⱓ

	// �÷��̾��� Transform
	private Transform playerTransform;

	// �̵� ����
	private Vector2 _moveDirection;

	void Start()
	{
		// �÷��̾ �±׸� ������� ã��
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

		// ���� �̵� �޼��带 �ڷ�ƾ���� ����
		StartCoroutine(MoveTowardsPlayer());
	}

	void Update()
	{
		// �� �̵� �޼��� ȣ��
		Move();
	}

	// �÷��̾� �������� ���� �ֱ⸶�� �̵��ϴ� �ڷ�ƾ
	IEnumerator MoveTowardsPlayer()
	{
		while (true)
		{
			// �÷��̾��� ��ġ�� �������� ���� �̵� ���� ����
			_moveDirection = (playerTransform.position - transform.position).normalized;

			// 0.5�ʸ��� �÷��̾��� ��ġ�� ����
			yield return new WaitForSeconds(0.5f);
		}
	}

	// �� �̵� �޼���
	void Move()
	{
		if (!isice)
		{
			// ���� ��ġ�� �������� �̵� ���� ���ο� ��ġ ���
			Vector2 newPosition = new Vector2(
				transform.position.x + _moveDirection.x * MoveSpeed * Time.deltaTime,
				transform.position.y + _moveDirection.y * MoveSpeed * Time.deltaTime
			);

			// ���� ��ġ�� �̵�
			transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
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