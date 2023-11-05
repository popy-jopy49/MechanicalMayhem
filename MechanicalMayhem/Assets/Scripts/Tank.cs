using SWAssets.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Tank : Repairable
{

	[Header("Shooting Values")]
	[SerializeField] protected float damage;
	[SerializeField] protected float fireRate;
	[SerializeField] protected LayerMask whatToHit;
	[SerializeField] protected GameObject bulletPrefab;
	[SerializeField] protected float bulletSpeed;
	[SerializeField] protected float explosionRadius;

	[Header("Navigation Values")]
	[SerializeField] protected float distanceToStop = 2.25f;
	[SerializeField] protected float distanceToSeek = 10f;

	protected float time = 0;
	private Transform firePoint;
	protected NavMeshAgent agent;

	protected override void Awake()
	{
		base.Awake();
		GetFirePoint();
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
	}

	protected override void RepairedUpdate()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distanceToSeek, whatToHit);

		if (colliders.Length <= 0)
			return;

		Transform target = colliders[0].transform;
		Vector2 vDist = Vector2.zero;
		float minDist = Mathf.Infinity;
		foreach (Collider2D col in colliders)
		{
			Vector2 vDistT = col.transform.position - transform.position;
			float sqrDist = vDistT.sqrMagnitude;
			if (sqrDist >= minDist)
				continue;

			RaycastHit2D hit = Physics2D.Raycast(transform.position, vDistT, distanceToSeek, whatToHit);

			if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Enemies"))
				continue;

			vDist = vDistT;
			minDist = sqrDist;
			target = col.transform;
		}

		if (minDist > distanceToSeek * distanceToSeek)
			return;

		agent.isStopped = minDist <= distanceToStop * distanceToStop;
		agent.SetDestination(target.position);

		float rot = Mathf.Atan2(vDist.y, vDist.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rot), Time.deltaTime * agent.angularSpeed);

		if (!agent.isStopped)
			return;

		if (time < 1 / fireRate)
		{
			time += Time.deltaTime;
			return;
		}
		time = 0;

		Attack();
	}

	protected virtual void Attack()
	{
		// Spawn bullet
		Quaternion bulletDirection = Quaternion.Euler(new Vector3(0, 0, VectorUtils.GetAngleFromVector(firePoint.right) - 90f));
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletDirection);
		bullet.GetComponent<Bullet>().Setup(firePoint.right * bulletSpeed, damage, explosionRadius, whatToHit, "PlayerBullet", this);
		Destroy(bullet, 5f);
	}

	protected virtual void GetFirePoint()
	{
		firePoint = transform.Find("FirePoint");
	}

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
	{
		Handles.color = Color.red;
		Handles.DrawWireDisc(transform.position, Vector3.back, distanceToSeek);
		Handles.color = Color.green;
		Handles.DrawWireDisc(transform.position, Vector3.back, distanceToStop);
	}
#endif

}
