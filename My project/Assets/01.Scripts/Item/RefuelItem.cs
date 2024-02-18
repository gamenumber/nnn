using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 연료를 가득 채우는 스킬을 실제로 구현한 클래스 -> BaseItem을 상속받는다.
public class RefuelItem : BaseItem
{
	// override하여 부모 스크립트의 가상함수를 실행함
	public override void OnGetItem(PlayerCharacter playercharacter)
	{
		PlayerFuelSystem system = GameManager.Instance.GetPlayerCharacter().GetComponent<PlayerFuelSystem>(); // PlayerFuelSystem을 가져옴
		if (system != null) // null 체크
		{
			system.Fuel = system.MaxFuel; // MaxFuel을 Fuel로 넣어줌 
			GameInstance.instance.CurrentPlayerFuel = system.Fuel; // GameInstance의 CurrentPlayerFuel를 system.Fuel과 같게 만듦
		}
	}
}