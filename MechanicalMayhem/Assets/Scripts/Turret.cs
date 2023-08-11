using SWAssets.Utils;
using UnityEditor;
using UnityEngine;

public class Turret : Repairable
{

	[SerializeField] private float range;
	[SerializeField] private float rotateSpeed = 10f;
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
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, whatToHit);

		if (colliders.Length <= 0)
			return;

		Transform collider = colliders[0].transform;
		float minDist = Mathf.Infinity;
		foreach (Collider2D col in colliders)
		{
			float sqrDist = (col.transform.position - transform.position).sqrMagnitude;
            if (sqrDist < minDist)
			{
				minDist = sqrDist;
				collider = col.transform;
			}
		}

		Vector2 vDir = collider.position - transform.position;
        float rot = Mathf.Atan2(vDir.y, vDir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rot), Time.deltaTime * rotateSpeed);

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
