using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightFollower : MonoBehaviour
{
    
    
    // Reference to the camera to follow  
    public Camera cameraToFollow;

    // Offset for the spotlight position relative to the camera  
    public Vector3 positionOffset =new Vector3(0,1,0);
    private void Start()
    {
       
    }
    void Update()
    {
        GameObject flashlight = GameObject.FindWithTag("Flash");
        if (flashlight != null && cameraToFollow != null)
        {
            // Set the spotlight's position to be offset from the camera's position  
            flashlight.transform.position = cameraToFollow.transform.position + positionOffset;

            // Set the spotlight's rotation to match the camera's rotation  
            flashlight.transform.rotation = cameraToFollow.transform.rotation;
        }
        else
        {
            //Debug.LogWarning("Spotlight or CameraToFollow reference is missing!");
        }
    }
}
