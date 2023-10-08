using SWAssets.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMesh))]
public class Tank : Repairable
{

    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;
    [SerializeField] private LayerMask whatToHit;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float explosionRadius;
    [SerializeField] protected float stopDistance = 2.25f;
    [SerializeField] protected float seekDistance = 3.5f;

    private float time = 0;
    private Transform firePoint;
    protected Transform target;
    protected NavMeshAgent agent;

    protected void Awake()
    {
        firePoint = transform.Find("FirePoint");
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected override void RepairedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, whatToHit);

        if (colliders.Length <= 0)
            return;

        Transform collider = colliders[0].transform;
        Vector2 vDist = Vector2.zero;
        float minDist = Mathf.Infinity;
        foreach (Collider2D col in colliders)
        {
            vDist = col.transform.position - transform.position;
            float sqrDist = vDist.sqrMagnitude;
            if (sqrDist < minDist)
            {
                minDist = sqrDist;
                collider = col.transform;
            }
        }

        target = collider;
        if (minDist > seekDistance * seekDistance)
            return;

        agent.isStopped = minDist <= stopDistance * stopDistance;
        agent.SetDestination(target.position);

        Vector3 vDir = agent.desiredVelocity;
        if (agent.isStopped)
            vDir = vDist;

        float rot = Mathf.Atan2(vDir.y, vDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rot - 90f), Time.deltaTime * agent.angularSpeed);

        if (time < 1 / fireRate)
        {
            time += Time.deltaTime;
            return;
        }
        time = 0;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, vDir, 50f, whatToHit);

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
