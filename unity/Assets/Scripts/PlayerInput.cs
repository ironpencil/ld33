using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public BaseMovement movementController;
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
        if (Input.GetButtonDown(inputAttackButton))
        {
            if (weaponHandler != null)
            {
                weaponHandler.Attack(facingHandler.facing);
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

        float horizontalMovement = 0.0f;
        float verticalMovement = 0.0f;

        if (horizontal > 0.0f)
        {
            horizontalMovement = 1.0f;
        }
        else if (horizontal < 0.0f)
        {
            horizontalMovement = -1.0f;
        }

        if (vertical > 0.0f)
        {
            verticalMovement = 1.0f;
        }
        else if (vertical < 0.0f)
        {
            verticalMovement = -1.0f;
        }

        movementController.movementDirection = new Vector2(horizontalMovement, verticalMovement);

        if (facingHandler != null)
        {
            facingHandler.UpdateFacing();
        }
    }
}
