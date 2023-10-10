using System;
using UnityEngine;

public class PuzzleWin : MonoBehaviour
{

    private Action winFunc;

    private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.layer != LayerMask.NameToLayer("Puzzle"))
            return;

        // Win
        winFunc();
	}

    public void SetWinFunc(Action winFunc) => this.winFunc = winFunc;

}
