using UnityEngine;

public class RangedEnemy : Enemy {
    
    [SerializeField] private float distanceToShoot = 5f;
    [SerializeField] private float distanceToStop = 3f;

    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float explosionRadius = 0f;
    [SerializeField] private LayerMask whatToHit;
    
    private Transform firePoint;

    protected override void Awake()
    {
        base.Awake();
        firePoint = transform.Find("FirePoint");
    }

    protected override void Update()
    {
        base.Update();

        float sqrDist = Vector2.SqrMagnitude(target.position - transform.position);
        if (sqrDist <= distanceToShoot * distanceToShoot)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        if (!CheckForFireRate())
            return;

        GameObject bullet = Instantiate(GameAssets.I.BulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Setup(firePoint.up * bulletSpeed, damage, explosionRadius, whatToHit, "EnemyBullet");
        Destroy(bullet, 5f);
        time = 0f;
    }

    protected override void FixedUpdate()
    {
        if (!target)
            return;

        float sqrDist = Vector2.SqrMagnitude(target.position - transform.position);
        if (sqrDist >= distanceToStop * distanceToStop)
        {
            rb.velocity = transform.up * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

}
