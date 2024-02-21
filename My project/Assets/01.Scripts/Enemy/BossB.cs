using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossB : MonoBehaviour
{
	public GameObject WindPrefab; // Reference to the wind prefab
	public GameObject LightningPrefab;
	public GameObject Danger;
	public Image imageToFade;  // 이곳에 페이드를 적용할 UI Image를 지정해주세요.


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
		GameObject canvas = GameObject.Find("Lightning");
		imageToFade = canvas.transform.Find("Image").GetComponent<Image>();
		StartCoroutine(FadeInOut());
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

		GameObject dangerInstance = Instantiate(Danger, randomPosition, Quaternion.identity);
		yield return new WaitForSeconds(1f);

		Destroy(dangerInstance); // Destroy the Danger object after waiting

		yield return new WaitForSeconds(0.4f);
		SoundManager.instance.PlaySFX("Lightning");

		// Instantiate lightning prefab at the same random position
		Instantiate(LightningPrefab, randomPosition, Quaternion.identity);

		yield return null;
	}

	private IEnumerator FadeInOut()
	{
		// Fade In
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1.5f) // 이곳에서 시간을 조정하실 수 있습니다.
		{
			imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, Mathf.Lerp(0, 1, t));
			yield return null;
		}

		// 일정 시간을 기다린 후
		yield return new WaitForSeconds(1.0f); // 이곳에서 기다리는 시간을 조정하실 수 있습니다.

		// Fade Out
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1.5f) // 이곳에서 시간을 조정하실 수 있습니다.
		{
			imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, Mathf.Lerp(1, 0, t));
			yield return null;
		}
	}





	private void OnDestroy()
	{
		GameManager.Instance.StageClear();
		GameInstance.instance.CurrentStageLevel += 1;
	}
}
