using UnityEngine;
using System.Collections;

public class DestroyEffect : GameEffect {

    public float delay = 0.0f;



    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        Destroy(gameObject, delay);
    }
}
