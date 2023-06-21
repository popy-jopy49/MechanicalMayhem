using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private float explosionRadius;
    private LayerMask whatToHit;

    public void Setup(Vector2 dir, float damage, float explosionRadius, LayerMask whatToHit)
    {
        this.damage = damage;
        this.explosionRadius = explosionRadius;
        this.whatToHit = whatToHit;
        GetComponent<Rigidbody2D>().velocity = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
		if (explosionRadius > 0)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, explosionRadius, whatToHit);
            if (cols.Length <= 0)
				return;

            foreach (Collider2D col in cols)
            {
                Attackable attackable = col.GetComponent<Attackable>();
                if (!attackable)
                    continue;

                attackable.Damage(damage);
            }

            // Spawn explosion effect

			return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            // Hit an enemy
            Attackable attackable = collision.transform.GetComponent<Attackable>();
            if (!attackable)
                return;

            attackable.Damage(damage);
            // Spawn hit effect

        }
        else
        {
			// Spawn wall hit effect

		}
	}

}
