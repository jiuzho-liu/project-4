using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Controller : MonoBehaviour
{
    CharacterController player; 
    public new Transform camera; 
    public float speed = 2f;			 
    float x, y;                 
    float g = 10f;              
    Vector3 playerrun;          

    void Start()
    {
        player = GetComponent<CharacterController>();    
    }

    void Update()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;//Òþ²ØÊó±ê

       
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
        if (player.isGrounded)
        {
            playerrun = new Vector3(_horizontal, 0, _vertical);
        }
        playerrun.y -= g * Time.deltaTime;
        player.Move(player.transform.TransformDirection(playerrun * Time.deltaTime * speed));

       
        x += Input.GetAxis("Mouse X");
        y -= Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(0, x, 0);
        y = Mathf.Clamp(y, -45f, 45f);
        camera.eulerAngles = new Vector3(y, x, 0);

       
        if (camera.localEulerAngles.z != 0)
        {
            float rotX = camera.localEulerAngles.x;
            float rotY = camera.localEulerAngles.y;
            camera.localEulerAngles = new Vector3(rotX, rotY, 0);
        }
    }
}
