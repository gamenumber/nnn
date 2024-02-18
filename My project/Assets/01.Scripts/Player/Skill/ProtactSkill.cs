using UnityEngine;

public class ProtactSkill : BaseSkill
{
	public GameObject bulletPrefab; // Drag your bullet prefab into this field in the Unity Editor

	public float rotationSpeed = 30f;
	public float bulletSpawnRadius = 3f;
	public float skillDuration = 5f;

	private float timer;

	public override void Activate()
	{
		base.Activate();
		Debug.Log("보호막 가동 !!!!!!!!!!!!!! 보호막 가동 !!!!!!!!!!!!!! 보호막 가동 !!!!!!!!!!!!!! 보호막 가동 !!!!!!!!!!!!!! 보호막 가동 !!!!!!!!!!!!!!");
		timer = skillDuration;
		InvokeRepeating("SpawnAndRotateBullets", 0f, 0.1f);
	}

	private void SpawnAndRotateBullets()
	{
		timer -= 0.1f;

		if (timer <= 0f)
		{
			CancelInvoke("SpawnAndRotateBullets");
		}

		// Spawn the first bullet
		SpawnBulletWithRotation();

		// Spawn the second bullet with a slight offset in rotation
		SpawnBulletWithRotation(angleOffset: 15f); // Adjust the offset as needed
	}

	private void SpawnBulletWithRotation(float angleOffset = 0f)
	{
		float angle = Random.Range(0f, 360f) + angleOffset;
		Vector3 spawnPosition = new Vector3(
			Mathf.Cos(Mathf.Deg2Rad * angle) * bulletSpawnRadius,
			Mathf.Sin(Mathf.Deg2Rad * angle) * bulletSpawnRadius,
			0f
		) + transform.position;

		GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

		bullet.transform.SetParent(transform);
		bullet.transform.Rotate(Vector3.forward * angle);

		Destroy(bullet, 5f);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyBullet")
		{
			Destroy(other.gameObject);
		}
	}


}