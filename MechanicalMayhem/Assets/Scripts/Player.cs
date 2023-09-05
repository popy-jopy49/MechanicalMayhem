using SWAssets;
using SWAssets.Utils;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Singleton<Player>
{

    [SerializeField] private float maxHealth;

	private float health;
	private HealthBar healthBar;
    private Transform respawnPoint;
    private Rigidbody2D rb;

	private List<GameObject> itemsToPickup = new();
    private List<Repairable> nearbyRepairables = new();
    private List<GameObject> inventory = new();

    private TMP_Text nbText;
    private int nutsAndBolts;
    private GameObject fToInteract;

    private void Awake()
    {
        print("cook");
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        nbText = GameObject.Find("NutsAndBoltsAmount").GetComponent<TMP_Text>();
        respawnPoint = GameObject.Find("RespawnPoint").transform;
        rb = GetComponent<Rigidbody2D>();

        fToInteract = GameObject.Find("FToInteract");
        fToInteract.SetActive(false);

        InputManager.INPUT_ACTIONS.Main.Interact.started += Interact;
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        fToInteract.SetActive(itemsToPickup.Count > 0 || nearbyRepairables.Count > 0);

        Vector3 mousePos = InputManager.INPUT_ACTIONS.Main.MousePosition.ReadValue<Vector2>();
        mousePos.z = 0.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos -= transform.position;
        float rot = VectorUtils.GetAngleFromVector(mousePos);
        transform.Find("WeaponManager").eulerAngles = new Vector3(0, 0, rot);
        transform.Find("WeaponManager").localScale = new Vector3(1, rot + 90 < 0 || rot + 90 >= 180 ? -1 : 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (!itemsToPickup.Contains(collision.gameObject))
                itemsToPickup.Add(collision.gameObject);
        }

        Repairable repairable = collision.GetComponent<Repairable>();
        if (repairable)
        {
            if (!nearbyRepairables.Contains(repairable))
                nearbyRepairables.Add(repairable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (itemsToPickup.Contains(collision.gameObject))
                itemsToPickup.Remove(collision.gameObject);
        }

        Repairable repairable = collision.GetComponent<Repairable>();
        if (repairable)
        {
            if (nearbyRepairables.Contains(repairable))
                nearbyRepairables.Remove(repairable);
        }
    }

    private void Interact(InputAction.CallbackContext obj)
	{
		// Pick up
		if (itemsToPickup.Count > 0)
        {
			GameObject temp = itemsToPickup[0];
			inventory.Add(temp);
            temp.SetActive(false);
			itemsToPickup.Remove(temp);
            temp.transform.parent = transform;
            return;
        }

        // Repair
        if (nearbyRepairables.Count <= 0)
            return;

        bool foundSomething = false;
        foreach (Repairable repairable in nearbyRepairables)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (!repairable.AddItem(inventory[i]))
                    continue;

                foundSomething = true;
                nearbyRepairables.Remove(repairable);
                inventory.RemoveAt(i);
            }
            if (foundSomething)
                break;
        }
    }

    public void Damage(float damage)
	{
		health -= damage;
		healthBar.SetHealth(health);

        if (health <= 0)
        {
            Respawn();
        }
	}

    private void Respawn()
    {
        rb.position = respawnPoint.position;
        rb.velocity = Vector2.zero;
        health = maxHealth;
        healthBar.SetHealth(health);
    }

    public void ChangeNutsAndBolts(int amount)
    {
		nutsAndBolts += amount;
        nbText.text = nutsAndBolts.ToString();
	}
    public int GetNutsAndBolts() => nutsAndBolts;

}
