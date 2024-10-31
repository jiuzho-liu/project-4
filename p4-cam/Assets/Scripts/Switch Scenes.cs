using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
 
  
    //public Collider triggerCollider;

    //public GameObject PlayerContro;

    public bool isInsideTrigger=false;

    //void Update()
    //{

    //    if (triggerCollider != null && triggerCollider.isTrigger)
    //    {
    //        if (triggerCollider.bounds.Contains(PlayerContro.transform.position))
    //        {
    //            if (!isInsideTrigger)
    //            {
    //                isInsideTrigger = true;
    //                Debug.Log("��ɫ���봥��������");
    //            }


    //            if (Input.GetKeyDown(KeyCode.E))
    //            {
    //                //if (destroyPlayer != null)
    //                //{

    //                //    Destroy(destroyPlayer);
    //                //}
    //                LoadNextScene();
    //            }
    //        }
    //        else
    //        {
    //            isInsideTrigger = false;
    //            Debug.Log("��ɫ�뿪����������");
    //        }
    //    }
    //}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&& isInsideTrigger)
        {

            LoadNextScene();
        }
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            Debug.Log("��ɫ���봥��������");
            isInsideTrigger = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("��ɫ�뿪����������");
            isInsideTrigger= false;
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