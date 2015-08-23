using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TextBox : MonoBehaviour {

    [Multiline]
    public List<string> textList;

    public int nextScreenIndex = 0;

    public MessageBox messageBox;
    public Text textControl;
    public Scrollbar scrollbar;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        nextScreenIndex = 0;
        NextScreen();
    }

    public void NextScreen()
    {
        if (nextScreenIndex >= textList.Count)
        {
            messageBox.StartClose();
            return;
        }

        string nextText = textList[nextScreenIndex];

        textControl.text = nextText;
        scrollbar.value = 1.0f;

        nextScreenIndex++;
    }
}
