using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatDamageEffect : GameEffect {

    public GameObject textPrefab;

    public bool destroyAfterDisplay = true;

    public float displayTime = 0.5f;

    public Vector2 originOffset = new Vector2(0.0f, 1.0f);

    public bool parentToDynamicObjects = true;



    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        GameObject floatingText = (GameObject)GameObject.Instantiate(textPrefab, transform.position, Quaternion.identity);

        if (parentToDynamicObjects)
        {
            try
            {
                floatingText.transform.parent = Globals.Instance.dynamicObjects;
            }
            catch { }             
        }
        else
        {
            floatingText.transform.parent = transform;
        }

        FloatingText textScript = floatingText.GetComponent<FloatingText>();

        if (textScript == null) { return; }

        Vector2 originPosition = transform.position;

        originPosition.x += originOffset.x;
        originPosition.y += originOffset.y;

        textScript.duration = displayTime;

        textScript.DisplayText(value.ToString(), originPosition);

        if (destroyAfterDisplay)
        {
            Destroy(textScript.gameObject, displayTime + 0.1f);
        }
    }
}
