using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float maxHealth;

    private List<GameObject> itemsToPickup = new List<GameObject>();
    private List<Repairable> repairables = new List<Repairable>();
    private List<GameObject> inventory = new List<GameObject>();
    private float health;
    private HealthBar healthBar;

    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        InputManager.INPUT_ACTIONS.Main.Interact.started += Interact;
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        //if ()
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            health -= 10;
            healthBar.SetHealth(health);
        }

        Vector3 mousePos = InputManager.INPUT_ACTIONS.Main.MousePosition.ReadValue<Vector2>();
        mousePos.z = 0.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos -= transform.position;
        float rot = SWAssets.Utils.VectorUtils.GetAngleFromVector(mousePos);
        transform.eulerAngles = new Vector3(0, 0, rot - 90);
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
            if (!repairables.Contains(repairable))
                repairables.Add(repairable);
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
            if (repairables.Contains(repairable))
                repairables.Remove(repairable);
        }
    }

    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (itemsToPickup.Count > 0)
        {
            // Pick up
            inventory.Add(itemsToPickup[0]);
            itemsToPickup.RemoveAt(0);
            return;
        }

        // Repair
        if (repairables.Count > 0)
        {
            for (int i = 0; i < itemsToPickup.Count; i++)
            {
                if (repairables[0].AddItem(itemsToPickup[i]))
                    repairables.RemoveAt(0);
            }
        }
    }

}
