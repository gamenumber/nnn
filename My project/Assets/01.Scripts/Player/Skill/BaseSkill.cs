using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseSkill : MonoBehaviour
{
	protected PlayerCharacter _playerCharacter;
	public float CooldownTime;
	public float CurrentTime;
	public bool bIsCoolDown = false;

	public void Init(PlayerCharacter playerCharacter)
	{
		_playerCharacter = playerCharacter;
	}

	public void Update()
	{
		if (bIsCoolDown)
		{
			CurrentTime -= Time.deltaTime;
			if (CurrentTime <= 0)
			{
				bIsCoolDown = false;
			}
		}

	}

	public bool IsAvailable()
	{
		return !bIsCoolDown;
	}


	public virtual void Activate()
	{
		bIsCoolDown = true;
		CurrentTime = CooldownTime;
	}

	public void InitCoolDown()
	{
		bIsCoolDown = false;
		CurrentTime = 0;
	}

}