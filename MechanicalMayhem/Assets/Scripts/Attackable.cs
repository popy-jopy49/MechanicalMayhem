using UnityEngine;

public class Attackable : MonoBehaviour
{

    protected float health;
    [Header("Health")]
    [SerializeField] protected float maxHealth;

    // Initialise health
    private void Start()
    {
        health = maxHealth;
    }

    // Damage function for other scripts
    public virtual bool Damage(float damage)
    {
        health -= damage;
        if (health > 0)
            return false;

        Die();
        return true;
    }

    // Destroys object on death
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}
