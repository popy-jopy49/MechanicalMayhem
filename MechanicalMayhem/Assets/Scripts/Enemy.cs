using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

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
    private Transform target;
    private Rigidbody2D rb;

    private void Awake()
    {
        firePoint = transform.Find("FirePoint");
        target = GameObject.Find("Player").transform;
        useBullet = firePoint;
        rb = GetComponent<Rigidbody2D>();
    }
     private void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        Vector2 vDir = target.position - transform.position;
        vDir.Normalize();
        float dir = Mathf.Atan2(vDir.y, vDir.x) * Mathf.Rad2Deg;

        if (distance <= followRange)
        {
            if (!collidingWithEnemies)
                rb.MovePosition(rb.position + (distance * Time.deltaTime * vDir));
            rb.MoveRotation(dir);
        }
        else
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }

        if (time < 1f / fireRate)
        {
            time += Time.fixedDeltaTime;
            return;
        }

        print("Hit");
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            collidingWithEnemies = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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
