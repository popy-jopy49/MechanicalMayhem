using UnityEngine;
using UnityEngine.EventSystems;

public class MazeController : MonoBehaviour, IDragHandler
{
	PuzzleGrid grid;
	private (int x, int y) prevIndex = default;

	// Reference grid in puzzle
	private void Awake()
	{
		grid = transform.parent.parent.Find("Grid").GetComponent<PuzzleGrid>();
	}

	public void OnDrag(PointerEventData eventData)
	{
		// Grab mous input
		Vector2 mousePos = InputManager.INPUT_ACTIONS.Main.MousePosition.ReadValue<Vector2>();
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		// Convert to world pos
		(int x, int y) index = grid.WorldToGridPos(mousePos);
		if (prevIndex == index)
			return;
		prevIndex = index;

		// check if valid move position
		if (!grid.ValidMovePosition(index))
			return;

		// Valid move position
		// Find current player position
		(int x, int y) = grid.WorldToGridPos(transform.position);

		// Switch hasPlayer around
		grid.grid[x, y].hasPlayer = false;
		grid.grid[index.x, index.y].hasPlayer = true;

		// move to new pos
		transform.position = grid.GridToWorldPos(index);
	}
}
