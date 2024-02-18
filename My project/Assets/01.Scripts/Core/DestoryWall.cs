using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryWall : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision != null)
		{
			Destroy(collision.gameObject);
		}
	}
}
