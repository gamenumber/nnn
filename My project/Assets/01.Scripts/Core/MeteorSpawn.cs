using System.Collections;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
	public Transform[] MeteorSpawnTransform; 
	public GameObject meteorPrefab;

	private void Start()
	{

		InvokeRepeating("SpawnMeteor", 0f, 4f);
	}

	private void SpawnMeteor()
	{

		int randomIndex = Random.Range(0, MeteorSpawnTransform.Length);
		Transform spawnPosition = MeteorSpawnTransform[randomIndex];


		InstantiateMeteor(spawnPosition);
	}

	private void InstantiateMeteor(Transform spawnPosition)
	{
		Instantiate(meteorPrefab, spawnPosition.position, Quaternion.identity);
	}
}