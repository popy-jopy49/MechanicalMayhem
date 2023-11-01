using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageController : MonoBehaviour, IDragHandler
{

	private Action winFunc;

	PuzzleGrid grid;
	private (int x, int y) prevIndex = default;

	private void Awake()
	{
		grid = transform.parent.parent.Find("Grid").GetComponent<PuzzleGrid>();
	}

	public void OnDrag(PointerEventData eventData)
	{
		// Grab mouse position
		Vector2 mousePos = InputManager.INPUT_ACTIONS.Main.MousePosition.ReadValue<Vector2>();
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		// Get grid position at mouse pos
		(int x, int y) targetIndex = grid.WorldToGridPos(mousePos);
		if (prevIndex == targetIndex)
			return;
		prevIndex = targetIndex;

		(int x, int y) currentIndex = grid.WorldToGridPos(transform.position);

		// Check for valid move position
		if (!grid.IsValidGridPosition(targetIndex) 
			|| !grid.AreNeighbours(targetIndex, currentIndex) 
			|| grid.grid[targetIndex.x, targetIndex.y].GetDigit() != 'X')
			return;

		// Switch digit around
		char thisDigit = grid.grid[currentIndex.x, currentIndex.y].GetDigit();
		grid.grid[currentIndex.x, currentIndex.y].SetDigit(grid.grid[targetIndex.x, targetIndex.y].GetDigit());
		grid.grid[targetIndex.x, targetIndex.y].SetDigit(thisDigit);

		// Valid move position
		transform.position = grid.GridToWorldPos(targetIndex);
		if (grid.CompareGrid())
		{
			// Win
			winFunc();
        }
	}

	public void SetWinFunc(Action winFunc) => this.winFunc = winFunc;

}
