using SWAssets;
using SWAssets.Utils;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private float maxHealth = 100;

	private float health;
	private HealthBar healthBar;
    private Transform respawnPoint;
    private Rigidbody2D rb;
    private Transform weaponManager;

    private List<GameObject> itemsToPickup = new();
    private List<Repairable> nearbyRepairables = new();
    private bool workbenchNearby = false;
    private Weapon nearbyWeapon = null;
    private List<GameObject> inventory = new();

    private GameObject fToInteract;
    private GameObject workbenchUI;

    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        respawnPoint = GameObject.Find("RespawnPoint").transform;
        rb = GetComponent<Rigidbody2D>();

        fToInteract = GameObject.Find("FToInteract");
		workbenchUI = gameObject.FindDeactivatedGameObject("Canvas", "WorkbenchUI");
        workbenchUI.SetActive(false);
        weaponManager = transform.Find("WeaponManager");

        InputManager.INPUT_ACTIONS.Main.Interact.started += Interact;
    }

    private void Update()
    {
        if (GameManager.I.InPuzzle())
            return;

        fToInteract.SetActive(itemsToPickup.Count > 0 || nearbyRepairables.Count > 0 || workbenchNearby || nearbyWeapon);

        Vector3 mousePos = InputManager.INPUT_ACTIONS.Main.MousePosition.ReadValue<Vector2>();
        mousePos.z = 0.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos -= transform.position;
        float rot = VectorUtils.GetAngleFromVector(mousePos);
        weaponManager.eulerAngles = new Vector3(0, 0, rot);
        weaponManager.localScale = new Vector3(1, rot + 90 < 0 || rot + 90 >= 180 ? -1 : 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.I.InPuzzle())
            return;

        if (collision.CompareTag("Item"))
        {
            if (!itemsToPickup.Contains(collision.gameObject))
                itemsToPickup.Add(collision.gameObject);
        }

        Repairable repairable = collision.GetComponent<Repairable>();
        if (repairable)
        {
            if (!repairable.IsRepaired() && !nearbyRepairables.Contains(repairable))
                nearbyRepairables.Add(repairable);
		}

		if (collision.CompareTag("Workbench"))
		{
			workbenchNearby = true;
		}

		if (collision.CompareTag("Weapon"))
		{
            print("weapon");
			nearbyWeapon = collision.GetComponent<Weapon>();
		}
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.I.InPuzzle())
            return;

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

		if (collision.CompareTag("Workbench"))
		{
			workbenchNearby = false;
            workbenchUI.SetActive(false);
		}

		if (collision.CompareTag("Weapon"))
		{
			nearbyWeapon = null;
		}
	}

    private void Interact(InputAction.CallbackContext obj)
	{
        // Repair
		if (nearbyRepairables.Count > 0)
		{
            InteractWithRepairables();
			return;
		}

		if (GameManager.I.InPuzzle())
            return;

        // Weapons
        if (nearbyWeapon)
        {
            nearbyWeapon.PickUp();
		}

		// Workbench
		if (workbenchNearby)
		{
			workbenchUI.SetActive(!workbenchUI.activeSelf);
		}

		// Pick up
		if (itemsToPickup.Count > 0)
        {
            InteractWithItems();
            return;
        }
	}

    private void InteractWithRepairables()
    {
		foreach (Repairable repairable in nearbyRepairables)
		{
			if (repairable.ThisPuzzle())
			{
				// Stop puzzle
				repairable.ExitPuzzle(false);
				return;
			}

			if (repairable.ReadyToBeRepaired())
			{
				repairable.AddItem(null);
				return;
			}

			bool foundSomething = false;
			for (int i = 0; i < inventory.Count; i++)
			{
				if (!repairable.AddItem(inventory[i]))
					continue;

				foundSomething = true;
				inventory.RemoveAt(i);
			}
			if (foundSomething)
				break;
		}
	}

    private void InteractWithItems()
	{
		GameObject temp = itemsToPickup[0];
		inventory.Add(temp);
		temp.SetActive(false);
		itemsToPickup.Remove(temp);
		temp.transform.parent = transform;
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
        foreach(Repairable repairable in nearbyRepairables)
        {
            if (repairable.ThisPuzzle())
            {
                repairable.ExitPuzzle(false);
            }
        }

        rb.position = respawnPoint.position;
        rb.velocity = Vector2.zero;
        health = maxHealth;
        healthBar.SetHealth(health);
    }

    public void RemoveRepairable(Repairable repairable) => nearbyRepairables.Remove(repairable);

	public void AddMaxHealth(float value) => SetMaxHealth(maxHealth + value);
	public void SetMaxHealth(float value)
	{
        maxHealth = value;
		health = value;
		healthBar.SetMaxHealth(value);
	}

}
