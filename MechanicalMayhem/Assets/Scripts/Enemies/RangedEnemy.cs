using UnityEngine;

public class RangedEnemy : Enemy {
    
    [SerializeField] private float distanceToShoot = 5f;
    [SerializeField] private float distanceToStop = 3f;

    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float explosionRadius = 0f;
    [SerializeField] protected LayerMask everthingButSelf;
    [SerializeField] private LayerMask whatToHit;
    
    private Transform firePoint;

    protected override void Awake()
    {
        base.Awake();
        firePoint = transform.Find("FirePoint");
    }

    protected virtual void Update()
    {
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

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.up, 50f, everthingButSelf);
        if (!hit2D.transform)
            goto TIME;

        if (hit2D.transform.gameObject.CompareTag("Player"))
        {
            GameObject bullet = Instantiate(GameAssets.I.BulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().Setup(firePoint.up * bulletSpeed, damage, explosionRadius, whatToHit, "EnemyBullet");
            Destroy(bullet, 5f);
        }

        TIME:
        time = 0f;
    }

}
