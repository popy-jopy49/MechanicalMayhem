using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private float explosionRadius;
    private LayerMask whatToHit;
	private Repairable repairable;
	private bool killedTarget = false;

	// Sets variables in bullet
    public void Setup(Vector2 dir, float damage, float explosionRadius, LayerMask whatToHit, string layer, Repairable repairable = null)
    {
        this.damage = damage;
        this.explosionRadius = explosionRadius;
        this.whatToHit = whatToHit;
        gameObject.layer = LayerMask.NameToLayer(layer);
        GetComponent<Rigidbody2D>().velocity = dir;
		this.repairable = repairable;
    }

    private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
        // When colliding, explode if it is a bomb bullet
        if (explosionRadius > 0)
        {
            Explode();
			return;
        }

		// Check to see if hit enemy
		if (HitEnemy(collision.gameObject))
			return;

		// Check to see if hit player
		if (HitPlayer(collision.gameObject))
			return;

		// TODO: Spawn a wall hit effect

	}

	// Get list of colliders and attack each one
	private void Explode()
	{
		Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, explosionRadius, whatToHit);
		if (cols.Length <= 0)
			return;

		foreach (Collider2D col in cols)
		{
			// Check to see if obstructed by wall
			RaycastHit2D hit2D = Physics2D.Raycast(transform.position, col.transform.position - transform.position);
			if (!hit2D)
				continue;
			
			if (gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
				HitEnemy(col.gameObject);
			if (gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
				HitPlayer(col.gameObject);
		}

		// TODO: Spawn an explosion effect

	}

	// See if player and damage if true
    private bool HitPlayer(GameObject gameObject)
	{
		Player player = gameObject.GetComponent<Player>();
		if (!player)
			return false;

		player.Damage(damage);
		// TODO: Spawn player hit affect

		return true;
	}

	// See if attackable and damage if true
    private bool HitEnemy(GameObject gameObject)
	{
		Attackable attackable = gameObject.GetComponent<Attackable>();
		if (!attackable)
			return false;

		killedTarget = attackable.Damage(damage);
		// TODO: Spawn an enemy hit effect

		return true;
	}

	private void OnDestroy()
	{
		if (repairable && killedTarget)
			repairable.Use();
	}

}
