using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairItem : BaseItem
{
	public override void OnGetItem(PlayerCharacter playerCharacter)
	{
		PlayerHpSystem system = GameManager.Instance.GetPlayerCharacter().GetComponent<PlayerHpSystem>(); // PlayerHPSystem�� ������
		if (system != null)
		{
			system.Health += 1; // 1ȸ����
		}
	}
}
