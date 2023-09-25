using SWAssets;
using SWAssets.Utils;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Singleton<Player>
{

    private float maxHealth = 100;

	private float health;
	private HealthBar healthBar;
    private Transform respawnPoint;
    private Rigidbody2D rb;

	private List<GameObject> itemsToPickup = new();
    private List<Repairable> nearbyRepairables = new();
    private bool workbenchNearby = false;
    private List<GameObject> inventory = new();

    private TMP_Text nbText;
    private int nutsAndBolts;
    private GameObject fToInteract;
    private GameObject workbenchUI;

    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        nbText = GameObject.Find("NutsAndBoltsAmount").GetComponent<TMP_Text>();
        respawnPoint = GameObject.Find("RespawnPoint").transform;
        rb = GetComponent<Rigidbody2D>();

        fToInteract = GameObject.Find("FToInteract");
		workbenchUI = gameObject.FindDeactivatedGameObject("Canvas", "WorkbenchUI");
        workbenchUI.SetActive(false);

        InputManager.INPUT_ACTIONS.Main.Interact.started += Interact;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Keyboard.current.numpadPlusKey.wasPressedThisFrame)
            ChangeNutsAndBolts(1000000);
#endif

        fToInteract.SetActive(itemsToPickup.Count > 0 || nearbyRepairables.Count > 0 || workbenchNearby);

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
            if (!repairable.IsRepaired() && !nearbyRepairables.Contains(repairable))
                nearbyRepairables.Add(repairable);
        }

        if (collision.CompareTag("Workbench"))
        {
            workbenchNearby = true;
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

		if (collision.CompareTag("Workbench"))
		{
			workbenchNearby = false;
            workbenchUI.SetActive(false);
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
        if (nearbyRepairables.Count > 0)
        {
			foreach (Repairable repairable in nearbyRepairables)
			{
				bool foundSomething = false;
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
			return;
		}

        // Workbench
        if (workbenchNearby)
        {
            workbenchUI.SetActive(!workbenchUI.activeSelf);
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
	public void AddMaxHealth(float value) => SetMaxHealth(maxHealth + value);
	public void SetMaxHealth(float value)
	{
        maxHealth = value;
		health = value;
		healthBar.SetMaxHealth(value);
	}

    public bool HoveringWorkbench()
    {
        return workbenchUI.activeSelf &&
		    InputManager.INPUT_ACTIONS.Main.MousePosition.ReadValue<Vector2>().x 
            < workbenchUI.GetComponent<RectTransform>().sizeDelta.x * 2f + 110f;
	}

}
