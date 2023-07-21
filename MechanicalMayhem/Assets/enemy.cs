using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    public Rigidbody2D rb;


  private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        if (!target){
            GetTarget();
        } else {
             RotateTowardsTarget();
        }
        
    }

    private void FixedUpdate(){
        rb.velocity = transform.up * speed;
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
        }
    }
}
