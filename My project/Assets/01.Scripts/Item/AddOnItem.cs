using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOnItem : BaseItem
{
	public GameObject Addon;
	public override void OnGetItem(PlayerCharacter playerCharacter)
	{
		base.OnGetItem(playerCharacter);
		PlayerCharacter player = playerCharacter;
		SpawnAddOn(player.AddOnPos[GameInstance.instance.CurrentPlayerAddOnCount], Addon);
		if (GameInstance.instance.CurrentPlayerAddOnCount < 2)
		{
			GameInstance.instance.CurrentPlayerAddOnCount++;
		}
	}

	public static void SpawnAddOn(Transform t , GameObject prefab)
	{
		GameObject Addon = Instantiate(prefab, t.position, Quaternion.identity);
		Addon.GetComponent<AddOn>().FollowTransform = t;
	}
}
