using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkill : BaseSkill
{
	public override void Activate()
	{

		Debug.Log("���� ������ ! ���� ������ ! ���� ������ !���� ������ !���� ������ !���� ������ !");
		base.Activate();
		
		GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");

		if (enemy != null)
		{
			Enemy e = GetComponent<Enemy>();	
			if (e != null)
			{
				e.Dead();
			}

		}

		
	}
}
