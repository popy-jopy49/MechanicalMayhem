using SWAssets.Utils;
using UnityEngine;

public class Drone : Tank
{

	private Transform[] firePoints;
	private byte currentTurret = 0;

	protected override void Attack()
	{
		currentTurret = (byte)Mathf.Abs(currentTurret - 1);
		// Spawn bullet
		Quaternion bulletDirection = Quaternion.Euler(new Vector3(0, 0, VectorUtils.GetAngleFromVector(firePoints[currentTurret].right) - 90f));
		GameObject bullet = Instantiate(bulletPrefab, firePoints[currentTurret].position, bulletDirection);
		bullet.GetComponent<Bullet>().Setup(firePoints[currentTurret].right * bulletSpeed, damage, explosionRadius, whatToHit, "PlayerBullet", this);
		Destroy(bullet, 5f);
	}

	protected override void GetFirePoint()
	{
		firePoints = new Transform[] { transform.Find("FirePoint1"), transform.Find("FirePoint2") };
	}

}
