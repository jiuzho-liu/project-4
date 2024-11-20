using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transVolume : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       
        animator = GetComponent<Animator>();

       
        if (animator != null)
        {
          
            animator.Play("transLevel Animation");
        }
        else
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }



void Update()
    {
        
    }
}
