using UnityEngine;

public class RangedEnemy : Enemy {
    
    [SerializeField] private float distanceToShoot = 5f;
    [SerializeField] private float distanceToStop = 3f;

    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float explosionRadius = 0f;
    [SerializeField] private LayerMask whatToHit;
    
    private Transform firePoint;
    private float timeToFire;

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
            Shoot();
        }
    }

    protected void Shoot()
    {
        if (timeToFire < 1 / fireRate)
        {
            timeToFire += Time.deltaTime;
            return;
        }

        GameObject bullet = Instantiate(GameAssets.I.EnemyBulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Setup(firePoint.up * bulletSpeed, damage, explosionRadius, whatToHit, "EnemyBullet");
        Destroy(bullet, 5f);
        timeToFire = 0f;
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
