﻿using UnityEngine;

public class Node : IHeapItem<Node>
{
	
	public bool walkable;
	public Vector2 worldPosition;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public Node parent;
	int heapIndex;
	
	public Node(bool _walkable, Vector2 _worldPos, int _gridX, int _gridY)
	{
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	public int FCost
	{
		get => gCost + hCost;
	}

	public int HeapIndex
	{
		get => heapIndex;
		set => heapIndex = value;
	}

	public int CompareTo(Node nodeToCompare)
	{
		int compare = FCost.CompareTo(nodeToCompare.FCost);
		if (compare == 0) {
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}
		return -compare;
	}
}