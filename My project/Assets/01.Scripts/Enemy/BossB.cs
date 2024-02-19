using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossB : BossA
{
	public GameObject mini;
	public override void Pattern7()
	{
		Instantiate(mini, transform.position, Quaternion.identity);
		mini.transform.localScale = new Vector3(0, -4, 0);
		
	}

	public override void Pattern8()
	{

	}
}
