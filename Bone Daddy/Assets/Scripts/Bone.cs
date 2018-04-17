﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bone : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody2D myRigidBody;

    private Vector2 direction;

	// Use this for initialization
	void Start () {

        myRigidBody = GetComponent<Rigidbody2D>();

	}


    private void FixedUpdate()
    {
        myRigidBody.velocity = direction * speed;
    }

    public void Initialize (Vector2 direction)//player accesses this when throwing bone, based on player's facing direction and public means it can be accessed anywhere
    {
        this.direction = direction;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
