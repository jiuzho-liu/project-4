using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class itemOnWorld : MonoBehaviour
{
    public Image itemImage;
    public Image itemImage2;
    public Item thisItem;
    public Inventory playerInventory;
    public float fadeDuration = 5f;


    public Dictionary<string, Image> tagToImageMapping = new Dictionary<string, Image>();


    private void Start()
    {
        tagToImageMapping.Add("DestroyOnClick4", itemImage);
        tagToImageMapping.Add("DestroyOnClick5", itemImage2);
        foreach (var image in tagToImageMapping.Values)
        {
            if (image != null)
            {
                image.gameObject.SetActive(false);

            }
        }

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {

                string hitTag = hit.collider.tag;

                // 检查标签是否在映射中  
                if (tagToImageMapping.ContainsKey(hitTag))
                {
                   
                    Image imageToShow = tagToImageMapping[hitTag];
                    if (imageToShow != null)
                    {
                       imageToShow.gameObject.SetActive(true);
                 
                         StartCoroutine(FadeOutImage(imageToShow, 5f));
                    }

                    AddNewItem();
                    Destroy(hit.collider.gameObject);
          
                }
            }
        }



    }
   

    public void AddNewItem()
    {
        if (!playerInventory.itemList.Contains(thisItem))
        {
            // playerInventory.itemList.Add(thisItem);
            // InventoryManager.CreateNewItem(thisItem);
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (playerInventory.itemList[i] == null)
                {
                    playerInventory.itemList[i] = thisItem;
                    break;
                }
            }
        }
        else
        {
            thisItem.itemHeld += 1;
        }

        InventoryManager.RefreshItem();
    }
    private IEnumerator FadeOutImage(Image image, float duration)
    {
        // 保存图片的初始颜色  
        Color initialColor = image.color;
        // 设置图片初始为不透明（如果之前设置为透明的话）  
        image.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            // 根据时间插值计算透明度  
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            // 设置图片颜色，只改变透明度  
            image.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            yield return null; // 等待下一帧  
        }

        // 淡出后隐藏图片（可选，取决于你的需求）  
        image.gameObject.SetActive(false);  
        // 如果你不想隐藏图片，可以在需要的时候再次显示它  
    }
}
