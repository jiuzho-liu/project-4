using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;



[Serializable]
public struct Lens
{
    public KeyCode key;
    public Volume volume;
    public bool isLocked;
    public Image image;
    public TMP_Text LensName;
    public GameObject[] Code;
}

public class LensManager : MonoBehaviour
{
    private int index = -1;
    //public bool GetLens=false;
    public bool GetLens = false;
    //public bool GetLens2 = false;
    //public bool GetLens3 = false;
    public bool PickFlash=false;
    //public GameObject myBag;
    //public bool isOpen = true;
    //public GameObject Lens1;
    public enum LensType
    {
        None,
        Infrared,
        ND,
        ND2,
        Flash
    }


    public LensType currentType = LensType.None;

    [SerializeField]
    public Lens[] lens;


    
    void Start()
    {
        foreach (Lens len in lens)
        {
           
            if (len.image != null)
            {
                len.image.enabled = false; 
            }

            if (len.LensName != null) { 
            
            len.LensName.enabled = false;
            
            
            
            }

            len.image.color = Color.gray;
            foreach (GameObject obj in len.Code)
            {
                if (obj != null) 
                {
                    obj.SetActive(false);  
                }
            }
        }

    }

   
    void Update()
    {   if (GetLens)
        {
            
            LensKeyCheckSwitch();
        }
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    isOpen = !isOpen;
        //    myBag.SetActive(isOpen);
        //}

    }


    void LensKeyCheckSwitch()
    {
       
        //int index = -1;
        //lens[1].isLocked = false;
        for (int i = 0; i < lens.Length; i++)
        {
            
            if (Input.GetKeyDown(lens[i].key) && !lens[i].isLocked)
            {
                Debug.Log("°´¼ü´¥·¢" + i);

                index = i;
               
                break;
            }
            
        }
        if (index == 0)
        {
            PickFlash = true;
        }
        else
        {
            PickFlash=false;
        }

        if (index < 0) return;
        SwitchLenTo(index);
    }

    void SwitchLenTo(int index)
    {

        if (currentType != LensType.None)
        {
            int currentIndex = (int)currentType - 1;
            lens[currentIndex].volume.gameObject.SetActive(false);
            lens[currentIndex].image.color = Color.gray;
            lens[currentIndex].LensName.color=Color.gray;
           
            foreach (GameObject obj in lens[currentIndex].Code)
            {
                obj.SetActive(false);
            }
        }


        lens[index].volume.gameObject.SetActive(true);
        lens[index].image.color = Color.white;
        lens[index].LensName.color = Color.white;
         foreach (GameObject obj in lens[index].Code)
        {
            obj.SetActive(true);
        }

        currentType = (LensType)index + 1;
    }
   

}
