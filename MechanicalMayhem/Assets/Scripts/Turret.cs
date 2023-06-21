using SWAssets.Utils;
using UnityEngine;

public class Turret : Repairable
{

	[SerializeField] private float range;
	[SerializeField] private float damage;
	[SerializeField] private float fireRate;
	[SerializeField] private LayerMask whatToHit;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float bulletSpeed;
	[SerializeField] private float explosionRadius;

	private float time = 0;
	private Transform firePoint;

	protected void Awake()
	{
		firePoint = transform.Find("FirePoint");
	}

	protected override void RepairedUpdate()
	{
		Collider2D collider = Physics2D.OverlapCircle(transform.position, range, whatToHit);

		if (!collider)
			return;

		float rot = VectorUtils.GetAngleFromVector(collider.transform.position - transform.position, true);
		transform.eulerAngles = new Vector3(0, 0, VectorUtils.LinearInterpolate(transform.eulerAngles.z, rot, Time.deltaTime * 10));

		if (time < 1 / fireRate)
		{
			time += Time.deltaTime;
			return;
		}

		time = 0;

		// Spawn muzzle flash

		// Spawn bullet
		Quaternion bulletDirection = Quaternion.Euler(new Vector3(0, 0, VectorUtils.GetAngleFromVector(firePoint.right) - 90f));
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletDirection);
		bullet.GetComponent<Bullet>().Setup(firePoint.right * bulletSpeed, damage, explosionRadius, whatToHit);
		Destroy(bullet, 5f);
	}

}
