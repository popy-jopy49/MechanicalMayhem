using SWAssets.Utils;
using UnityEngine;

public class Enemy : Attackable
{

    [SerializeField] private float lookRange;
    [SerializeField] private float followRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;
    [SerializeField] private LayerMask whatToHit;
    [SerializeField] private bool useBullet;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float explosionRadius;

    private float time;
    private Transform firePoint;

    private void Awake()
    {
        firePoint = transform.Find("FirePoint");
        useBullet = firePoint;
    }

    private void Update()
    {
        FindTarget();

        if (time < 1f / fireRate)
        {
            time += Time.deltaTime;
            return;
        }

        print("Hit");
    }

    private void FindTarget()
    {
        Collider2D[] lookCols = Physics2D.OverlapCircleAll(transform.position, lookRange, whatToHit);

        if (lookCols.Length <= 0)
            return;

        float shortestLength = float.MaxValue;
        Collider2D shortestCol = null;
        foreach (Collider2D col in lookCols)
        {
            float length = (col.transform.position - transform.position).sqrMagnitude;
            if (length <= shortestLength)
            {
                shortestLength = length;
                shortestCol = col;
            }
        }

        // Look
        float rot = VectorUtils.GetAngleFromVector(shortestCol.transform.position - transform.position, true);
        transform.eulerAngles = new Vector3(0, 0, VectorUtils.LinearInterpolate(transform.eulerAngles.z, rot, Time.deltaTime * 10));

        if (shortestLength > followRange * followRange)
            return;

        if (shortestLength > attackRange * attackRange)
        {
            // Follow

            return;
        }

        // attack
    }
}
