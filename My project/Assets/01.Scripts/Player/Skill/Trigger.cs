using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Thunder" || other.gameObject.tag == "Wind")
		{
			Destroy(other.gameObject);
		}

	}

}
