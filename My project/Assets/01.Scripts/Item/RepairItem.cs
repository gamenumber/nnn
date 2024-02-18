using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairItem : BaseItem
{
	public override void OnGetItem(PlayerCharacter playerCharacter)
	{
		PlayerHpSystem system = GameManager.Instance.GetPlayerCharacter().GetComponent<PlayerHpSystem>(); // PlayerHPSystem을 가져옴
		if (system != null)
		{
			system.Health += 1; // 1회복함
		}
	}
}
