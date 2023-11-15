using UnityEngine;

public class RangedEnemy : Enemy {

	[SerializeField] private LayerMask whatToHit;

	[Header("Bullet Values")]
	[SerializeField] private float bulletSpread = 0.5f;
	[SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float explosionRadius = 0f;

	protected Transform firePoint;

    protected override void Awake()
    {
        base.Awake();
        firePoint = transform.Find("FirePoint");
    }

    protected override void Attack()
    {
		// Spawn bullet
        GameObject bullet = Instantiate(GameAssets.I.BulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Setup(GetFireDirection() * bulletSpeed, damage, explosionRadius, whatToHit, "EnemyBullet");
        Destroy(bullet, 5f);
	}

	// Direction of fire + bullet spread
	protected Vector2 GetFireDirection()
	{
		float bulletSpread = Random.Range(-this.bulletSpread, this.bulletSpread);
		Vector3 targetPos = new(
			transform.up.x + bulletSpread,
			transform.up.y + bulletSpread
			);

		return targetPos;
	}

}
