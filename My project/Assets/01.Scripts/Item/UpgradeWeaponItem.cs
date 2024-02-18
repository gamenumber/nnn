using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeWeaponItem : BaseItem
{
	// override�Ͽ� �θ� ��ũ��Ʈ�� �����Լ��� ������
	public override void OnGetItem(PlayerCharacter playercharacter)
	{
		if (playercharacter != null ) // ���� characterManager�� null�� �ƴϰ�, characterManager.Player���
		{
			PlayerCharacter playerCharacter = GameManager.Instance.GetPlayerCharacter().GetComponent<PlayerCharacter>(); // �÷��̾� ĳ���͸� ������

			int currentLevel = playerCharacter.CurrentWeaponLevel; // �÷��̾� ĳ������ CurrentWeaponLevel�� ������
			int maxLevel = playerCharacter.MaxWeaponLevel; // �÷��̾� ĳ������ MaxWeaponLevel�� ������

			if (currentLevel >= maxLevel) // ���� ������ maxLevel���� ���ٸ� 
			{
				//GameManager.Instance.AddScore(30);
				return; // �Լ��� ���
			}

			playerCharacter.CurrentWeaponLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel); // playerCharacter.CurrentWeaponLevel�� 0 �̸����� �������ų� maxLevel�� �ʰ����� �ʵ����ϰ�, currentLevel���� 1�� ���Ѵ�.
			GameInstance.instance.CurrentPlayerWeaponLevel = playerCharacter.CurrentWeaponLevel; // GameInstance.instance�� CurrentPlayerWeaponLevel�� �����ͼ� playerCharacter.CurrentWeaponLevel���� ����
		}
	}
}
