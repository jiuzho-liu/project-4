using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using Whilefun.FPEKit;



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

    void Start()
    {
        screenRecorder = GetComponent<ScreenRecorder>();
        fPEMouseLook = FindAnyObjectByType<FPEMouseLook>();
        fPEFirstPersonController = FindAnyObjectByType<FPEFirstPersonController>();
    }

 
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
                fPEMouseLook.enableMouseLook = false;
                fPEFirstPersonController.disableMovement();

                setCursorVisibility(true);
                screenRecorder.isOn = false;
            }
            else
            {
                photoUI.SetActive(false);
                delArea.SetActive(false);
          
                setCursorVisibility(false);

                fPEMouseLook.enableMouseLook = true;
                fPEFirstPersonController.enableMovement();

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

        Debug.Log(files[files.Length-1]);

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
            tex.LoadImage(fileData); 
            return true;
        }
        else
        {
            return false;
        }
    }
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
