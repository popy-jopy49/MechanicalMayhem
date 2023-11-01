using SWAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerStats : Singleton<PlayerStats>
{

	private TDPlayerMovement playerMovement;
	private Camera minimapCamera;
	private Player player;

	[SerializeField] private float defaultMaxSprint;
	[SerializeField] private float defaultMaxHealth;
	[SerializeField] private float defaultMinimapSize;

	private void Awake()
	{
		if (GameManager.I.newGame)
		{
			PlayerPrefs.DeleteKey("Player_MaxHealth_Value");
			PlayerPrefs.DeleteKey("Player_MaxSprint_Value");
			PlayerPrefs.DeleteKey("Player_MinimapSize_Value");
		}

		SceneManager.sceneLoaded += (_, _) =>
		{
			NBSceneLoaded();

			player = GameObject.Find("Player").GetComponent<Player>();
			playerMovement = player.GetComponent<TDPlayerMovement>();
			minimapCamera = GameObject.Find("MinimapCamera").GetComponent<Camera>();

			SetMaxHealth(20, "Player", "MaxHealth");
			SetMaxSprint(1, "Player", "MaxSprint");
			SetMinimapSize(1, "Player", "MinimapSize");

			InitialisePlayerUpgrades();
		};
	}

	private void InitialisePlayerUpgrades()
	{
		player.SetMaxHealth(PlayerPrefs.GetFloat("Player_MaxHealth_Value"));
		playerMovement.SetMaxSprint(PlayerPrefs.GetFloat("Player_MaxSprint_Value"));
		minimapCamera.orthographicSize = PlayerPrefs.GetFloat("Player_MinimapSize_Value");
	}

	public void SetMaxSprint(float incrementAmount, string item, string upgradeName)
	{
		int level = PlayerPrefs.GetInt($"{item}_{upgradeName}", 0);
		PlayerPrefs.SetFloat($"{item}_{upgradeName}_Value", (incrementAmount * level) + defaultMaxSprint);
		InitialisePlayerUpgrades();
	}

	public void SetMaxHealth(float incrementAmount, string item, string upgradeName)
	{
		int level = PlayerPrefs.GetInt($"{item}_{upgradeName}", 0);
		PlayerPrefs.SetFloat($"{item}_{upgradeName}_Value", (incrementAmount * level) + defaultMaxHealth);
		InitialisePlayerUpgrades();
	}

	public void SetMinimapSize(float incrementAmount, string item, string upgradeName)
	{
		int level = PlayerPrefs.GetInt($"{item}_{upgradeName}", 0);
		PlayerPrefs.SetFloat($"{item}_{upgradeName}_Value", (incrementAmount * level) + defaultMinimapSize);
		InitialisePlayerUpgrades();
	}

	#region Nuts And Bolts
	private int nutsAndBolts;
	private TMP_Text nbText;

	private void NBSceneLoaded()
	{
		nbText = GameObject.Find("NutsAndBoltsAmount").GetComponent<TMP_Text>();
		ChangeNutsAndBolts(0);
	}

	private void Update()
	{
#if UNITY_EDITOR
		if (Keyboard.current.numpadPlusKey.wasPressedThisFrame)
			ChangeNutsAndBolts(1000000);
#endif
	}

	public void ChangeNutsAndBolts(int amount)
	{
		nutsAndBolts += amount;
		nbText.text = nutsAndBolts.ToString();
	}
	public int GetNutsAndBolts() => nutsAndBolts;
	#endregion

}
