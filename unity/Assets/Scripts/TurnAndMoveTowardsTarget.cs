using UnityEngine;
using System.Collections;

public class TurnAndMoveTowardsTarget : BaseMovement {

    public Transform target;

    public LookAtTarget rotateToTarget;

    private Rigidbody2D rb;    

    public Vector3 stopDistance = new Vector3(0.5f, 0.5f, 0.0f);

    public bool doMove = false;

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

        if (doMove)
        {
            if (rotateToTarget == null)
            {
                movementDirection = (target.position - transform.position).normalized;
            }
            else
            {
                rotateToTarget.targetPos = target.position;
                rotateToTarget.RotateWithPhysics();
                movementDirection = transform.right;
            }

            Vector2 velocity = movementDirection * speed;
            if (rb.velocity != velocity)
            {
                rb.velocity = velocity;
            }
        }

        doMove = false;
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
            doMove = false;
            target = null;
        }
        else
        {
            doMove = true;
        }
    }
}
