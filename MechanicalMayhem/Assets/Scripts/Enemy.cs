using SWAssets.Utils;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : Attackable
{

    [SerializeField] private float lookRange;
    [SerializeField] private float followRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;
    [SerializeField] private LayerMask whatToHit;
    [SerializeField] private bool useBullet;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float explosionRadius;

    private float time;
    private bool collidingWithEnemies = false;
    private Transform firePoint;
    private Rigidbody2D rb;
    private Vector3 dir;

    private void Awake()
    {
        firePoint = transform.Find("FirePoint");
        useBullet = firePoint;
        rb = GetComponent<Rigidbody2D>();
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

    private void FixedUpdate()
    {
        if (!collidingWithEnemies)
            rb.MovePosition(transform.position + (dir * movementSpeed));
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

        Vector3 dir = shortestCol.transform.position - transform.position;

        // Look
        float rot = VectorUtils.GetAngleFromVector(dir, true);
        transform.eulerAngles = new Vector3(0, 0, VectorUtils.LinearInterpolate(transform.eulerAngles.z, rot, Time.deltaTime * 10));

        if (shortestLength > followRange * followRange)
            return;

        if (shortestLength > attackRange * attackRange)
        {
            // Follow
            this.dir = dir;
            return;
        }

        // attack

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            collidingWithEnemies = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            collidingWithEnemies = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.back, lookRange);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.back, followRange);
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.back, attackRange);
    }

}
