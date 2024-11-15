using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemOnWorld : MonoBehaviour
{
    public Image Drug;
    public Item thisItem;
    public Inventory playerInventory;
    public Dictionary<string, Image> tagToImageMapping = new Dictionary<string, Image>();
    public float fadeDuration = 5f;


    private void Start()
    {
        tagToImageMapping.Add("DestroyOnClick4", Drug);

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

              
                if (tagToImageMapping.ContainsKey(hitTag))
                {
                  
                    Image imageToShow = tagToImageMapping[hitTag];
                    if (imageToShow != null)
                    {
                        imageToShow.gameObject.SetActive(true);
                    
                        StartCoroutine(FadeOutImage(imageToShow, fadeDuration));
                    }
                   
                   
                    Destroy(hit.collider.gameObject);
                    AddNewItem();
                }
            }
        }
    }
   


    public void AddNewItem()
    {
        
        if (!playerInventory.itemList.Contains(thisItem))
        {
            
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
        InventoryManger.RefreshItem();
    }
    private IEnumerator FadeOutImage(Image image, float duration)
    {
      
        Color initialColor = image.color;
        
        image.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);

        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
        
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
           
            image.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            Debug.Log("111");
            yield return null; 
        }
        

    }
}
