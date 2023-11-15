using System.Collections.Generic;
using UnityEngine;

public class DroneController : Enemy
{

	[Header("Drones")]
	[SerializeField] protected int maxDroneCount = 5;
	protected List<GameObject> drones = new();
	protected int droneCount;
	protected Transform firePoint;

	protected override void Awake()
	{
		base.Awake();
		firePoint = transform.Find("FirePoint");
	}

	// Spawn drone if less than max drone count
	protected override void Attack()
	{
		if (droneCount >= maxDroneCount)
			return;

		GameObject drone = Instantiate(GameAssets.I.GetRandomPrefab(GameAssets.I.DronePrefabs), firePoint.position, firePoint.rotation);
		droneCount++;
		drones.Add(drone);
	}

	// Check if drones have been destroyed and update accordingly
	protected override void Update()
	{
		base.Update();
		for (int i = drones.Count - 1; i > 0; i--)
		{
			if (drones[i])
				continue;

			droneCount--;
			drones.RemoveAt(i);
		}
	}

}
