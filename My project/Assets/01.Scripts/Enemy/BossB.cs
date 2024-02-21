using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossB : MonoBehaviour
{
	public GameObject WindPrefab; // Reference to the wind prefab
	public GameObject LightningPrefab;
	public GameObject Danger;
	public GameObject DarkMeteor;


	public float initialWindSpeed = 5f;
	public float windChangeInterval = 2f;
	public float horizontalForce = 2f;

	public float MoveSpeed = 2.0f; // Boss move speed
	public float FireRate = 2.0f; // Fire rate between patterns

	private int _currentPatternIndex = 0; // Current pattern index
	private Vector3 _originPosition; // Original position

	private bool _isPatternInProgress = false; // Flag to track if a pattern is in progress

	private List<Rigidbody2D> windRbs = new List<Rigidbody2D>(); // List to store multiple wind Rigidbody2D components

	private void Start()
	{
		_originPosition = transform.position;
		StartCoroutine(MoveDownAndStartPattern());
	}

	private IEnumerator MoveDownAndStartPattern()
	{
		while (transform.position.y > _originPosition.y - 3f)
		{
			transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
			yield return null;
		}

		StartCoroutine(AlternatePatterns());
	}

	private IEnumerator AlternatePatterns()
	{
		while (true)
		{
			if (!_isPatternInProgress)
			{
				if (_currentPatternIndex == 0)
				{
					yield return StartCoroutine(Pattern1());
				}
				else if (_currentPatternIndex == 1)
				{
					yield return StartCoroutine(Pattern2());
				}

				else if (_currentPatternIndex == 2)
				{
					yield return StartCoroutine(Pattern3());
				}
			}

			yield return new WaitForSeconds(FireRate);

			// Toggle between pattern 1 and 2
			_currentPatternIndex = (_currentPatternIndex + 1) % 3;
		}
	}

	private IEnumerator Pattern1()
	{
		_isPatternInProgress = true;
		
		// Spawn multiple wind objects with random directions
		for (int i = 0; i < 40; i++)
		{
			SoundManager.instance.PlaySFX("Wind");
			GameObject wind = Instantiate(WindPrefab, transform.position, Quaternion.identity);
			Rigidbody2D windRb = wind.GetComponent<Rigidbody2D>();
			windRbs.Add(windRb);
		}

		// InvokeRepeating to periodically update wind directions
		InvokeRepeating("AdjustWindDirection", 0f, windChangeInterval);

		// Apply horizontal force (assuming you want to apply it during Pattern1)
		ApplyHorizontalForce();

		// Wait for a duration before completing the pattern
		yield return new WaitForSeconds(5.0f); // Adjust this duration as needed

		// Stop invoking AdjustWindDirection when needed (optional)
		CancelInvoke("AdjustWindDirection");

		// Set _isPatternInProgress to false to allow starting the next pattern
		_isPatternInProgress = false;
	}

	private void AdjustWindDirection()
	{
		foreach (Rigidbody2D windRb in windRbs.ToArray())
		{
			if (windRb != null)
			{
				Vector2 randomDirection = Random.insideUnitCircle.normalized;
				windRb.velocity = randomDirection * initialWindSpeed;
			}
			else
			{
				// Remove null references from the list
				windRbs.Remove(null);
			}
		}
	}

	private void ApplyHorizontalForce()
	{
		foreach (Rigidbody2D windRb in windRbs.ToArray())
		{
			if (windRb != null)
			{
				windRb.AddForce(Vector2.right * windRb.velocity.magnitude * horizontalForce, ForceMode2D.Impulse);
			}
			else
			{
				// Remove null references from the list
				windRbs.Remove(null);
			}
		}
	}

	private IEnumerator Pattern2()
	{
		_isPatternInProgress = true;

		// Stop invoking AdjustWindDirection when needed (optional)
		CancelInvoke("AdjustWindDirection");

		// Perform actions for Pattern2
		// For now, it's empty; you can add your actions here

		// Wait for a duration before completing the pattern
		yield return new WaitForSeconds(1f); // Adjust this duration as needed

		// Set _isPatternInProgress to false to allow starting the next pattern
		_isPatternInProgress = false;
	}


	private IEnumerator Pattern3()
	{
		_isPatternInProgress = true;
		

		for (int i = 0; i < 3; i++)
		{
			
			StartCoroutine(LightningAppear());
		}

		// Wait for a duration before completing the pattern
		yield return new WaitForSeconds(3.0f); // Adjust this duration as needed

		// Set _isPatternInProgress to false to allow starting the next pattern
		_isPatternInProgress = false;
	}

	private IEnumerator LightningAppear()
	{
		// Instantiate danger object at a random position within boss's range
		Vector3 randomPosition = new Vector3(
			Random.Range(transform.position.x - 7f, transform.position.x + 7f),
			Random.Range(transform.position.y - 8f, transform.position.y),
			0f
		);
		ImageFadeInOut.Instance.StartFade();

		GameObject dangerInstance = Instantiate(Danger, randomPosition, Quaternion.identity);
		yield return new WaitForSeconds(0.4f);

		Destroy(dangerInstance); // Destroy the Danger object after waiting

		yield return new WaitForSeconds(0.4f);
		SoundManager.instance.PlaySFX("Lightning");

		// Instantiate lightning prefab at the same random position
		Instantiate(LightningPrefab, randomPosition, Quaternion.identity);

		yield return null;
	}

	private IEnumerator Pattern4()
	{
		// 화면을 어둡게 만드는 효과 실행
		ImageFadeInOut.Instance.IsDark();
		yield return new WaitForSeconds(0.5f);

		// 1에서 5개의 DarkMeteor를 랜덤으로 생성
		int numberOfMeteors = Random.Range(1, 6);

		for (int i = 0; i < numberOfMeteors; i++)
		{
			// 화면 상단의 랜덤 위치에 DarkMeteor를 생성
			Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 10f, 0f);
			GameObject darkMeteor = Instantiate(DarkMeteor, spawnPosition, Quaternion.identity);

			// Rigidbody2D 컴포넌트를 얻어 속도를 랜덤으로 적용
			Rigidbody2D darkMeteorRb = darkMeteor.GetComponent<Rigidbody2D>();
			darkMeteorRb.gravityScale = 0; // 중력을 0으로 설정하여 수직 방향으로만 이동
			float randomSpeed = Random.Range(initialWindSpeed, initialWindSpeed * 2f); // 랜덤한 속도
			darkMeteorRb.velocity = Vector2.down * randomSpeed; // 아래로만 이동

			yield return new WaitForSeconds(0.1f); // 각 DarkMeteor 생성 간의 지연 시간
		}

		yield return null; // Coroutine 종료
	}

	private void OnDestroy()
	{
		GameManager.Instance.StageClear();
		GameInstance.instance.CurrentStageLevel += 1;
	}
}
