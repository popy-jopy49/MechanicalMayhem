using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class PuzzleGrid : MonoBehaviour
{
    public GridObject[,] grid;
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private Vector2 gridObjectSize;
    [SerializeField] private string fileName = "";
    [SerializeField] private string comparisonFileName = "";
    [SerializeField] private Transform parent;
    [SerializeField] private TextPrefabDigit[] textPrefabDigits;
    private Vector2 gridOffset;

    private void Awake()
    {
        string text = GetTextAtPath(fileName);
        InitialiseGrid(text);
    }

    private void InitialiseGrid(string text)
	{
        gridOffset = (gridSize * gridObjectSize - gridObjectSize) / 2f;
		gridOffset.x *= -1;
		grid = new GridObject[(int)gridSize.x, (int)gridSize.y];
        Vector3 scale = gridSize / 2f;
        scale.z = 1;
		transform.parent.Find("Background").localScale = scale;

		int dataIndex = 0;
		for (int y = 0; y < grid.GetLength(1); y++)
		{
			for (int x = 0; x < grid.GetLength(0); x++)
			{
				char digit = text[dataIndex];
				Vector2 pos = GridToWorldPos((x, y));
				grid[x, y] = new GridObject(digit, pos, gridObjectSize, parent, textPrefabDigits);
				dataIndex++;
			}
		}
	}

    private string GetTextAtPath(string path)
	{
		StreamReader reader = new("Assets/Resources/" + path);
		string text = reader.ReadToEnd().Trim().Replace("\n", "").Replace("\r", "");
		reader.Close();
		return text;
    }

    public bool ValidMovePosition((int x, int y) index)
    {
        if (!IsValidGridPosition(index))
            return false;

        bool free = grid[index.x, index.y].GetDigit() == '1';
        bool neighboursHavePlayer = false;

		List<(int x, int y)> neighbours = new()
        {
            (index.x + 1, index.y),
            (index.x - 1, index.y),
            (index.x, index.y + 1),
            (index.x, index.y - 1),
        };
        foreach ((int x, int y) neighbour in neighbours)
		{
			if (!IsValidGridPosition(neighbour))
				continue;

			if (grid[neighbour.x, neighbour.y].hasPlayer)
			{
				neighboursHavePlayer = true;
				break;
			}
		}

		return free && neighboursHavePlayer;
	}

    public bool AreNeighbours((int x, int y) firstIndex, (int x, int y) secondIndex)
    {
        if (!IsValidGridPosition(firstIndex) || !IsValidGridPosition(secondIndex) || firstIndex == secondIndex)
            return false;

        return (Mathf.Abs(firstIndex.x - secondIndex.x) == 1 && firstIndex.y == secondIndex.y) ^
            (Mathf.Abs(firstIndex.y - secondIndex.y) == 1 && firstIndex.x == secondIndex.x);
    }

	public (int x, int y) WorldToGridPos(Vector2 pos)
	{
		Vector2 index = pos;
		index -= gridOffset;
		index /= gridObjectSize;
		index.y *= -1;
		return (Mathf.RoundToInt(index.x), Mathf.RoundToInt(index.y));
	}

	public Vector2 GridToWorldPos((int x, int y) index)
	{
		return new Vector2(index.x, -index.y) * gridObjectSize + gridOffset;
	}

    public bool CompareGrid()
    {
        string text = GetTextAtPath(comparisonFileName);
        int index = 0;
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (index >= text.Length || grid[x, y].GetDigit() == text[index])
                    return false;

                index++;
            }
        }

        return true;
    }

	public bool IsValidGridPosition((int x, int y) index)
	{
		return index.x >= 0 && index.x < grid.GetLength(0) &&
			   index.y >= 0 && index.y < grid.GetLength(1);
	}

	public class GridObject
    {
        public bool hasPlayer = false;
		readonly bool wall = false;
		char digit;

        public GridObject(char digit, Vector2 pos, Vector2 size, Transform parent, TextPrefabDigit[] textPrefabDigits)
        {
            this.digit = digit;
            Transform prefab = null;
            foreach (TextPrefabDigit tPD in textPrefabDigits)
            {
                if (digit != tPD.digit)
                    continue;

                hasPlayer = tPD.player;
                wall = tPD.wall;
                prefab = tPD.prefab;
                break;
            }
            if (!prefab)
                return;

            Transform obj = Instantiate(prefab, pos, Quaternion.identity, parent);
            obj.localScale = size;
        }

        public bool OpenPos() => !wall;
        public char GetDigit() => digit;
        public void SetDigit(char digit) => this.digit = digit;
	}

    [Serializable]
    public struct TextPrefabDigit
    {
        public char digit;
        public Transform prefab;
        public bool wall;
        public bool player;
    }

}
