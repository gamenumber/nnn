using System.Collections;
using UnityEngine;

public class EnemyPattern1 : MonoBehaviour
{
	public float MoveSpeed = 4f;
	public float Amplitude; // 패턴의 진폭(위아래 이동 거리)
	public float StopDuration = 3f; // 움직임을 멈추는 기간
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

			// 위로 이동 중이면서 현재 위치가 시작 위치보다 진폭보다 작은 경우
			if (movingUp && transform.position.x < startPosition.x + Amplitude)
			{
				transform.position += new Vector3(verticalMovement, 0f, 0f);
			}
			// 아래로 이동 중이면서 현재 위치가 시작 위치보다 진폭보다 큰 경우
			else if (!movingUp && transform.position.x > startPosition.x - Amplitude)
			{
				transform.position -= new Vector3(verticalMovement, 0f, 0f);
			}
			// 진폭 범위를 벗어날 경우 이동 방향을 반대로 변경
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