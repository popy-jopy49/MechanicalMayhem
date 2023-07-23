using SWAssets.Utils;
using System.IO.Pipes;
using UnityEditor;
using UnityEngine;

public class Enemy : Attackable
{

    [SerializeField] private float collisionDetectionRange;
    [SerializeField] private float lookRange;
    [SerializeField] private float followRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;
    [SerializeField] private bool useBullet;
    [SerializeField] private LayerMask whatToHit;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float explosionRadius;

    private float time;
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
            if (!CollidingWithEntities())
            {
                rb.MovePosition(rb.position + (distance * Time.deltaTime * vDir));
            }
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

        // Spawn Bullet
        print("hit");
        /*Quaternion bulletDirection = Quaternion.Euler(new Vector3(0, 0, dir - 90f));
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletDirection);
        bullet.GetComponent<Bullet>().Setup(vDir * bulletSpeed, damage, explosionRadius, whatToHit);
        Destroy(bullet, 5f);*/
    }

    private bool CollidingWithEntities()
    {
        // Check forward
        RaycastHit2D hitf = Physics2D.Raycast(transform.position, transform.up, collisionDetectionRange, whatToHit);
        if (hitf && hitf.transform.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            return true;
        }
        // Check left
        RaycastHit2D hitl = Physics2D.Raycast(transform.position, -transform.right, collisionDetectionRange, whatToHit);
        if (hitl && hitl.transform.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            return true;
        }
        // Check right
        RaycastHit2D hitr = Physics2D.Raycast(transform.position, transform.right, collisionDetectionRange, whatToHit);
        if (hitr && hitr.transform.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            return true;
        }
        return false;
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
