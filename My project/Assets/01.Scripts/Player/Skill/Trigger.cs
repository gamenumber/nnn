using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyBullet")
		{
			Destroy(other.gameObject);
		}

	}

}
