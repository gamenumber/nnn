using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class ImageFadeInOut : MonoBehaviour
{
	public static ImageFadeInOut Instance;

	public Image imageToFade;
	public Image Dark;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		
	}


	public void IsDark()
	{
		Dark.gameObject.SetActive(true);
	}

	public void StartFade()
	{
		StartCoroutine(FadeImageInOut());
	}

	public IEnumerator FadeImageInOut()
	{
		imageToFade.gameObject.SetActive(true);

		// Fade In
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 0.5f)  // Faster fade in
		{
			imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, Mathf.Lerp(0, 1, t));
			yield return null;
		}

		// Wait
		yield return new WaitForSeconds(0.2f);

		// Fade Out
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 0.5f)  // Faster fade out
		{
			imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, Mathf.Lerp(1, 0, t));
			yield return null;
		}

		imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, 0);  // Reset alpha to 0
		//imageToFade.gameObject.SetActive(false);
	}
}
