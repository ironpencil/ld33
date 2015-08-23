using UnityEngine;
using System.Collections;

public class MonsterWonEffect : GameEffect {	

    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        Globals.Instance.endGameHandler.MonsterWon();
    }
}
