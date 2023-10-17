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

	private void Awake()
	{
		SceneManager.sceneLoaded += (_, _) =>
		{
			player = GameObject.Find("Player").GetComponent<Player>();
			playerMovement = player.GetComponent<TDPlayerMovement>();
			minimapCamera = GameObject.Find("MinimapCamera").GetComponent<Camera>();
			nbText = GameObject.Find("NutsAndBoltsAmount").GetComponent<TMP_Text>();
			ChangeNutsAndBolts(0);
			player.SetMaxHealth(MaxHealth);
			playerMovement.SetMaxSprint(MaxSprint);
			minimapCamera.orthographicSize = MinimapSize;
		};
	}

	private void Start()
	{
		/*if (!GameManager.I.newGame)
			return;*/ // TODO: Change and uncomment

		MaxSprint = defaultMaxSprint;
		MaxHealth = defaultMaxHealth;
		minimapCamera.orthographicSize = defaultMinimapSize; // Doesn't start at 0
	}

	private void Update()
	{
#if UNITY_EDITOR
		if (Keyboard.current.numpadPlusKey.wasPressedThisFrame)
			ChangeNutsAndBolts(1000000);
#endif
	}

    private float _maxSprint;
    private float _maxHealth;
    private float _minimapSize;
    public float MaxSprint 
		{ get { return _maxSprint; } set { playerMovement.AddMaxSprint(value); _maxSprint = value; } }
	public float MaxHealth
		{ get { return _maxHealth; } set { player.AddMaxHealth(value); _maxHealth = value; } }
	public float MinimapSize
		{ get { return _minimapSize; } set { minimapCamera.orthographicSize += value; _minimapSize = value; } }

	[SerializeField] private float defaultMaxSprint;
	[SerializeField] private float defaultMaxHealth;
	[SerializeField] private float defaultMinimapSize;

	private int nutsAndBolts;
	private TMP_Text nbText;
	public void ChangeNutsAndBolts(int amount)
	{
		nutsAndBolts += amount;
		nbText.text = nutsAndBolts.ToString();
	}
	public int GetNutsAndBolts() => nutsAndBolts;

}
