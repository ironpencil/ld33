﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public Image fillBar;

    public float imageMaxHeight = 100.0f;

    public bool invertSize = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealthValue(float percent)
    {
        int imageHeight = (int)(imageMaxHeight * percent);

        if (invertSize)
        {
            imageHeight = (int)imageMaxHeight - imageHeight;
        }

        fillBar.rectTransform.sizeDelta = new Vector2(fillBar.rectTransform.sizeDelta.x, imageHeight);


    }
}
