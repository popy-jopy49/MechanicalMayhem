using SWAssets;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : Singleton<SceneFader>
{

	[SerializeField] private Image panel;
	[SerializeField] private int fadeTime;

	private void Awake()
	{
		panel = GetComponent<Image>();
	}

	public async void FadeToScene(int buildIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);
		operation.allowSceneActivation = false;
		await Fade(1f);
		operation.allowSceneActivation = true;
		operation.completed += SceneFaderCompleted;
	}

	private async void SceneFaderCompleted(AsyncOperation obj)
	{
		await Fade(0f);
	}

	private async Task Fade(float targetAlpha)
	{
		float time = 0;
		while (panel.color.a != targetAlpha)
		{
			Color colour = panel.color;
			colour.a = Mathf.Lerp(panel.color.a, targetAlpha, time);
			panel.color = colour;
			time += 1f / fadeTime;
			await Task.Delay(fadeTime / 50);
		}
	}

}
