using SWAssets.Utils;
using UnityEditor;
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

		Vector2 vDir = collider.transform.position - transform.position;
        float rot = VectorUtils.GetAngleFromVector(vDir, true);
		transform.eulerAngles = new Vector3(0, 0, VectorUtils.LinearInterpolate(transform.eulerAngles.z, rot, Time.deltaTime * 10));

		if (time < 1 / fireRate)
		{
			time += Time.deltaTime;
			return;
		}

		time = 0;

		RaycastHit2D hit = Physics2D.Raycast(firePoint.position, vDir, 50f, whatToHit);

		print(hit.transform.gameObject.layer);
		if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Enemies"))
			return;

		// Spawn muzzle flash

		// Spawn bullet
		Quaternion bulletDirection = Quaternion.Euler(new Vector3(0, 0, VectorUtils.GetAngleFromVector(firePoint.right) - 90f));
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletDirection);
		bullet.GetComponent<Bullet>().Setup(firePoint.right * bulletSpeed, damage, explosionRadius, whatToHit, "PlayerBullet");
		Destroy(bullet, 5f);
	}

    private void OnDrawGizmosSelected()
    {
		Handles.color = Color.red;
		Handles.DrawWireDisc(transform.position, Vector3.back, range);
    }

}
