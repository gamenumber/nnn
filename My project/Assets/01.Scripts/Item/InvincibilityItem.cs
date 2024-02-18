using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvincibilityItem : BaseItem
{
	public override void OnGetItem(PlayerCharacter playerCharacter)
	{
		base.OnGetItem(playerCharacter);
		GameManager.Instance.GetPlayerCharacter().SetInvincibility(true);
	}
}
