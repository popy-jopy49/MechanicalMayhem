using UnityEngine;
using SWAssets.Utils;

public class Enemy : Attackable {
    
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected float rotateSpeed = 0.0025f;
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

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            target = null;
            return;
        }
    }
}
