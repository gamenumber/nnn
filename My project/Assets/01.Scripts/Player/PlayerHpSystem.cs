using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerHpSystem : MonoBehaviour
{
	public int Health;
	public int MaxHealth;

	void Start()
	{
		Health = GameInstance.instance.CurrentPlayerHP;
	}
	public void InitHealth()
	{
		Health = MaxHealth;
		GameInstance.instance.CurrentPlayerHP = Health;
	}

	IEnumerator HitFlick()
	{
		int flickCount = 0;

		while (flickCount < 5)
		{
			GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
			yield return new WaitForSeconds(0.1f);
			GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
			yield return new WaitForSeconds(0.1f);
			flickCount++;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			SoundManager.instance.PlaySFX("Hit");
			Health -= 1;
			StartCoroutine(HitFlick());
			Destroy(collision.gameObject);

			if (Health <= 0)
			{
				GameManager.Instance.GetPlayerCharacter().DeadProcess();
			}
		}

		if (collision.gameObject.CompareTag("EnemyBullet"))
		{
			SoundManager.instance.PlaySFX("Hit");
			Health -= 1;
			StartCoroutine(HitFlick());
			Destroy(collision.gameObject);

			if (Health <= 0)
			{
				GameManager.Instance.GetPlayerCharacter().DeadProcess();
			}
		}

		if (collision.gameObject.CompareTag("Item"))
		{
			if (Health > MaxHealth)
			{
				Health = MaxHealth;
			}
		}
		GameInstance.instance.CurrentPlayerHP = Health;
	}



}