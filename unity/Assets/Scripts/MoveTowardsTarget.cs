using UnityEngine;
using System.Collections;

public class MoveTowardsTarget : BaseMovement {

    public Transform target;

    public LookAtTarget rotateToTarget;

    private Rigidbody2D rb;    

    public Vector3 stopDistance = new Vector3(0.5f, 0.5f, 0.0f);

	// Use this for initialization
	void Start () {
        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
	
	}
	
	// Update is called once per frame
	void Update () {
        if (disabled) { return; }

        if (moveDuringUpdate)
        {
            Move();
        }
	}

    void FixedUpdate()
    {
        if (disabled) { return; }

        Vector2 velocity = movementDirection * speed;
        if (rb.velocity != velocity)
        {
            rb.velocity = velocity;
        }
    }

    public override void Move()
    {
        if (disabled || target == null) {
            return;
        }

        Vector2 distanceToTarget = target.position - transform.position;

        bool targetReached = false;

        if (Mathf.Abs(distanceToTarget.x) < stopDistance.x && Mathf.Abs(distanceToTarget.y) < stopDistance.y)
        {
            targetReached = true;
        }

        if (targetReached)
        {
            movementDirection = Vector2.zero;
        }
        else
        {
            if (rotateToTarget == null)
            {
                movementDirection = distanceToTarget.normalized;
            }
            else
            {
                rotateToTarget.targetPos = target.position;
                rotateToTarget.RotateWithPhysics();
                movementDirection = transform.forward;
            }
        }
    }
}
