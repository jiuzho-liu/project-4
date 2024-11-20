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
        // ��ʼ������ͼƬΪ���ɼ�  
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

                // ����ǩ�Ƿ���ӳ����  
                if (tagToImageMapping.ContainsKey(hitTag))
                {
                    // ��ȡ��Ӧ��ͼƬ������Ϊ�ɼ�  
                    Image imageToShow = tagToImageMapping[hitTag];
                    if (imageToShow != null)
                    {
                        imageToShow.gameObject.SetActive(true);
                        // ��ʼ����Э�̣������Ҫ�Ļ���  
                        StartCoroutine(FadeOutImage(imageToShow, fadeDuration));
                    }

                    // �ݻٱ����е�����  
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Destroyed object with tag: " + hitTag);
                }
            }
        }
    }

    // ����ͼƬ��Э��  
    private IEnumerator FadeOutImage(Image image, float duration)
    {
        // ����ͼƬ�ĳ�ʼ��ɫ  
        Color initialColor = image.color;
        // ����ͼƬ��ʼΪ��͸�������֮ǰ����Ϊ͸���Ļ���  
        image.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            // ����ʱ���ֵ����͸����  
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            // ����ͼƬ��ɫ��ֻ�ı�͸����  
            image.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            yield return null; // �ȴ���һ֡  
        }

        // ����������ͼƬ����ѡ��ȡ�����������  
        // image.gameObject.SetActive(false);  
        // ����㲻������ͼƬ����������Ҫ��ʱ���ٴ���ʾ��  
    }
}