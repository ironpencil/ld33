using UnityEngine;
using System.Collections;

public class TankWonEffect : GameEffect {	

    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        Globals.Instance.endGameHandler.TankWon();
    }
}
