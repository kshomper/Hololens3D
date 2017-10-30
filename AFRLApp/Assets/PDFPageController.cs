﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDFPageController : MonoBehaviour {

    public int PageNum;
	
	public void OnSelect()
    {
        this.transform.root.GetComponentInChildren<PDFViewerController>().SetPageVisible(PageNum);
    }
}
