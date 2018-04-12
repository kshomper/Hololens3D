﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListCompletedTaskShowHide : MonoBehaviour
{
    public bool showCompleted;
    public TaskListViewerController tlvc;

    // Use this for initialization
    void Start()
    {
        showCompleted = true;
        this.GetComponent<Renderer>().material.color = Color.green;
    }

    void OnSelect()
    {
        showCompleted = !showCompleted;
        tlvc.UpdateTasks();
        if (showCompleted)
        {
            this.GetComponent<Renderer>().material.color = Color.green;
        } else
        {
            this.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
