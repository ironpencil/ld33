using UnityEngine;
using System.Collections;

public class DestroyEffect : GameEffect {

    public float delay = 0.0f;

    public GameObject gameObjToDestroy;


    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        if (gameObjToDestroy == null)
        {
            gameObjToDestroy = gameObject;
        }

        Destroy(gameObjToDestroy, delay);
    }
}
