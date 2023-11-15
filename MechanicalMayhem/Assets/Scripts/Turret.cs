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

	protected override void Awake()
	{
		base.Awake();
		firePoint = transform.Find("FirePoint");
	}

	protected override void RepairedUpdate()
	{
		// check all targets in range
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, whatToHit);

		if (colliders.Length <= 0)
			return;

		// Check for closest and whether they are through a wall or not
		Vector2 vDist = Vector2.zero;
		float minDist = Mathf.Infinity;
		foreach (Collider2D col in colliders)
		{
			Vector2 vDistT = col.transform.position - transform.position;
			float sqrDist = vDistT.sqrMagnitude;
			if (sqrDist >= minDist)
				continue;

			RaycastHit2D hit = Physics2D.Raycast(transform.position, vDistT, range, whatToHit);

			if (!hit || hit.transform.gameObject.layer != LayerMask.NameToLayer("Enemies"))
				continue;

			vDist = vDistT;
			minDist = sqrDist;
		}

		// Shoot and rotate if in range
		if (minDist > range)
			return;

        float rot = Mathf.Atan2(vDist.y, vDist.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rot), Time.deltaTime * rotateSpeed);

		if (time < 1 / fireRate)
		{
			time += Time.deltaTime;
			return;
		}
		time = 0;

		// Spawn bullet
		Quaternion bulletDirection = Quaternion.Euler(new Vector3(0, 0, VectorUtils.GetAngleFromVector(firePoint.right) - 90f));
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletDirection);
		bullet.GetComponent<Bullet>().Setup(firePoint.right * bulletSpeed, damage, explosionRadius, whatToHit, "PlayerBullet");
		Destroy(bullet, 5f);
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
    {
		Handles.color = Color.red;
		Handles.DrawWireDisc(transform.position, Vector3.back, range);
    }
#endif

}
