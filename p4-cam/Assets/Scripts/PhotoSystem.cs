using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
public class PhotoSystem : MonoBehaviour
{

    public GameObject photoUI;
    public GameObject photoPrefab;
    public GameObject panel;

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
            }
            else
            {
                ShowPhotoUI();
                photoUI.SetActive(true);
                isPhotoUIShown = true;
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
}
