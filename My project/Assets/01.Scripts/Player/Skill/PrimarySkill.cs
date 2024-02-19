using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Weapon
{
	// ĳ���� ������ ��ü, ���⸦ Ȱ��ȭ�� ��� ĳ���͸� �����ϴ� �� ����
	void Activate(PrimarySkill primarySkill, PlayerCharacter playerCharacter);
}

public class Level1Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, PlayerCharacter playerCharacter)
	{
		// ���� 1 ������ Ư�� ����
		Vector3 position = playerCharacter.transform.position;
		primarySkill.ShootProjectile(position, Vector3.up * 3);
		
	}
}

// ���� 2 ���Ⱑ ��� �����ϴ��� ������ �κ�
public class Level2Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, PlayerCharacter playerCharacter)
	{
		// ���� 2 ������ Ư�� ����
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

// ���� 4 ���Ⱑ ��� �����ϴ��� ������ �κ�
public class Level4Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, PlayerCharacter playerCharacter)
	{
		// ���� 4 ������ Ư�� ����
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

// ���� 5 ���Ⱑ ��� �����ϴ��� ������ �κ�
public class Level5Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, PlayerCharacter playerCharacterr)
	{
		// ���� 5 ������ Ư�� ����
		Vector3 position = playerCharacterr.transform.position;

		for (int i = 0; i < 180; i += 10) // 360���� 10���� ������ �Ѿ� �߻�
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
		// �÷��̾� ��ġ�� ����
		Vector3 position = playerCharacter.transform.position;

		for (float x = -18; x <= 18; x += 1) // x���� -18���� 18���� 1�� ������Ű�鼭 �ݺ��ϴ� �ݺ���
		{

			Vector3 directionPositiveY = new Vector3(0, 1, 0);
			Vector3 offsetPositiveY = new Vector3(x, 0, 0);
			primarySkill.ShootProjectile(position + offsetPositiveY, directionPositiveY);
			Vector3 directionNegativeY = new Vector3(0, -1, 0); // �� �������� 
			Vector3 offsetNegativeY = new Vector3(x, 0, 0); // �� ��ġ���� �߻�
			primarySkill.ShootProjectile(position + offsetNegativeY, directionNegativeY);
			Vector3 directionPositiveX = new Vector3(1, 0, 0); // �� �������� 
			Vector3 offsetPositiveX = new Vector3(0, x, 0); // �� ��ġ���� �߻�
			primarySkill.ShootProjectile(position + offsetPositiveX, directionPositiveX);
			Vector3 directionNegativeX = new Vector3(-1, 0, 0); // �� �������� 
			Vector3 offsetNegativeX = new Vector3(0, x, 0); // �� ��ġ���� �߻�
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
		// �⺻ ��ų ��Ÿ��
		CooldownTime = 0.2f;


	}

	public override void Activate()
	{
		base.Activate();
		weapons[GameManager.Instance.GetPlayerCharacter().CurrentWeaponLevel].Activate(this, GameManager.Instance.GetPlayerCharacter());

	}

	public void ShootProjectile(Vector3 position, Vector3 direction)
	{
		GameObject instance = Instantiate(Projectile, position, Quaternion.identity); // �������� �ν��Ͻ�ȭ ���Ѽ� ������ ���̰Բ� ��üȭ��Ŵ
		Projectile projectile = instance.GetComponent<Projectile>(); // Projectile�� ������

		if (projectile != null)
		{
			projectile.MoveSpeed = ProjectileMoveSpeed; // ������ MoveSpeed�� ProjectileMoveSpeed ���� ������Ŵ
			projectile.SetDirection(direction.normalized); // ������ ���ؼ� ǥ��ȭ ��Ŵ
			SoundManager.instance.PlaySFX("Shoot");
		}

	}
}
