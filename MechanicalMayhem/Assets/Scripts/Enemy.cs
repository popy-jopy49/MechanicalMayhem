using UnityEngine;
using SWAssets.Utils;

public class Enemy : Attackable {
    
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected float rotateSpeed = 1f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float fireRate = 1f;
    protected float time;
    protected Transform target;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
    }

    protected virtual void Update()
    {
        if (target)
            RotateTowardsTarget();
    }

    protected virtual void Attack()
    {
        if (!CheckForFireRate())
            return;

        target.GetComponent<Player>().Damage(damage);
    }
    
    protected bool CheckForFireRate()
    {
        if (time < 1 / fireRate)
        {
            time += Time.deltaTime;
            return false;
        }
        else
        {
            time = 0;
            return true;
        }
    }

    protected virtual void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    protected void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = VectorUtils.GetAngleFromVector(targetDirection);
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

}
