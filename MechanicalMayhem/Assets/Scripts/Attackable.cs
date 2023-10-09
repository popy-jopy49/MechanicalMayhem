using UnityEngine;

public class Attackable : MonoBehaviour
{

    protected float health;
    [Header("Health")]
    [SerializeField] protected float maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    public virtual bool Damage(float damage)
    {
        health -= damage;
        if (health > 0)
            return false;

        Die();
        return true;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}
