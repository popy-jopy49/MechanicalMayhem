using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
    public Transform target;
    public float speed = 3f;

    public Rigidbody2D rb;


  private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        // Get the target

       // Rotate towards the target
    }

    private void FixedUpdate(){
        // Move fowards
    }


    private void GetTarget () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


}
