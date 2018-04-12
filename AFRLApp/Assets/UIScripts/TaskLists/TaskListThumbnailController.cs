﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskListThumbnailController : MonoBehaviour
{
    public TaskListViewerController tlvc;
    public TaskListGalleryController tlgc;
    public int ID;
    public Text ThumbText;

    // Use this for initialization
    void Start()
    {
        tlvc = GameObject.Find("TaskListViewer").GetComponent<TaskListViewerController>();
        tlgc = GameObject.Find("TaskListGallery").GetComponent<TaskListGalleryController>();
        ID = -1;
    }

    private void Update()
    {
        if (ID > -1)
        {
            ThumbText.text = tlgc.taskLists.Find(x => x.Id == ID).GetTitleWithNumCompleted();
        }
    }

    void OnSelect()
    {
        if (ID > -1)
        {
            tlgc.Hide();
            tlvc.DisplayTaskList(ID, 0);
            tlvc.Show();
            GameObject.Find("TaskListShowGalleryButton").GetComponent<TaskListShowGalleryController>().TaskViewerCurrentlyShown();
        }
    }

    internal void SetThumbnail(int id)
    {
        ID = id;
        ThumbText.text = tlgc.taskLists.Find(x => x.Id == ID).GetTitleWithNumCompleted();
    }
}
