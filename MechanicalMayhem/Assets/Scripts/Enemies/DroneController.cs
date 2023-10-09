using UnityEngine;

public class DroneController : Enemy
{

	[Header("Drones")]
	[SerializeField] protected int maxDroneCount = 5;
	protected int droneCount;

	protected override void Attack()
	{
		GameObject drone = Instantiate(GameAssets.I.DronePrefab, transform.position, transform.rotation);
		//drone.GetComponent<Drone>().Setup(target, );
	}

}
