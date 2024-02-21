using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision != null)
		{
			if (!collision.CompareTag("Player") && !collision.CompareTag("Item") && !collision.CompareTag("BossA") && !collision.CompareTag("BossB"))
			{
				Destroy(collision.gameObject);
			}
		}
	}
}
