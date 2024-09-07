using System;
using System.Collections;
using System.Collections.Generic;
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
}

public class LensManager : MonoBehaviour
{
    public enum LensType
    {
        None,
        Infrared,
        ND,
        ND2
    }


    public LensType currentType = LensType.None;

    [SerializeField]
    public Lens[] lens;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Lens len in lens)
        {
            len.image.color = Color.gray;
        }
    }

    // Update is called once per frame
    void Update()
    {
        LensKeyCheckSwitch();
    }


    void LensKeyCheckSwitch()
    {

        int index = -1;

        for (int i = 0; i < lens.Length; i++)
        {
            if (Input.GetKeyDown(lens[i].key) && !lens[i].isLocked)
            {
                Debug.Log("°´¼ü´¥·¢" + i);
                index = i;

                break;
            }
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
        }


        lens[index].volume.gameObject.SetActive(true);
        lens[index].image.color = Color.white;

        currentType = (LensType)index + 1;
    }
}
