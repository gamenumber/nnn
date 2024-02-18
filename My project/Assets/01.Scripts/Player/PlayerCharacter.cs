using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
	#region Movement
	private Vector2 _moveInput;
	public float MoveSpeed;
	#endregion

	#region Skills
	[HideInInspector]public Dictionary<EnumTypes.PlayerSkill, BaseSkill> Skills;
	[SerializeField] private GameObject[] _skillPrefabs;
	#endregion

	#region Invincibility
	private bool invincibility;
	private Coroutine invincibilityCoroutine;
	private const double InvincibilityDurationInSeconds = 3; // ¹??? ??¼? ½?°? (??)
	public bool Invincibility
	{
		get { return invincibility; }
		set { invincibility = value; }
	}
	#endregion

	#region Weapon
	public int CurrentWeaponLevel = 0;
	public int MaxWeaponLevel = 3;
	#endregion


	public float distance;
	public Transform[] AddOnPos;
	private AddOnItem addOnItem;
	private int addon;
	public GameObject Addon;

	public void InitAddOn()
	{
		// 게임 인스턴스 의 CurrentADdOnCount 만큼 AddOn 다시 생성
		for (int i = 0; i < GameInstance.instance.CurrentPlayerAddOnCount; i++)
		{
			AddOnItem.SpawnAddOn(AddOnPos[i], Addon);
		}

	}

	private void Start()
	{
		InitAddOn();
		InitializeSkills();
	}
	public void SetAddOnItem(AddOnItem item)
	{
		addOnItem = item;
	}

	public void DeadProcess()
	{
		Destroy(gameObject);
		SceneManager.LoadScene("MainMenu");
		GameInstance.instance = null;
	}

	private void Update()
	{
		//InitAddOn();
		UpdateMovement();
		UpdateSkillInput();
	}

	public void InitSkillCoolDown()
	{
		foreach (var skill in Skills.Values)
		{
			skill.InitCoolDown();
		}
	}

	private void UpdateMovement()
	{
		_moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		transform.Translate(new Vector3(_moveInput.x, _moveInput.y, 0f) * (MoveSpeed * Time.deltaTime));
		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		if (pos.x < 0f) pos.x = 0f;
		if (pos.x > 1f) pos.x = 1f;
		if (pos.y < 0f) pos.y = 0f;
		if (pos.y > 1f) pos.y = 1f;
		transform.position = Camera.main.ViewportToWorldPoint(pos);
	}

	private void UpdateSkillInput()
	{
		if (Input.GetKey(KeyCode.Z)) ActivateSkill(EnumTypes.PlayerSkill.Primary);
		if (Input.GetKeyUp(KeyCode.X)) ActivateSkill(EnumTypes.PlayerSkill.Repair);
		if (Input.GetKeyUp(KeyCode.C)) ActivateSkill(EnumTypes.PlayerSkill.Protact);
		if (Input.GetKeyUp(KeyCode.V)) ActivateSkill(EnumTypes.PlayerSkill.Iceage);
		if (Input.GetKeyUp(KeyCode.B)) ActivateSkill(EnumTypes.PlayerSkill.Bomb);
	}

	private void InitializeSkills()
	{
		Skills = new Dictionary<EnumTypes.PlayerSkill, BaseSkill>();

		for (int i = 0; i < _skillPrefabs.Length; i++)
		{
			AddSkill((EnumTypes.PlayerSkill)i, _skillPrefabs[i]);
		}

		CurrentWeaponLevel = GameInstance.instance.CurrentPlayerWeaponLevel;
	}

	private void AddSkill(EnumTypes.PlayerSkill skillType, GameObject prefab)
	{
		GameObject skillObject = Instantiate(prefab, transform.position, Quaternion.identity);
		skillObject.transform.parent = this.transform;

		if (skillObject != null)
		{

			BaseSkill skillComponent = skillObject.GetComponent<BaseSkill>();
			skillComponent.Init(this);

			Skills.Add(skillType, skillComponent);
		}
	}

	private void ActivateSkill(EnumTypes.PlayerSkill skillType)
	{
		
		if (Skills.ContainsKey(skillType))
		{
			if (Skills[skillType].IsAvailable())
			{
				Skills[skillType].Activate();
			}
		}
	}

	public void SetInvincibility(bool invin)
	{
		if (invin)
		{
			if (invincibilityCoroutine != null)
			{
				StopCoroutine(invincibilityCoroutine);
			}
			invincibilityCoroutine = StartCoroutine(InvincibilityCoroutine());
		}
	}

	private IEnumerator InvincibilityCoroutine()
	{
		Invincibility = true;
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		float invincibilityDuration = 3f;
		spriteRenderer.color = new Color(1, 1, 1, 0.5f);
		yield return new WaitForSeconds(invincibilityDuration);
		Invincibility = false;
		spriteRenderer.color = new Color(1, 1, 1, 1f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		BaseItem item = collision.GetComponent<BaseItem>();
		if (item != null)
		{
			item.OnGetItem(this);
			Destroy(collision.gameObject);
		}
	}
}