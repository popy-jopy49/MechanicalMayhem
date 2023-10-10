using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MeleeEnemy : Enemy
{

    protected virtual void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject == target.gameObject)
        {
            Attack();
            return;
        }
    }

}
