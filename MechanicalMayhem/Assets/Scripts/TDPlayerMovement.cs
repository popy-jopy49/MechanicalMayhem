using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class TDPlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float sprintSpeed = 4f;
    [SerializeField] private float maxSprint = 2f;
    [SerializeField] private float minSprint = 0.5f;
    
    private float sprint = 0;
    private bool isSprinting = false;
    private Rigidbody2D rb;
    private Vector2 input;
    private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprint = maxSprint;
		InputManager.INPUT_ACTIONS.Main.Sprint.started += SprintStarted;
		InputManager.INPUT_ACTIONS.Main.Sprint.canceled += SprintCanceled; ;
	}

	private void SprintCanceled(InputAction.CallbackContext obj)
	{
        isSprinting = false;
	}

	private void SprintStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
        if (sprint > minSprint)
            isSprinting = true;
	}

	private void Update()
    {
        input = InputManager.INPUT_ACTIONS.Main.Movement.ReadValue<Vector2>();
        speed = movementSpeed;

        if (isSprinting)
        {
            sprint -= Time.deltaTime;
            speed = sprintSpeed;
            if (sprint <= 0)
                isSprinting = false;
        }
        else
        {
            sprint += Time.deltaTime;
        }

        sprint = Mathf.Clamp(sprint, 0, maxSprint);
    }

    private void FixedUpdate()
    {
        Vector2 newPos = speed * Time.fixedDeltaTime * input;
        rb.MovePosition(rb.position + newPos);
    }

}
