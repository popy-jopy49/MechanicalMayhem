using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;

    public void Setup(Vector2 dir, float damage)
    {
        print(dir);
        this.damage = damage;
        print(GetComponent<Rigidbody2D>());
        GetComponent<Rigidbody2D>().velocity = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collide");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            print("enemy");
            // Hit an enemy
            Attackable attackable = collision.transform.GetComponent<Attackable>();

            attackable.Damage(damage);
            Destroy(gameObject);
            // Spawn hit affect
        }
    }

}
