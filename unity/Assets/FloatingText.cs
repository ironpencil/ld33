using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingText : MonoBehaviour {

    public Text textControl;    

    public float speed = 4.0f;

    public Vector2 floatDirection = new Vector2(0.0f, 1.0f);

    public float duration = 0.5f;

    private IEnumerator currentFloatRoutine;

    public bool destroyAfterDisplay = true;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayText(string textToDisplay, Vector2 startingPosition)
    {
        if (currentFloatRoutine != null)
        {
            StopCoroutine(currentFloatRoutine);
        }

        textControl.text = textToDisplay;        

        transform.position = startingPosition;

        currentFloatRoutine = DoDisplayText();
        StartCoroutine(currentFloatRoutine);
    }

    private IEnumerator DoDisplayText()
    {

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            yield return null;

            elapsedTime += Time.deltaTime;

            Vector2 newPosition = transform.position;

            float moveSpeed = speed * Time.deltaTime;

            newPosition.x += floatDirection.x * moveSpeed;
            newPosition.y += floatDirection.y * moveSpeed;

            transform.position = newPosition;
        }

        textControl.text = "";

        if (destroyAfterDisplay)
        {
            Destroy(gameObject);
        }
    }
}
