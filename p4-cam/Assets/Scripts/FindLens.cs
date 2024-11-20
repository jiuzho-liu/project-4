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
        // 初始化所有图片为不可见  
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

                // 检查标签是否在映射中  
                if (tagToImageMapping.ContainsKey(hitTag))
                {
                    // 获取对应的图片并设置为可见  
                    Image imageToShow = tagToImageMapping[hitTag];
                    if (imageToShow != null)
                    {
                        imageToShow.gameObject.SetActive(true);
                        // 开始淡出协程（如果需要的话）  
                        StartCoroutine(FadeOutImage(imageToShow, fadeDuration));
                    }

                    // 摧毁被击中的物体  
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Destroyed object with tag: " + hitTag);
                }
            }
        }
    }

    // 淡出图片的协程  
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
        // image.gameObject.SetActive(false);  
        // 如果你不想隐藏图片，可以在需要的时候再次显示它  
    }
}