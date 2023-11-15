using SWAssets.Utils;
using UnityEngine;

public class Door : Repairable
{

	[SerializeField] private Vector2 moveDirection = Vector2.right;
	[SerializeField] private float speed = 10;
	private Vector2 newPos;
	
	// Set new pos and play sound
	protected override void OnRepairGFX()
	{
		newPos = (Vector2)transform.position + moveDirection * Mathf.Max(transform.localScale.x, transform.localScale.y);
		SoundManager.I.PlaySound("DoorOpening");
	}

    // Move and open door
    protected override void RepairedUpdate()
	{
		transform.position = VectorUtils.LinearInterpolate((Vector2)transform.position, newPos, Time.deltaTime * speed);
	}

}
