using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
	public float windForce = 10f; // �ٶ��� �������� ���� ũ��

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player")) // �÷��̾� �ױ׸� ����Ͽ� �浹�� Ȯ��
		{

			Transform playerTransform = other.transform;
			// ������ �������� �÷��̾��� ��ġ�� ���� �̵���Ŵ
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
