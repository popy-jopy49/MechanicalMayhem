using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Attackable {
    
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;
    public GameObject bulletPrefab;

    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;

    public float fireRate;
    private float timeToFire;
    
    public Transform firingPoint;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        firingPoint = transform.Find("FirePoint");
    }

    private void Update(){
        if (!target){
            GetTarget();
        } else {
             RotateTowardsTarget();
        }
        
        if (Vector2.Distance(target.position, transform.position) <= distanceToStop) {
            Shoot();
        }
    }

    private void Shoot() {
        if (timeToFire <= 0f) {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            timeToFire = fireRate;
         } else {
            timeToFire -= Time.deltaTime;
       
         }
    }

    private void FixedUpdate() {
        if (target != null){
            if (Vector2.Distance(target.position, transform.position) >= distanceToStop) {
                rb.velocity = transform.up * speed;
            } else {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void RotateTowardsTarget() {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg -
            90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0,angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q,
        rotateSpeed);
    }
    private void GetTarget () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(other.gameObject);
            target = null;
        } else if (other.gameObject.CompareTag("Player")) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
