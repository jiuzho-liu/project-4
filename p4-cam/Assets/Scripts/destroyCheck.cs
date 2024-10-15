using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum LensType
{
    Lens1,
    Lens2,
    Lens3,
    Flash
}

public class destroyCheck : MonoBehaviour
{   public ScreenRecorder screenRecorder;
    public LensManager lensManager;
    public LensType lensType; 

    private void OnDestroy()
    {
        if (lensManager != null)
        {
            switch (lensType)
            {
                case LensType.Lens1:
                    lensManager.lens[0].isLocked = false;
                    lensManager.lens[0].image.enabled = true;
                    lensManager.lens[0].LensName.enabled = true;
                    lensManager.GetLens = true;
                    //lensManager.GetLens1 = true;
                    break;
                case LensType.Lens2:
                    lensManager.lens[1].isLocked = false;
                    lensManager.lens[1].image.enabled = true;
                    lensManager.lens[1].LensName.enabled = true;
                    //lensManager.GetLens2 = true;
                    break;
                case LensType.Lens3:
                    lensManager.lens[2].isLocked = false;
                    lensManager.lens[2].image.enabled = true;
                    lensManager.lens[2].LensName.enabled = true;
                    //lensManager.GetLens3 = true;
                    break;
                case LensType.Flash:
                    screenRecorder.GetFlash = true;
                    lensManager.lens[3].isLocked = false;
                    lensManager.lens[3].image.enabled = true;
                    lensManager.lens[3].LensName.enabled = true;
                    break;
            }
        }
    }
}