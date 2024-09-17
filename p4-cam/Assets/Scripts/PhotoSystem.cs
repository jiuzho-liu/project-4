using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class PhotoSystem : MonoBehaviour
{


    public GameObject photoPrefab;
    public GameObject panel;

    bool isLastFile = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            ShowPhotoUI();
        }
    }


    public void ShowPhotoUI()
    {

        Debug.Log("ShowPhotoUI");
        
        int index = 0;
        while (!isLastFile)
        {
            Texture2D tex2D;
            if (LoadPNG(@"photos\screen_1920x1080_" + index.ToString() + ".png", out tex2D))
            {
                Image img = Instantiate(photoPrefab, panel.transform).GetComponent<Image>();
                img.sprite = Sprite.Create(tex2D, new Rect(0.0f, 0.0f, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            }else
            {
                Debug.Log("结束读取，共有" + index + "个文件");

                isLastFile = true; }
            index++;
        }
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
