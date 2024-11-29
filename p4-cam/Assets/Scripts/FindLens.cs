using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FindLens : MonoBehaviour
{
  
    public Image FlashImage;
    public Image InfraredImage;
    public Image UV;
    public Image Acetylene;
    public Image LiquidChlorine;
    public Image L3Code1;
    public Image L3Code2;
    public Image L3Code3;
    public Image L3Code4;
    //public Image FlashImage;
    public float fadeDuration = 5f;

    
    public Dictionary<string, Image> tagToImageMapping = new Dictionary<string, Image>();
 

    private void Start()
    {
        
        tagToImageMapping.Add("DestroyOnClick1", FlashImage);
        tagToImageMapping.Add("DestroyOnClick2", InfraredImage);
        tagToImageMapping.Add("DestroyOnClick3", UV);
        tagToImageMapping.Add("DestroyOnClick4", Acetylene);
        tagToImageMapping.Add("DestroyOnClick5", LiquidChlorine);
        tagToImageMapping.Add("DestroyOnClick6", L3Code1);
        tagToImageMapping.Add("DestroyOnClick7", L3Code2);
        tagToImageMapping.Add("DestroyOnClick8", L3Code3);
        tagToImageMapping.Add("DestroyOnClick9", L3Code4);


        foreach (var image in tagToImageMapping.Values)
        {
            if (image != null)
            {
                image.gameObject.SetActive(false);
                
            }
        }
    }

    void Update()
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

                    imageToShow.gameObject.SetActive(false);
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Destroyed object with tag: " + hitTag);
                }
            }
        }
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

            yield return null; 
        }

        
    }
}