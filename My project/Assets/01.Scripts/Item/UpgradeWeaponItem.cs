using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeWeaponItem : BaseItem
{
	// override하여 부모 스크립트의 가상함수를 실행함
	public override void OnGetItem(PlayerCharacter playercharacter)
	{
		if (playercharacter != null ) // 만약 characterManager가 null이 아니고, characterManager.Player라면
		{
			PlayerCharacter playerCharacter = GameManager.Instance.GetPlayerCharacter().GetComponent<PlayerCharacter>(); // 플레이어 캐릭터를 가져옴

			int currentLevel = playerCharacter.CurrentWeaponLevel; // 플레이어 캐릭터의 CurrentWeaponLevel을 가져옴
			int maxLevel = playerCharacter.MaxWeaponLevel; // 플레이어 캐릭터의 MaxWeaponLevel을 가져옴

			if (currentLevel >= maxLevel) // 현재 레벨이 maxLevel보다 높다면 
			{
				//GameManager.Instance.AddScore(30);
				return; // 함수를 벗어남
			}

			playerCharacter.CurrentWeaponLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel); // playerCharacter.CurrentWeaponLevel을 0 미만으로 내려가거나 maxLevel을 초과하지 않도록하고, currentLevel에는 1을 더한다.
			GameInstance.instance.CurrentPlayerWeaponLevel = playerCharacter.CurrentWeaponLevel; // GameInstance.instance로 CurrentPlayerWeaponLevel을 가져와서 playerCharacter.CurrentWeaponLevel값을 넣음
		}
	}
}
