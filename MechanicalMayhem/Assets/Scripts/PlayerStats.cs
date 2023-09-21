using SWAssets;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{

	private TDPlayerMovement playerMovement;
	private Camera minimapCamera;

	private void Awake()
	{
		playerMovement = GetComponent<TDPlayerMovement>();
		minimapCamera = GameObject.Find("MinimapCamera").GetComponent<Camera>();
	}

	private void Start()
	{
		if (!GameManager.I.newGame)
			return;

		MaxSprint = defaultMaxSprint;
		MaxHealth = defaultMaxHealth;
		MinimapSize = defaultMinimapSize;
	}

	public float MaxSprint 
		{ get { return MaxSprint; } set { playerMovement.AddMaxSprint(value); } }
	public float MaxHealth
		{ get { return MaxHealth; } set { Player.I.AddMaxHealth(value); } }
	public float MinimapSize
		{ get { return MinimapSize; } set { minimapCamera.orthographicSize += value; } }

	[SerializeField] private float defaultMaxSprint;
	[SerializeField] private float defaultMaxHealth;
	[SerializeField] private float defaultMinimapSize;

}
