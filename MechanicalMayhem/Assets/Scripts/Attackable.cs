using UnityEngine;

public class Attackable : MonoBehaviour
{

    protected float health;
    [SerializeField] protected float maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    public virtual void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}
