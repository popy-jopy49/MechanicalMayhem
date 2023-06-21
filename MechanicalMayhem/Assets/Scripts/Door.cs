using SWAssets.Utils;
using UnityEditor.Rendering;
using UnityEngine;

public class Door : Repairable
{

	[SerializeField] private Vector2 modifier = new(1, 0);
	[SerializeField] private float speed = 10;
	private Vector2 newPos;

	protected override void OnRepair()
	{
		repaired = true;
		newPos = (Vector2)transform.position + (transform.localScale * modifier);
	}

	protected override void RepairedUpdate()
	{
		transform.position = VectorUtils.LinearInterpolate((Vector2)transform.position, newPos, Time.deltaTime * speed);
	}

}
