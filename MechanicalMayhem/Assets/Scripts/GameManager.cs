using SWAssets;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    private GameObject escapeMenu;
    private bool puzzle = false;

	public static int MAINSCENE = 1;

	private bool bloom;
	private bool chromaticAberration;
	private float brightness;

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.buildIndex == MAINSCENE)
		{
			ApplyPPData();
		}
	}

	public void SavePPData(bool bloom, bool chromaticAberration, float brightness)
	{
		this.bloom = bloom;
		this.chromaticAberration = chromaticAberration;
		this.brightness = brightness;
	}

	private void ApplyPPData()
	{
		GameAssets.I.VolumeProfile.TryGet(out Bloom bloom);
		GameAssets.I.VolumeProfile.TryGet(out ChromaticAberration chromaticAberration);
		GameAssets.I.VolumeProfile.TryGet(out ColorAdjustments brightness);

		bloom.active = this.bloom;
		chromaticAberration.active = this.chromaticAberration;
		brightness.postExposure = new FloatParameter(this.brightness);
	}

	private void Start()
    {
        escapeMenu = GameObjectExtensionMethods.FindDeactivatedGameObject(gameObject, "Canvas", "EscapeMenu");

        InputManager.Initialise();
        InputManager.INPUT_ACTIONS.Main.PauseScreen.started += PauseScreenStarted;
    }

    private void PauseScreenStarted(InputAction.CallbackContext obj)
    {
        Time.timeScale = Convert.ToSingle(escapeMenu.activeSelf);
        escapeMenu.SetActive(!escapeMenu.activeSelf);
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public bool InPuzzle() => puzzle;
    public void SetInPuzzle(bool inPuzzle) => puzzle = inPuzzle;

}
