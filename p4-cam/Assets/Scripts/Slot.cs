using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Slot : MonoBehaviour
{
    public int slotID; //ø’∏ÒID=ŒÔÃÂID
    public Item slotItem;
    public Image slotImage;
    public TMP_Text slotNum;
    public string slotInfo;
    public GameObject itemInSlot;
    public void ItemOnClicked()
    {
        InventoryManger.UpdateItemInformation(slotInfo);
    }
    public void SetupSlot(Item item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        else
        {
            slotImage.sprite = item.itemImage;
            slotNum.text = item.itemHeld.ToString();
            slotInfo = item.itemInfo;
        }
    }
}
