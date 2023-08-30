using UnityEngine;

public class PuzzleWin : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TrafficJamController tJC = collision.GetComponent<TrafficJamController>();
        if (!tJC)
            return;

        if (tJC.GetTargetCar())
        {
            // Win
            Win();
            transform.parent.gameObject.SetActive(false);
        }
    }

    protected void Win()
    {

    }

}
