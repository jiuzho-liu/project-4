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
    
    public void OnMouseDrag()
    {
        transform.position = Input.mousePosition;
    }

    public void OnMouseDown()
    {
        photoSystem.currentSeletedPhoto = this;
        gameObject.GetComponent<RectTransform>().SetAsLastSibling();
    }

}
