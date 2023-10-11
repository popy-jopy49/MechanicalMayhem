using SWAssets.Utils;
using UnityEngine;

public class Door : Repairable
{

	[SerializeField] private float modifier = 1;
	[SerializeField] private float speed = 10;
	private Vector2 newPos;

	protected override void OnRepairGFX()
	{
		newPos = transform.position + (modifier * transform.localScale.x * transform.right);
	}

	protected override void RepairedUpdate()
	{
		transform.position = VectorUtils.LinearInterpolate((Vector2)transform.position, newPos, Time.deltaTime * speed);
	}

}
