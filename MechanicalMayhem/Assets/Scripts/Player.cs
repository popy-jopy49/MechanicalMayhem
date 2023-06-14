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

}
