using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PDFGalleryController : MonoBehaviour
{

    private int currViewedPDFIndex;
    private List<PDFDocument> documents;
    public Vector3 OrigScale;
    public Vector3 ResetScale;
    public bool GalleryIsVisible;
    public GameObject[] galleryPDFPanes { get; private set; }
    public Renderer[] galleryPDFRenderers { get; private set; }
    public int currentPageNum;
    // Use this for initialization
    void Start()
    {
        documents = GameObject.Find("Managers").GetComponent<DataManager>().documents;

        OrigScale = this.transform.localScale;
        GalleryIsVisible = true;

        // Set ImageId of all Gallery Image Panes and acquire their renderers
        // for the purpose of applying textures later

        int numGalleryPanes = this.transform.childCount;
        galleryPDFPanes = new GameObject[numGalleryPanes];
        galleryPDFRenderers = new Renderer[numGalleryPanes];
        for (int i = 0; i < galleryPDFPanes.Length; i++)
        {
            galleryPDFPanes[i] = this.transform.GetChild(i).gameObject;
            galleryPDFPanes[i].GetComponent<PDFGallerySwapper>().PDFId = i; //TODO: Add PDFGallerySwappers to each of the gallery Thumbnails - JR
            galleryPDFRenderers[i] = galleryPDFPanes[i].GetComponent<Renderer>();
            galleryPDFRenderers[i].material.SetTextureScale("_MainTex", new Vector2(-1, -1));
        }
        currViewedPDFIndex = 0;
        currentPageNum = 0;

        GameObject PDFViewer = this.transform.parent.gameObject;
        bool IsFirstInstance = PDFViewer.GetComponent<PDFReceiver>().FirstInstance;

        if (!IsFirstInstance && OrigScale == new Vector3(0, 0, 0))
        {
            OrigScale = ResetScale;
        }
        HideWindow();
    }


    /// <summary>
    /// Displays the first pdf received on the 
    /// main image pane
    /// </summary>

    public void OnFirstPDF()
    {
        OnSelectByIndex(0);
    }

    /// <summary>
    /// Display the gallery image received immediately after the current one
    /// </summary>

    public void OnNextPDF()
    {
        //TODO: make this work in light of multiple pages
        if (currViewedPDFIndex < documents.Count)
        {
            OnSelectByIndex(currViewedPDFIndex + 1);
        }
    }

    /// <summary>
    /// Display the gallery image received immediately before the current one
    /// </summary>

    public void OnPreviousPDF()
    {
        //TODO: Make this work in light of multiple pages
        if (currViewedPDFIndex > 0)
        {
            OnSelectByIndex(currViewedPDFIndex + 1);
        }
    }

    /// <summary>
    /// Sets the index of the currently displayed gallery image
    /// </summary>
    /// <param name="newIndex"></param>

    public void UpdateCurrGalleryIndex(int newIndex)
    {
        currViewedPDFIndex = newIndex;
        //TODO: Go to the right page in the gallery for this if you're not already there
    }

    /// <summary>
    /// Selects a gallery image pane to display based on its index
    /// </summary>
    /// <param name="GalleryPDFIndex"></param>

    public void OnSelectByIndex(int GalleryPDFIndex)
    {
        //TODO: Make this work considering we are on a different page
        GameObject galleryPDFPaneObj = galleryPDFPanes[GalleryPDFIndex];
        galleryPDFPaneObj.GetComponent<PDFGallerySwapper>().OnSelect();
    }

    /// <summary>
    /// Shifts in a newly received image into the gallery, shifting all current
    /// gallery images appropriately
    /// </summary>
    /// <param name="PDF"></param>
    /// <param name="numRcvdPDFs"></param>

    public void RcvNewPDF(PDFDocument PDF, int numRcvdPDFs)
    {
        int numDocs = documents.Count;
        int pageItShouldBeOn = numDocs / 15;
        int thumbnailNum = (numDocs % 15) - 1;
        if (currentPageNum == pageItShouldBeOn)
        {
            Renderer currObjRenderer = galleryPDFRenderers[thumbnailNum];
            byte[] page = PDF.pages[0];
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(page);
            currObjRenderer.material.mainTexture = tex;
        }
    }

    /// <summary>
    /// Hides the gallery window
    /// </summary>

    public void HideWindow()
    {
        this.enabled = false;
        this.GalleryIsVisible = false;
    }

    /// <summary>
    /// Makes the gallery window visible
    /// </summary>

    public void ShowWindow()
    {
        this.enabled = true;
        this.GalleryIsVisible = true;
    }
}