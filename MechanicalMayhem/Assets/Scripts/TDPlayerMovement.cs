using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class TDPlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float sprintSpeed = 4f;
    [SerializeField] private float regainSpeed = 0.5f;
    [SerializeField] private float loseSpeed = 0.5f;
    [SerializeField] private float minSprint = 0.5f;
	private float maxSprint = 2f;

	public static event Action<float, float, float> OnSprintChanged;
    
    private float sprint = 0;
    private bool isSprinting = false;
    private Rigidbody2D rb;
    private Vector2 input;
    private float speed;

    private Animator anim;
    private string currentState = IDLE_STATE;
    private string currentDir = LEFTRIGHT;
    const string LEFTRIGHT = "Right";
    const string UP = "Up";
    const string DOWN = "Down";
    const string IDLE_STATE = "Idle";
    const string WALK_STATE = "Walk";
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprint = maxSprint;
		InputManager.INPUT_ACTIONS.Main.Sprint.started += SprintStarted;
		InputManager.INPUT_ACTIONS.Main.Sprint.canceled += SprintCanceled;
	}

	private void SprintCanceled(InputAction.CallbackContext obj)
	{
        isSprinting = false;
	}

	private void SprintStarted(InputAction.CallbackContext obj)
	{
        if (sprint > minSprint) // Sprint if valid
            isSprinting = true;
	}

    // Gets input, deals with sprinting, handles animation
	private void Update()
	{
		input = InputManager.INPUT_ACTIONS.Main.Movement.ReadValue<Vector2>().normalized;
		Sprint();
		if (GameManager.I.InPuzzle())
		{
			ChangeAnimationState(IDLE_STATE, LEFTRIGHT);
			return;
		}

		HandlePlayerAnimation();
    }

    private void HandlePlayerAnimation()
    {
        // Sets animation based on input
        if (input == Vector2.zero)
        {
            ChangeAnimationState(IDLE_STATE, currentDir);
            return;
        }
        if (input.y > 0)
        {
            ChangeAnimationState(WALK_STATE, UP);
            return;
        }
        if (input.y < 0)
        {
            ChangeAnimationState(WALK_STATE, DOWN);
            return;
        }
        if (input.x != 0)
        {
            ChangeAnimationState(WALK_STATE, LEFTRIGHT);
            Vector3 scale = transform.GetChild(0).localScale;
            scale.x = input.x > 0 ? 1 : -1;
            transform.Find("GFX").localScale = scale;
            return;
        }
    }

    private void Sprint()
    {
        speed = movementSpeed;

        // Inputing, moving, and not in a puzzle
        if (isSprinting && input != Vector2.zero && !GameManager.I.InPuzzle())
        {
            // Sprint
            sprint -= Time.deltaTime * loseSpeed;
            speed = sprintSpeed;
            if (sprint <= 0)
                isSprinting = false;
        }
        else
        {
            // Regain sprint
            sprint += Time.deltaTime * regainSpeed;
        }

        sprint = Mathf.Clamp(sprint, 0, maxSprint);

        // Update sprint bar
        OnSprintChanged?.Invoke(sprint, maxSprint, minSprint);
    }

    // Move player
    private void FixedUpdate()
	{
		if (GameManager.I.InPuzzle())
			return;
		Vector2 newPos = speed * Time.fixedDeltaTime * input;
        rb.MovePosition(rb.position + newPos);
    }

    // change animation
    private void ChangeAnimationState(string newState, string newDir)
    {
        string state = newState + newDir;
        if (state == currentState + currentDir)
            return;

        anim.Play(state);
        currentState = newState;
        currentDir = newDir;
    }

    public void SetMaxSprint(float value) => maxSprint = value;

}
