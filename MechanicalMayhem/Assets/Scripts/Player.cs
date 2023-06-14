using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    private float health;
    private HealthBar healthBar;

    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
    }

    private void Start()
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
