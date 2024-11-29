using UnityEngine;
using UnityEngine.UI;
using Whilefun.FPEKit;

public class PasswordChecker : MonoBehaviour
{
    
    public bool inCheckArea = false;
    public GameObject CheckUI;
    public InputField passwordInput;
    public Button submitButton;
    public Text messageText;
    public AudioSource correctSource;
    public AudioSource incorrectSource;
    FPEMouseLook fPEMouseLook;
    FPEFirstPersonController fPEFirstPersonController;
    [SerializeField, Tooltip("The door(s) the security system will lock or unlock")]
    private FPEDoor[] doorsToControl = null;
    
    public string correctPassword = "password"; 

    void Start()
    {
        fPEMouseLook = FindAnyObjectByType<FPEMouseLook>();
        fPEFirstPersonController = FindAnyObjectByType<FPEFirstPersonController>();
        
        submitButton.onClick.AddListener(CheckPassword);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inCheckArea)
           {
           
                CheckUI.SetActive(true);
                fPEMouseLook.enableMouseLook = false;
                Debug.Log("stop");
                fPEFirstPersonController.disableMovement();
                setCursorVisibility(true);

            }
        if(Input.GetKeyDown(KeyCode.Escape)&& inCheckArea)
           {
                CheckUI.SetActive(false);
                setCursorVisibility(false);
                fPEMouseLook.enableMouseLook = true;
                fPEFirstPersonController.enableMovement();


            }
    }

    
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            Debug.Log("CheckIn");
            inCheckArea = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("CheckOut");
            inCheckArea = false;
        }
    }
    void CheckPassword()
    {
        string enteredPassword = passwordInput.text;

        if (enteredPassword == correctPassword)
        {
            messageText.text = "correct";
            unlockDoors();
            correctSource.Play();
            CheckUI.SetActive(false);
            setCursorVisibility(false);
            fPEMouseLook.enableMouseLook = true;
            fPEFirstPersonController.enableMovement();
        }
        else
        {
            incorrectSource.Play();
            messageText.text = "incorrect";
        }

      
        passwordInput.text = "";
    }
    private void setCursorVisibility(bool visible)
    {
        Debug.Log("setCursorVisibility:" + visible);
        Cursor.visible = visible;

        if (visible)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
    private void unlockDoors()
    {

        foreach (FPEDoor d in doorsToControl)
        {

            if (d != null)
            {

                bool success = d.ExternallyUnlockDoor();

              

            }

        }

    }
}