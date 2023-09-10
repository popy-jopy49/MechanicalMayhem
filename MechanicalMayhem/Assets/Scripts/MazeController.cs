using UnityEngine;
using UnityEngine.EventSystems;

public class MazeController : MonoBehaviour, IDragHandler
{
	PuzzleGrid grid;
	private (int x, int y) prevIndex = default;

	private void Awake()
	{
		grid = transform.parent.parent.Find("Grid").GetComponent<PuzzleGrid>();
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 mousePos = InputManager.INPUT_ACTIONS.Main.MousePosition.ReadValue<Vector2>();
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		(int x, int y) index = grid.WorldToGridPos(mousePos);
		if (prevIndex == index)
			return;
		prevIndex = index;

		if (!grid.ValidMovePosition(index))
			return;

		// Valid move position
		// Find current player position
		(int x, int y) playerIndex = grid.WorldToGridPos(transform.position);

		// Switch hasPlayer around
		grid.grid[playerIndex.x, playerIndex.y].hasPlayer = false;
		grid.grid[index.x, index.y].hasPlayer = true;

		transform.position = grid.GridToWorldPos(index);
	}
}
