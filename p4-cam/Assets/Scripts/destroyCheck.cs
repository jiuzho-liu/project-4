using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class destroyCheck : MonoBehaviour
{
 
    public LensManager lensManager;
    // Start is called before the first frame update
    private void OnDestroy()
    {
      
        if (lensManager != null)
        {
            lensManager.GetLens = true;
        }        
    }
}
