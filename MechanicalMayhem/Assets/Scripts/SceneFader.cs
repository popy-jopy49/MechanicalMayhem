using SWAssets;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SceneFader : Singleton<SceneFader>
{

	private Image panel;
	[SerializeField] private float fadeTime;
	private bool fading = false;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(transform.parent);

		panel = GetComponent<Image>();
		panel.enabled = false;
	}
	private void Update()
	{
		panel.enabled = fading;
	}

	public async void FadeToScene(int buildIndex)
	{
		if (fading || buildIndex < 0)
			return;

		fading = true; panel.enabled = true;
		AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);
		operation.allowSceneActivation = false;
		await Fade(1f);
		operation.allowSceneActivation = true;
		operation.completed += async _ =>
		{
			await Fade(0f);
			fading = false;
		};
	}

	private async Task Fade(float targetAlpha)
	{
		float originalAlpha = panel.color.a;
		Color colour = panel.color;
		for (float time = 0; time < fadeTime; time += Time.deltaTime)
		{
			colour.a = Mathf.Lerp(originalAlpha, targetAlpha, time / fadeTime);
			panel.color = colour;
			await Task.Yield();
		}
		colour.a = targetAlpha;
		panel.color = colour;
	}

}
