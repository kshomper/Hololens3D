﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListViewerController : MonoBehaviour {
    public GameObject[] Tasks;
	// Use this for initialization
	void Start () {
        GameObject TasksHolder = GameObject.Find("Tasks");
        Tasks = new GameObject[TasksHolder.transform.childCount];
        int i = 0;
        foreach(Transform task in TasksHolder.transform)
        {
            Tasks[i] = task.gameObject;
            i++;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
