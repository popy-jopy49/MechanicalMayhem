using UnityEngine;

public class PuzzleWin : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.layer != LayerMask.NameToLayer("Puzzle"))
            return;

		// Win
		Win();
	}

    protected void Win()
    {
        print("You win!");
    }

}
