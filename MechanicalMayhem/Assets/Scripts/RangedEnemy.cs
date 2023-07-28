using UnityEngine;

public class RangedEnemy : Enemy {
    
    [SerializeField] private float distanceToShoot = 5f;
    [SerializeField] private float distanceToStop = 3f;

    [SerializeField] private float fireRate;
    [SerializeField] private float timeToFire;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float explosionRadius = 0f;
    [SerializeField] private LayerMask whatToHit;
    
    private Transform firePoint;

    protected override void Start()
    {
        base.Start();
        firePoint = transform.Find("FirePoint");
    }

    protected override void Update()
    {
        base.Update();

        float sqrDist = Vector2.SqrMagnitude(target.position - transform.position);
        if (sqrDist <= distanceToStop * distanceToStop)
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
        bullet.GetComponent<Bullet>().Setup(firePoint.rotation.eulerAngles * bulletSpeed, damage, explosionRadius, whatToHit, "EnemyBullet");
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

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            return;
        }
    }
}
