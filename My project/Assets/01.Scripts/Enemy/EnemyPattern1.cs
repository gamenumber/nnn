using System.Collections;
using UnityEngine;

public class EnemyPattern1 : MonoBehaviour
{
	public float MoveSpeed = 4f;
	public float Amplitude; // ������ ����(���Ʒ� �̵� �Ÿ�)
	public float StopDuration = 3f; // �������� ���ߴ� �Ⱓ
	public float OriginMoveSpeed = 4f;

	private bool movingUp = true;
	private Vector3 startPosition;

	public bool isice = false;

	void Start()
	{
		startPosition = transform.position;

	}

	void Update()
	{
		if (!isice)
		{
			float verticalMovement = MoveSpeed * Time.deltaTime;

			// ���� �̵� ���̸鼭 ���� ��ġ�� ���� ��ġ���� �������� ���� ���
			if (movingUp && transform.position.x < startPosition.x + Amplitude)
			{
				transform.position += new Vector3(verticalMovement, 0f, 0f);
			}
			// �Ʒ��� �̵� ���̸鼭 ���� ��ġ�� ���� ��ġ���� �������� ū ���
			else if (!movingUp && transform.position.x > startPosition.x - Amplitude)
			{
				transform.position -= new Vector3(verticalMovement, 0f, 0f);
			}
			// ���� ������ ��� ��� �̵� ������ �ݴ�� ����
			else
			{
				movingUp = !movingUp;
			}

			transform.position -= new Vector3(0f, MoveSpeed * Time.deltaTime, 0f);
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