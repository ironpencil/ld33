using UnityEngine;
using System.Collections;

public class FacingHandler : MonoBehaviour {

    public BaseMovement movementController;
    public Animator animator;

    public const string ANIM_PARAM_FACING_X = "facingX";
    public const string ANIM_PARAM_FACING_Y = "facingY";
    public const string ANIM_PARAM_WALKING = "walking";

    public Vector2 facing = Vector2.zero;

    public bool simpleFacing = true;

    public bool majorityFacing = false;

    public bool doFacingInUpdate = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (doFacingInUpdate)
        {
            UpdateFacing();
        }

        
        

        
        
	
	}

    public void UpdateFacing()
    {
        float moveX = movementController.movementDirection.x;
        float moveY = movementController.movementDirection.y;
        bool walking = false;

        float absMoveX = Mathf.Abs(moveX);
        float absMoveY = Mathf.Abs(moveY);

        if (simpleFacing)
        {
            if (absMoveX > 0 || absMoveY > 0)
            {
                walking = true;

                facing.x = moveX;
                facing.y = moveY;
            }
        }
        else
        {
            if (absMoveX > 0 && absMoveY > 0)
            {
                //they're moving in both directions
                //so just use whatever our previous facing was

                walking = true;

                if (majorityFacing)
                {
                    //face in whichever direction we're moving MORE
                    if (absMoveY > absMoveX)
                    {
                        facing.y = moveY;
                        facing.x = 0.0f;
                    }
                    else
                    {
                        facing.x = moveX;
                        facing.y = 0.0f;
                    }

                }


                //TODO: make sure they haven't changed facing direction in one frame somehow
            }
            else
            {
                // arbitrarily favor horizontal facing
                if (absMoveX > 0)
                {
                    moveY = 0.0f;
                    facing.x = moveX;
                    facing.y = 0.0f;

                    walking = true;
                }

                if (absMoveY > 0)
                {
                    facing.x = 0.0f;
                    facing.y = moveY;

                    walking = true;
                }
            }
        }

        if (animator != null)
        {
            animator.SetFloat(ANIM_PARAM_FACING_X, facing.x);
            animator.SetFloat(ANIM_PARAM_FACING_Y, facing.y);
            animator.SetBool(ANIM_PARAM_WALKING, walking);
        }
    }
}
