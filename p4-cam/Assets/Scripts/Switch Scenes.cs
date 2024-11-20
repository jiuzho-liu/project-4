using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{

    public GameObject transVolume;

    //public Collider triggerCollider;

    //public GameObject PlayerContro;
   
    public bool isInsideTrigger=false;
    private void Start()
    {
      

    }
    //void Update()
    //{

    //    if (triggerCollider != null && triggerCollider.isTrigger)
    //    {
    //        if (triggerCollider.bounds.Contains(PlayerContro.transform.position))
    //        {
    //            if (!isInsideTrigger)
    //            {
    //                isInsideTrigger = true;
    //                Debug.Log("角色进入触发器区域");
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
    //            Debug.Log("角色离开触发器区域");
    //        }
    //    }
    //}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&& isInsideTrigger)
        {
            Animator transAni = transVolume.GetComponent<Animator>();
            transAni.Play("transLevel Animation2");
            Invoke("LoadNextScene", 0.5f);

        }
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            Debug.Log("角色进入触发器区域");
            isInsideTrigger = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("角色离开触发器区域");
            isInsideTrigger= false;
        }
    }
    void LoadNextScene()
    {
        CancelInvoke("picfile");
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        
        int nextIndex = currentIndex + 1;

        
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            
            SceneManager.LoadScene(nextIndex);
            
        }
        else
        {
            Debug.LogError("没有下一个场景可以加载");
        }
        
    }
}