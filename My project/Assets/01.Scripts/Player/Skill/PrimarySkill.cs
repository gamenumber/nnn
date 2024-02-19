using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Weapon
{
	// 캐릭터 관리자 객체, 무기를 활성화할 대상 캐릭터를 관리하는 데 사용됨
	void Activate(PrimarySkill primarySkill, PlayerCharacter playerCharacter);
}

public class Level1Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, PlayerCharacter playerCharacter)
	{
		// 레벨 1 무기의 특정 동작
		Vector3 position = playerCharacter.transform.position;
		primarySkill.ShootProjectile(position, Vector3.up * 3);
		
	}
}

// 레벨 2 무기가 어떻게 동작하는지 구현한 부분
public class Level2Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, PlayerCharacter playerCharacter)
	{
		// 레벨 2 무기의 특정 동작
		Vector3 position = playerCharacter.transform.position;
		position.x -= 0.1f;

		for (int i = 0; i < 2; i++)
		{
			primarySkill.ShootProjectile(position, Vector3.up * 3);
			position.x += 0.2f;
		}
		
	}
}


public class Level3Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, PlayerCharacter playerCharacter)
	{

		Vector3 position = playerCharacter.transform.position;

		primarySkill.ShootProjectile(position, Vector3.up * 3);
		primarySkill.ShootProjectile(position, new Vector3(0.3f, 1, 0) * 3);
		primarySkill.ShootProjectile(position, new Vector3(-0.3f, 1, 0) * 3);

		
	}
}

// 레벨 4 무기가 어떻게 동작하는지 구현한 부분
public class Level4Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, PlayerCharacter playerCharacter)
	{
		// 레벨 4 무기의 특정 동작
		Vector3 position = playerCharacter.transform.position;
		position.x -= 0.1f;

		for (int i = 0; i < 2; i++)
		{
			primarySkill.ShootProjectile(position, Vector3.up * 3);
			position.x += 0.2f;
		}

		Vector3 position2 = playerCharacter.transform.position;
		primarySkill.ShootProjectile(position2, new Vector3(0.3f, 1, 0) * 3);
		primarySkill.ShootProjectile(position2, new Vector3(-0.3f, 1, 0) * 3);

		
	}
}

// 레벨 5 무기가 어떻게 동작하는지 구현한 부분
public class Level5Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, PlayerCharacter playerCharacterr)
	{
		// 레벨 5 무기의 특정 동작
		Vector3 position = playerCharacterr.transform.position;

		for (int i = 0; i < 180; i += 10) // 360도를 10도씩 나눠서 총알 발사
		{
			float angle = i * Mathf.Deg2Rad;
			Debug.Log(angle);
			Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * 9;

			primarySkill.ShootProjectile(position, direction);
		}

		
	}
}

public class Level6Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, PlayerCharacter playerCharacter)
	{
		// 플레이어 위치를 저장
		Vector3 position = playerCharacter.transform.position;

		for (float x = -18; x <= 18; x += 1) // x값을 -18부터 18까지 1씩 증가시키면서 반복하는 반복문
		{

			Vector3 directionPositiveY = new Vector3(0, 1, 0);
			Vector3 offsetPositiveY = new Vector3(x, 0, 0);
			primarySkill.ShootProjectile(position + offsetPositiveY, directionPositiveY);
			Vector3 directionNegativeY = new Vector3(0, -1, 0); // 이 방향으로 
			Vector3 offsetNegativeY = new Vector3(x, 0, 0); // 이 위치에서 발사
			primarySkill.ShootProjectile(position + offsetNegativeY, directionNegativeY);
			Vector3 directionPositiveX = new Vector3(1, 0, 0); // 이 방향으로 
			Vector3 offsetPositiveX = new Vector3(0, x, 0); // 이 위치에서 발사
			primarySkill.ShootProjectile(position + offsetPositiveX, directionPositiveX);
			Vector3 directionNegativeX = new Vector3(-1, 0, 0); // 이 방향으로 
			Vector3 offsetNegativeX = new Vector3(0, x, 0); // 이 위치에서 발사
			primarySkill.ShootProjectile(position + offsetNegativeX, directionNegativeX);
		}

	
	}
}


public class PrimarySkill : BaseSkill
{
	public float ProjectileMoveSpeed;
	public GameObject Projectile;
	private Weapon[] weapons;

	private void Awake()
	{
		weapons = new Weapon[6];

		weapons[0] = new Level1Weapon();
		weapons[1] = new Level2Weapon();
		weapons[2] = new Level3Weapon();
		weapons[3] = new Level4Weapon();
		weapons[4] = new Level5Weapon();
		weapons[5] = new Level6Weapon();
	}
	void Start()
	{
		// 기본 스킬 쿨타임
		CooldownTime = 0.2f;


	}

	public override void Activate()
	{
		base.Activate();
		weapons[GameManager.Instance.GetPlayerCharacter().CurrentWeaponLevel].Activate(this, GameManager.Instance.GetPlayerCharacter());

	}

	public void ShootProjectile(Vector3 position, Vector3 direction)
	{
		GameObject instance = Instantiate(Projectile, position, Quaternion.identity); // 프리팹을 인스턴스화 시켜서 실제로 보이게끔 실체화시킴
		Projectile projectile = instance.GetComponent<Projectile>(); // Projectile에 접근함

		if (projectile != null)
		{
			projectile.MoveSpeed = ProjectileMoveSpeed; // 현재의 MoveSpeed에 ProjectileMoveSpeed 값을 참조시킴
			projectile.SetDirection(direction.normalized); // 방향을 정해서 표준화 시킴
			SoundManager.instance.PlaySFX("Shoot");
		}

	}
}
