using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using Whilefun.FPEKit;
using Unity.VisualScripting.Antlr3.Runtime;


public class PhotoSystem : MonoBehaviour
{
    [Header("UI")]
    public GameObject photoUI;
    public GameObject photoPrefab;
    public GameObject panel;
    public GameObject delArea;



    [Space(10f)]
    public PhotoObj currentSeletedPhoto;


    private bool mouseLookEnabled = true;
    private bool ShowPhoto = false;
    public GameObject myBag;
    public bool isOpen = true;
    bool isLastFile = false;
    ScreenRecorder screenRecorder;
    FPEMouseLook fPEMouseLook;
    FPEFirstPersonController fPEFirstPersonController;  
    // Start is called before the first frame update
    void Start()
    {
        screenRecorder = GetComponent<ScreenRecorder>();
        fPEMouseLook = FindAnyObjectByType<FPEMouseLook>();
        fPEFirstPersonController = FindAnyObjectByType<FPEFirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
           
            isOpen = !isOpen;

            
            if (isOpen)
            {
                
                myBag.SetActive(true);
                fPEMouseLook.enableMouseLook = false;
                fPEFirstPersonController.disableMovement();

                setCursorVisibility(true);

            }
            else
            {
               
                myBag.SetActive(false);
                setCursorVisibility(false);

                fPEMouseLook.enableMouseLook = true;
                fPEFirstPersonController.enableMovement();

            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShowPhoto = !ShowPhoto;
            if (ShowPhoto)
            {
                ShowPhotoUI();
                delArea.SetActive(true);   
                photoUI.SetActive(true);
                //FPEMenu.Instance.activateMenu();
                //Time.timeScale = 0.0f;

                fPEMouseLook.enableMouseLook = false;
                fPEFirstPersonController.disableMovement();

                setCursorVisibility(true);
                screenRecorder.isOn = false;
            }
            else
            {
                photoUI.SetActive(false);
                delArea.SetActive(false);
                //FPEMenu.Instance.deactivateMenu();
                setCursorVisibility(false);

                fPEMouseLook.enableMouseLook = true;
                fPEFirstPersonController.enableMovement();

                //Time.timeScale = 1.0f;

                //Clear up
                foreach (Transform obj in panel.transform)
                {
                    Destroy(obj.gameObject);
                }
                StartGrid();


                screenRecorder.isOn = true;
            }
        }
    }


    public void ShowPhotoUI()
    {
        Debug.Log("ShowPhotoUI");
     
        string mask = string.Format("screen_{0}x{1}*.{2}", screenRecorder.captureWidth, screenRecorder.captureHeight, screenRecorder.format.ToString().ToLower());
        string[] files = Directory.GetFiles(screenRecorder.folder, mask, SearchOption.TopDirectoryOnly);

        int counter = files.Length; 
        //Debug.Log("ÕÕÆ¬Êý:" + counter);
        Debug.Log(files[files.Length-1]);


        //for (int i = 0; i < 25; i++)
        //{
        //    Texture2D tex2D;
        //    if (LoadPNG(@"photos\screen_1920x1080_" + i.ToString() + ".png", out tex2D))
        //    {
        //        PhotoObj photoObj = Instantiate(photoPrefab, panel.transform).GetComponent<PhotoObj>();
        //        photoObj.img.sprite = Sprite.Create(tex2D, new Rect(0.0f, 0.0f, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f), 100.0f);
        //        photoObj.path = @"photos\screen_1920x1080_" + i.ToString() + ".png";
        //        photoObj.index = i;
        //        photoObj.photoSystem = this;
        //    }
        //}
        //Í£µôFPS¿ØÖÆÆ÷

        Texture2D tex2D;
        int index = 0;
        foreach (string file in files)
        {
        
            if (LoadPNG(file, out tex2D))
            {
                PhotoObj photoObj = Instantiate(photoPrefab, panel.transform).GetComponent<PhotoObj>();
                photoObj.img.sprite = Sprite.Create(tex2D, new Rect(0.0f, 0.0f, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f), 100.0f);
                photoObj.path = file;
                photoObj.index = index;
                photoObj.photoSystem = this;

                index++;
            }

        }
        

        Invoke("StopGrid", 0.1f);
    }

    void StopGrid()
    {
        panel.GetComponent<GridLayoutGroup>().enabled = false;
    }

    void StartGrid()
    {
        panel.GetComponent<GridLayoutGroup>().enabled = true;
    }

    public void DeletPhoto(PhotoObj obj)
    {
        File.Delete(obj.path);
        Destroy(obj.gameObject);
    }


    public void DeletPhoto()
    {
        File.Delete(currentSeletedPhoto.path);
        Destroy(currentSeletedPhoto.gameObject);
    }



    public static bool LoadPNG(string filePath, out Texture2D tex)
    {

        tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            return true;
        }
        else
        {
            return false;
        }

    }
    




    //private void disableMouseLook()
    //{
    //    thePlayer.GetComponent<FPEMouseLook>().enableMouseLook = false;
    //    mouseLookEnabled = false;
    //}
    //// Unlocks mouse look so we can move mouse to look when walking/moving normally.
    //// If using another Character Controller (UFPS, etc.) substitute mouselook enable functionality
    //private void enableMouseLook()
    //{
    //    thePlayer.GetComponent<FPEMouseLook>().enableMouseLook = true;
    //    mouseLookEnabled = true;
    //}
    //// Locks movement of Character Controller. 
    //// If using another Character Controller (UFPS, etc.) substitute disable functionality
    //private void disableMovement()
    //{
    //    thePlayer.GetComponent<FPEFirstPersonController>().disableMovement();
    //}
    //// Unlocks movement of Character Controller. 
    //// If using another Character Controller (UFPS, etc.) substitute enable functionality
    //private void enableMovement()
    //{
    //    thePlayer.GetComponent<FPEFirstPersonController>().enableMovement();
    //}

    public bool isMouseLookEnabled()
    {
        return mouseLookEnabled;
    }
    private void setCursorVisibility(bool visible)
    {
        Debug.Log("setCursorVisibility:" + visible);
        Cursor.visible = visible;

        if (visible)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
}
