﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjects : MonoBehaviour {

	public float minGroundNormalY = 0.65f;
	public float gravityModifier = 1f;

	protected bool grounded;
	protected Vector2 targetVelocity;
	protected Vector2 groundNormal;
	protected Vector2 velocity;
	protected Rigidbody2D rb2d;
	protected ContactFilter2D contactFilter;
	protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);

	protected const float minMoveDistance = 0.001f;
	protected const float shellRadius = 0.01f;
	void OnEnable()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () 
	{
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
	}
	
	// Update is called once per frame
	void Update () 
	{
		targetVelocity = Vector2.zero;
		ComputeVelocity ();
	}

	protected virtual void ComputeVelocity()
	{

	}
	void FixedUpdate()
	{
		velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
		velocity.x = targetVelocity.x;

		grounded = false;

		Vector2 deltaPosition = velocity * Time.deltaTime;

		Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);

		Vector2 move = moveAlongGround * deltaPosition.x;

		Movement (move, false);

		move = Vector2.up * deltaPosition.y;

		Movement (move, true);
	}

	void Movement (Vector2 move, bool yMovement)
	{
		float distance = move.magnitude;

		if (distance > minMoveDistance) 
		{
			int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
			hitBufferList.Clear ();

			for (int i = 0; i < count; ++i) 
			{
				hitBufferList.Add (hitBuffer [i]);
			}

			for (int i = 0; i < hitBufferList.Count; i++) 
			{
				Vector2 currentNormal = hitBufferList [i].normal;

				if (currentNormal.y > minGroundNormalY) 
				{
					grounded = true;
					if (yMovement) 
					{
						groundNormal = currentNormal;
						currentNormal.x = 0;
					}
				}

				float projection = Vector2.Dot (velocity, currentNormal);
				if (projection < 0) 
				{
					velocity = velocity - projection * currentNormal;
				}

				float modifiedDistance = hitBufferList [i].distance - shellRadius;
				distance = modifiedDistance < distance ? modifiedDistance : distance;
			}
		}

		rb2d.position = rb2d.position + move.normalized * distance;
	}
}
