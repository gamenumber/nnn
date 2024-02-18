using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���Ḧ ���� ä��� ��ų�� ������ ������ Ŭ���� -> BaseItem�� ��ӹ޴´�.
public class RefuelItem : BaseItem
{
	// override�Ͽ� �θ� ��ũ��Ʈ�� �����Լ��� ������
	public override void OnGetItem(PlayerCharacter playercharacter)
	{
		PlayerFuelSystem system = GameManager.Instance.GetPlayerCharacter().GetComponent<PlayerFuelSystem>(); // PlayerFuelSystem�� ������
		if (system != null) // null üũ
		{
			system.Fuel = system.MaxFuel; // MaxFuel�� Fuel�� �־��� 
			GameInstance.instance.CurrentPlayerFuel = system.Fuel; // GameInstance�� CurrentPlayerFuel�� system.Fuel�� ���� ����
		}
	}
}