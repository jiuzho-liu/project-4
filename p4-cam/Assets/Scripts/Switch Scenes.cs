using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
 
  
    public Collider triggerCollider;

    public GameObject destroyPlayer;

    private bool isInsideTrigger = false;

    void Update()
    {
          
        if (triggerCollider != null && triggerCollider.isTrigger)
        {
            if (triggerCollider.bounds.Contains(transform.position))
            {
                if (!isInsideTrigger)
                {
                    isInsideTrigger = true;
                    Debug.Log("��ɫ���봥��������");
                }

               
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (destroyPlayer != null)
                    {

                        Destroy(destroyPlayer);
                    }
                    LoadNextScene();
                }
            }
            else
            {
                isInsideTrigger = false;
                Debug.Log("��ɫ�뿪����������");
            }
        }
    }

    void LoadNextScene()
    {
      
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        
        int nextIndex = currentIndex + 1;

        
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.LogError("û����һ���������Լ���");
        }
    }
}