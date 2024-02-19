using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float Health = 3f;
	public float AttackDamage = 1f;
	bool bIsDead = false;
	public bool bMustSpawnItem = false;

	public GameObject ExplodeFX;

	public void Dead()
	{
		if (!bIsDead)
		{
			SoundManager.instance.PlaySFX("Explosion");

			if (!bMustSpawnItem)
			{
				GameManager.Instance.ItemManager.SpawnRandomItem(0, 3, transform.position);
			} 
			else
			{
				GameManager.Instance.ItemManager.SpawnRandom2Item(transform.position);
			}

			Destroy(gameObject);
			Instantiate(ExplodeFX, transform.position, Quaternion.identity);

		}

	}

	

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerBullet"))
		{

			Health -= 1f;
			StartCoroutine(HitFlick());
			if (Health <= 0)
			{
				Dead();
			}

			
		}

		
		if (collision.CompareTag("Protact") && !gameObject.CompareTag("BossA") && !gameObject.CompareTag("BossB"))
		{
			Destroy(gameObject);
		}

		if (collision.gameObject.CompareTag("Player"))
		{

			Health -= 1f;
			StartCoroutine(HitFlick());
			if (Health <= 0)
			{
				Dead();
			}
		}


	}

	IEnumerator HitFlick()
	{
		int FlickCount = 0;
		while (FlickCount < 5)
		{
			gameObject.GetComponent<SpriteRenderer>().color = Color.red;
			yield return new WaitForSeconds(0.1f);
			gameObject.GetComponent<SpriteRenderer>().color = Color.white;
			yield return new WaitForSeconds(0.1f);
			FlickCount++;
		}
		
	}

	private void OnDestroy()
	{
		
		Instantiate(ExplodeFX, transform.position, Quaternion.identity);
		GameManager.Instance.AddScore(10);
		
	}
}
