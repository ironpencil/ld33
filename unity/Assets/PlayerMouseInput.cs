using UnityEngine;
using System.Collections;

public class PlayerMouseInput : MonoBehaviour
{

    public BaseMovement movementController;
    public FacingHandler facingHandler;
    public WeaponHandler weaponHandler;

    public bool canMoveWhileAttacking = false;

    public string inputMoveButton = "Fire1";
    public string inputAttackButton = "Fire2";

    public Transform movementTarget;
    public Vector3 targetPosition;

    public bool targetSet = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

        if (Input.GetButton(inputMoveButton))
        {

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            targetPosition = mousePosition;

            targetSet = true;             
        }

        movementTarget.position = targetPosition;        

        if (targetSet)
        {
            if (movementController is TurnAndMoveTowardsTarget)
            {
                ((TurnAndMoveTowardsTarget)movementController).target = movementTarget;
            }
        }

        if (canMoveWhileAttacking || weaponHandler == null || !weaponHandler.IsAttacking())
        {
            movementController.Move();
        }

        targetSet = false;
    }
}

