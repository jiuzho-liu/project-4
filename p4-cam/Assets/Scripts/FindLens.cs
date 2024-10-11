using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FindLens : MonoBehaviour
{
    public string objectToDestroyTag = "DestroyOnClick";
    public Image lensImage;
    public float fadeDuration = 3f;



    private void Start()
    {

        lensImage.gameObject.SetActive(false);

    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(objectToDestroyTag))
                {
                    lensImage.gameObject.SetActive(true);
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Destroyed: " + hit.collider.gameObject.name);
                    StartCoroutine(FadeOutImage(lensImage, fadeDuration));
                }
            }
        }
    }
    private System.Collections.IEnumerator FadeOutImage(Image image, float duration)
    {
        float elapsed = 0f;
        Color color = image.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / duration);  
            image.color = color;

            yield return null; 
        }
    }
}
