using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using Whilefun.FPEKit;
public class PhotoSystem : MonoBehaviour
{

    public GameObject photoUI;
    public GameObject photoPrefab;
    public GameObject panel;
    private GameObject thePlayer = null;
    private bool mouseLookEnabled = true;
    bool isLastFile = false;
    ScreenRecorder screenRecorder;
    private bool isPhotoUIShown = false;

    public PhotoObj currentSeletedPhoto;   
    // Start is called before the first frame update
    void Start()
    {
        screenRecorder = GetComponent<ScreenRecorder>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
           
            if (isPhotoUIShown)
            {
                photoUI.SetActive(false);
                isPhotoUIShown = false;

                FPEMenu.Instance.deactivateMenu();
                setCursorVisibility(false);
                enableMouseLook();
                enableMovement();
                Time.timeScale = 1.0f;

            }
            else
            {
                ShowPhotoUI();
                photoUI.SetActive(true);
                isPhotoUIShown = true;

                FPEMenu.Instance.activateMenu();
                Time.timeScale = 0.0f;
                disableMovement();
                disableMouseLook();
                setCursorVisibility(true);
            }
          
        }
    }


    public void ShowPhotoUI()
    {
        Debug.Log("ShowPhotoUI");

        string mask = string.Format("screen_{0}x{1}*.{2}", screenRecorder.captureWidth, screenRecorder.captureHeight, screenRecorder.format.ToString().ToLower());
        int counter = Directory.GetFiles(screenRecorder.folder, mask, SearchOption.TopDirectoryOnly).Length;

        for (int i = 0; i < counter; i++)
        {

            Texture2D tex2D;
            if (LoadPNG(@"photos\screen_1920x1080_" + i.ToString() + ".png", out tex2D))
            {
                PhotoObj photoObj = Instantiate(photoPrefab, panel.transform).GetComponent<PhotoObj>();
                photoObj.img.sprite = Sprite.Create(tex2D, new Rect(0.0f, 0.0f, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f), 100.0f);
                photoObj.path = @"photos\screen_1920x1080_" + i.ToString() + ".png";
                photoObj.index = i;
                photoObj.photoSystem = this;
            }
        }

        //Í£µôFPS¿ØÖÆÆ÷


        Invoke("StopGrid", 0.1f);
    }

    void StopGrid()
    {
        panel.GetComponent<GridLayoutGroup>().enabled = false;
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


    private void disableMouseLook()
    {
        thePlayer.GetComponent<FPEMouseLook>().enableMouseLook = false;
        mouseLookEnabled = false;
    }
    // Unlocks mouse look so we can move mouse to look when walking/moving normally.
    // If using another Character Controller (UFPS, etc.) substitute mouselook enable functionality
    private void enableMouseLook()
    {
        thePlayer.GetComponent<FPEMouseLook>().enableMouseLook = true;
        mouseLookEnabled = true;
    }
    // Locks movement of Character Controller. 
    // If using another Character Controller (UFPS, etc.) substitute disable functionality
    private void disableMovement()
    {
        thePlayer.GetComponent<FPEFirstPersonController>().disableMovement();
    }
    // Unlocks movement of Character Controller. 
    // If using another Character Controller (UFPS, etc.) substitute enable functionality
    private void enableMovement()
    {
        thePlayer.GetComponent<FPEFirstPersonController>().enableMovement();
    }

    public bool isMouseLookEnabled()
    {
        return mouseLookEnabled;
    }
    private void setCursorVisibility(bool visible)
    {

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
