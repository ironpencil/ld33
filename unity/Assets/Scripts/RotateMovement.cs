using UnityEngine;
using System.Collections;

public class RotateMovement : BaseMovement {

    private Rigidbody2D rb;    

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

            rb.rotation -= (movementDirection.x * speed);
        }

        doMove = false;
    }

    public override void Move()
    {
        if (disabled) {
            return;
        }

        doMove = true;

    }
}
