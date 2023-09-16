using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class MainMenuManager : MonoBehaviour {

	private void Awake()
	{
        LoadVolume();

        resolutions = Screen.resolutions;
        resoloutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        originalResoloutionIndex = 0;

		for (int i = 0; i < resolutions.Length; i++)
		{
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
			{
                originalResoloutionIndex = i;
			}
		}

        resoloutionDropdown.AddOptions(options);
        resoloutionDropdown.value = originalResoloutionIndex;
        _resoloution = originalResoloutionIndex;
        resoloutionDropdown.RefreshShownValue();

        LoadGraphics();
    }

	#region Button Methods
    public static void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

	public static void QuitGame()
	{
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
	}

	#endregion

	#region Graphics Menu
	[Header("Graphics Dropdowns")]
    [SerializeField] private TMP_Dropdown antiAliasingDropdown;
    [SerializeField] private TMP_Dropdown frameRateDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private TMP_Dropdown resoloutionDropdown;

	[Header("Other Graphics")]
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TMP_Text brightnessTextValue;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private float defaultBrightness;

    private Resolution[] resolutions;
    private int originalResoloutionIndex = 0;

    private int _qualityLevel;
    private bool _isFullscreen;
    private float _brightnessLevel;
    private int _resoloution;
    private int _aliasing;
    private int _frameRate;
    private bool _vsync;
    private bool _hdr;
    private bool _bloom;
    private bool _chromaticAberration;

    public void SetBrightness(float brightness)
	{
        _brightnessLevel = brightness;
        UpdateGraphicsUI();
	}
	public void SetResoloution(int resoloutionIndex)
	{
		_resoloution = resoloutionIndex;
	}
	public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }
    public void SetAntiAliasing(int aliasingIndex)
    {
        _aliasing = aliasingIndex;
    }
    public void SetFrameRate(int frameRate)
    {
        _frameRate = frameRate;
    }
    public void SetFullscreen(bool isFullscreen)
    {
        _isFullscreen = isFullscreen;
    }
	public void SetVSync(bool vsync)
	{
		_vsync = vsync;
	}
	public void SetHDR(bool hdr)
	{
		_hdr = hdr;
	}
	public void SetBloom(bool bloom)
	{
		_bloom = bloom;
	}
	public void SetChromaticAberration(bool chromaticAberration)
	{
		_chromaticAberration = chromaticAberration;
	}

	public void ApplyGraphics()
	{
		PlayerPrefs.SetInt("Resoloution", _resoloution);
		Screen.SetResolution(resolutions[_resoloution].width, resolutions[_resoloution].height, Screen.fullScreen);

		PlayerPrefs.SetInt("Quality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

		PlayerPrefs.SetInt("AntiAliasing", _aliasing);
		QualitySettings.antiAliasing = _aliasing;

        PlayerPrefs.SetInt("FrameRate", _frameRate);
        if (_frameRate == 0)
            Application.targetFrameRate = 500;
        else if (_frameRate == 1)
            Application.targetFrameRate = 30;
        else if (_frameRate == 2)
            Application.targetFrameRate = 60;

		PlayerPrefs.SetInt("Fullscreen", _isFullscreen ? 1 : 0);
		Screen.fullScreen = _isFullscreen;


        PlayerPrefs.SetInt("VSync", _vsync ? 1 : 0);
        QualitySettings.vSyncCount = _vsync ? 1 : 0;

		PlayerPrefs.SetInt("HDR", _hdr ? 1 : 0);
		((UniversalRenderPipelineAsset)QualitySettings.renderPipeline).supportsHDR = _hdr;

		PlayerPrefs.SetFloat("Brightness", _brightnessLevel);

		PlayerPrefs.SetInt("Bloom", _bloom ? 1 : 0);

		PlayerPrefs.SetInt("ChromaticAberration", _chromaticAberration ? 1 : 0);

        GameManager.I.SavePPData(_bloom, _chromaticAberration, _brightnessLevel);

		UpdateGraphicsUI();
    }

    void LoadGraphics()
	{
        SetBrightness(PlayerPrefs.GetFloat("Brightness", defaultBrightness));
        SetFullscreen(PlayerPrefs.GetInt("Fullscreen", 1) == 1);
        SetQuality(PlayerPrefs.GetInt("Quality", 2));
        SetResoloution(PlayerPrefs.GetInt("Resoloution", 0));
        SetAntiAliasing(PlayerPrefs.GetInt("AntiAliasing", 1));
        SetFrameRate(PlayerPrefs.GetInt("FrameRate", 0));
        SetVSync(PlayerPrefs.GetInt("VSync", 1) == 1);
        SetHDR(PlayerPrefs.GetInt("HDR", 0) == 1);
        SetBloom(PlayerPrefs.GetInt("Bloom", 1) == 1);
		SetChromaticAberration(PlayerPrefs.GetInt("ChromaticAberration", 1) == 1);
        ApplyGraphics();
        UpdateGraphicsUI();
    }

	public void ResetBrightness()
	{
		_brightnessLevel = defaultBrightness;
		ApplyGraphics();
	}
	public void ResetResoloution()
	{
		_resoloution = originalResoloutionIndex;
		ApplyGraphics();
	}
	public void ResetQuality()
    {
        _qualityLevel = 2;
        ApplyGraphics();
	}
	public void ResetAntiAliasing()
	{
		_aliasing = 1;
		ApplyGraphics();
	}
    public void ResetFrameRate()
    {
        _frameRate = 0;
        ApplyGraphics();
	}
	public void ResetAllGraphics()
	{
		ResetBrightness();
		ResetResoloution();
		ResetQuality();
        ResetAntiAliasing();
        ResetFrameRate();
        UpdateGraphicsUI();
	}

    void UpdateGraphicsUI()
    {
        fullscreenToggle.isOn = _isFullscreen;
        brightnessSlider.value = _brightnessLevel;
        brightnessTextValue.text = _brightnessLevel.ToString("0.0");
        frameRateDropdown.SetValueWithoutNotify(_frameRate);
        antiAliasingDropdown.SetValueWithoutNotify(_aliasing);
        resoloutionDropdown.SetValueWithoutNotify(_resoloution);
        qualityDropdown.SetValueWithoutNotify(_qualityLevel);
    }
	#endregion

	#region Audio Menu
	[Header("Audio")]
    [SerializeField] private TMP_Text volumeTextValue;
    [SerializeField] private Slider volumeSlider;

    public void SetVolume(float volume)
    {
        GameAssets.I.MainMixer.SetFloat("Volume", volume);
        volumeTextValue.text = Mathf.FloorToInt(volume + 80).ToString();
    }
    public void VolumeApply()
    {
        GameAssets.I.MainMixer.GetFloat("Volume", out float v);
        PlayerPrefs.SetFloat("MasterVolume", v);
    }
    void LoadVolume()
    {
        float volume = PlayerPrefs.GetFloat("MasterVolume", 0);
        volumeSlider.value = volume;
        SetVolume(volume);
    }
    public void ResetVolume()
    {
        SetVolume(0);
        VolumeApply();
        volumeSlider.value = 0;
    }
	#endregion

}
