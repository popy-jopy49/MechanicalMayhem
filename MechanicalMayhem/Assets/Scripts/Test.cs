using UnityEngine;

public class Test : MonoBehaviour
{

	private void Awake()
	{
		PuzzleGrid.Setup(GameAssets.I.ImagePuzzle, () => { });
	}

}
