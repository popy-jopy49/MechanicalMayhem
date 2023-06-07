using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TDPlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float sprintSpeed = 4f;
    [SerializeField] private float maxSprint = 2f;
    [SerializeField] private float minSprint = 0.5f;
    
    private float sprint = 0;
    private bool isSprinting = false;
    private float speedMultiplier = 10;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprint = maxSprint;
    }

    private void Update()
    {
        Vector2 input = InputManager.INPUT_ACTIONS.Main.Movement.ReadValue<Vector2>();
        isSprinting = InputManager.INPUT_ACTIONS.Main.Sprint.ReadValue<float>() != 0;
        float speed = movementSpeed;
        if (isSprinting)
        {
            sprint -= Time.deltaTime;
            if (sprint > minSprint)
            {
                speed = sprintSpeed;
            }
        }
        else
        {
            sprint += Time.deltaTime;
        }
        sprint = Mathf.Clamp(sprint, 0, maxSprint);

        Vector2 newPos = input * speed / speedMultiplier;
        rb.MovePosition(rb.position + newPos);
    }

}
