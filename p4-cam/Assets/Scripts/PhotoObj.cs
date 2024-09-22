using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoObj : MonoBehaviour
{
    public int index;
    public Image img;

    [HideInInspector]public string path;

    public PhotoSystem photoSystem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDrag()
    {
        transform.position = Input.mousePosition;
    }

    private void OnMouseDown()
    {
        photoSystem.currentSeletedPhoto = this;
    }

}
