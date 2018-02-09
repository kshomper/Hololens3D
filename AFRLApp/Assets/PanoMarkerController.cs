﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanoMarkerController : MonoBehaviour
{
    public int myIndex;
    public GameObject TakerController;
    public Vector3 starterScale;
    public bool focused;
    public int counter;
    public Text countdownText;
    // Use this for initialization
    void Awake()
    {
        starterScale = this.transform.localScale;
        focused = false;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (focused)
        {
            counter++;
            //Debugging println
            countdownText.text = counter.ToString();
        }
        else
        {
            counter = 0;
            //countdownText.text = "";
        }

        if (counter > 20)
        {
            counter = 0;

            TakerController.GetComponent<PanoTakerController>().TakeSinglePicture(myIndex);
            this.Hide();
        }
        focused = false;
    }

    internal void Show()
    {
        this.transform.localScale = starterScale;
    }

    internal void Hide()
    {
        this.transform.localScale = new Vector3(0, 0, 0);
    }
}