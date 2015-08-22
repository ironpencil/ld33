using UnityEngine;
using System.Collections;

public class TankInput : MonoBehaviour {

    public BaseMovement movementController;
    public BaseMovement rotationController;
    public FacingHandler facingHandler;
    public WeaponHandler weaponHandler;

    public bool canMoveWhileAttacking = false;

    public string inputHorizontalAxis = "Horizontal";
    public string inputVerticalAxis = "Vertical";
    public string inputAttackButton = "Fire1";    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Globals.Instance.acceptPlayerGameInput)
        {
            HandleMovement();
            HandleAttack();
        }
	
	}

    private void HandleAttack()
    {
        if (Input.GetButton(inputAttackButton))
        {
            if (weaponHandler != null)
            {
                weaponHandler.Attack(new Vector2(1.0f, 0.0f));
            }
        }
    }

    void HandleMovement()
    {

        float horizontal = 0.0f;
        float vertical = 0.0f;

        if (canMoveWhileAttacking || weaponHandler == null || !weaponHandler.IsAttacking())
        {
            horizontal = Input.GetAxis(inputHorizontalAxis);
            vertical = Input.GetAxis(inputVerticalAxis);
        }

        bool doMove = false;
        bool doTurn = false;

        float horizontalMovement = 0.0f;
        float verticalMovement = 0.0f;

        if (horizontal > 0.0f)
        {
            horizontalMovement = 1.0f;
            doTurn = true;
        }
        else if (horizontal < 0.0f)
        {
            horizontalMovement = -1.0f;
            doTurn = true;
        }

        if (vertical > 0.0f)
        {
            verticalMovement = 1.0f;
            doMove = true;
        }
        else if (vertical < 0.0f)
        {
            verticalMovement = -1.0f;
            doMove = true;
        }

        movementController.movementDirection = new Vector2(0.0f, verticalMovement);
        rotationController.movementDirection = new Vector2(horizontalMovement, 0.0f);

        if (doMove)
        {
            movementController.Move();
        }

        if (doTurn)
        {
            rotationController.Move();
        }

        if (facingHandler != null)
        {
            facingHandler.UpdateFacing();
        }
    }
}
