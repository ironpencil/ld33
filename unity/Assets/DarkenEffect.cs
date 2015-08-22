using UnityEngine;
using System.Collections;

public class DarkenEffect : GameEffect {

    public SpriteRenderer spriteToDarken;

    public float darkenByPercent = 0.8f;


    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        Color newColor = spriteToDarken.color;

        newColor.r *= darkenByPercent;
        newColor.g *= darkenByPercent;
        newColor.b *= darkenByPercent;

        spriteToDarken.color = newColor;
    }
}
