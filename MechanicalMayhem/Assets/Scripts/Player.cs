using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    private float health;
    [SerializeField] private HealthBar healthBar;

    private void Awake()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            health -= 10;
            healthBar.SetHealth(health);
        }
    }

}
