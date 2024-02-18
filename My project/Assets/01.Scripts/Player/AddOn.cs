using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOn : MonoBehaviour
{
	public GameObject bulletPrefab; // 총알 프리팹
	public float bulletSpeed = 40f; // 총알 속도
	public float ShootCycleTime = 3f;
	public float speed = 5f;
	public float range = 10f;

	public float LerpThirdValue = 0.05f;

	public Transform[] AddonPoint;
	public Transform FollowTransform;

	private Transform playerTransform;
	private Vector2 _moveDirection;

	// AddOn 인스턴스 개수를 저장하는 정적 변수
	public static int instanceCount = 0;

	void Start()
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		StartCoroutine(MoveTowardsPlayer());
		InvokeRepeating("Fire", 0, ShootCycleTime);
	}

	IEnumerator MoveTowardsPlayer()
	{
		while (true)
		{
			_moveDirection = (playerTransform.position - transform.position).normalized;
			yield return new WaitForSeconds(0.5f);
		}
	}

	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, FollowTransform.position, LerpThirdValue * Time.deltaTime * 4);
	}

	void Fire()
	{
		// 가장 가까운 적 찾기
		GameObject closestEnemy = FindClosestEnemy();
		Vector3 targetDirection;

		if (closestEnemy != null)
		{
			// 가장 가까운 적이 있다면 그 적을 향하는 방향 계산
			targetDirection = (closestEnemy.transform.position - transform.position).normalized * bulletSpeed * Time.deltaTime;
		}
		else
		{
			// 가장 가까운 적이 없다면 위로 발사
			targetDirection = Vector3.up * bulletSpeed * Time.deltaTime;
		}

		// 총알 생성 및 발사
		GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
		bullet.GetComponent<Rigidbody2D>().velocity = targetDirection * bulletSpeed;
	}

	GameObject FindClosestEnemy()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] bossAs = GameObject.FindGameObjectsWithTag("BossA");
		GameObject[] bossBs = GameObject.FindGameObjectsWithTag("BossB");

		List<GameObject> allTargets = new List<GameObject>();

		// Add enemies to the list
		if (enemies != null)
			allTargets.AddRange(enemies);

		// Add bossAs to the list
		if (bossAs != null)
			allTargets.AddRange(bossAs);

		// Add bossBs to the list
		if (bossBs != null)
			allTargets.AddRange(bossBs);

		if (allTargets.Count == 0)
		{
			Debug.LogWarning("No enemies or bosses found.");
			return null;
		}

		GameObject closestEnemy = allTargets[0];
		float minDistance = 1000f;
		Vector3 currentPos = transform.position;

		foreach (GameObject target in allTargets)
		{
			if (target == null)
			{
				Debug.LogWarning("Found a null target. Skipping.");
				continue;
			}

			float distance = Vector3.Distance(target.transform.position, currentPos);
			if (distance < minDistance)
			{
				closestEnemy = target;
				minDistance = distance;
			}
		}

		Debug.Log("Closest enemy: " + closestEnemy.name);
		return closestEnemy;
	}



	void OnDestroy()
	{
		// AddOn 인스턴스 개수 감소
		instanceCount--;
	}
}