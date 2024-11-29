using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Whilefun.FPEKit;
using UnityEngine.Playables;



public class ScreenRecorder : MonoBehaviour
{  
    public int captureWidth = 1920;
    public int captureHeight = 1080;
    public bool GetFlash=false;
    public bool Getlens1=false;
    public GameObject hideGameObject;
    public bool optimizeForManyScreenshots = true;
    public Text full;
    public Image cameraUI;
    private bool isCameraUIActive = false;
    public enum Format { RAW, JPG, PNG, PPM };
    public Format format = Format.PPM;
    public LensManager lensManager;
    public string folder;
    private Rect rect;
    private RenderTexture renderTexture;
    private Texture2D screenShot;
    private int counter = 0; 
    private bool captureScreenshot = false;
    private bool captureVideo = false;
    public GameObject GetCamera;
    public AudioSource shootPhoto;
    public GameObject L1Code;
    public GameObject L2Code;
    public GameObject L4Code;      
 
    public bool isUsingCamera = false;
    public GameObject Flash;
    public GameObject photoPreviewPrefab;
    public GameObject photoPreviewPanel;
    Image photoPreview;
    public event Action<string> OnScreenshotLimitReached;


    private void Start()
    {
        L4Code.SetActive(false);
        L2Code.SetActive(false);
        L1Code.SetActive(false);
        isUsingCamera = false;
    }
    private string uniqueFilename(int width, int height)
    {
       
        if (folder == null || folder.Length == 0)
        {
            folder = Application.dataPath;
            if (Application.isEditor)
            {
               
                var stringPath = folder + "/..";
                folder = Path.GetFullPath(stringPath);
            }
            folder += "/screenshots";

           
            System.IO.Directory.CreateDirectory(folder);

          
            string mask = string.Format("screen_{0}x{1}*.{2}", width, height, format.ToString().ToLower());
            counter = Directory.GetFiles(folder, mask, SearchOption.TopDirectoryOnly).Length;
        }
       
        var filename = string.Format("{0}/screen_{1}x{2}_{3}.{4}", folder, width, height, counter, format.ToString().ToLower());

        ++counter;
        return filename;
    }

    public void CaptureScreenshot()
    {
        captureScreenshot = true;
    }




    public bool isOn = true;
    void Update()
    {


        if (!isOn) return;

       
        PlayableDirector playableDirector = Flash.GetComponent<PlayableDirector>();
       
        FPEInteractablePickupScript pickupScript = GetCamera.GetComponent<FPEInteractablePickupScript>();
        if (pickupScript != null && pickupScript.pickedUp)
        {

   
            if (Input.GetMouseButtonDown(1))
            {

                isUsingCamera = !isUsingCamera;
                ToggleCameraUIState();
            }


        }
      
        if (isUsingCamera)
        {
            if (Getlens1 && lensManager.PickInfrared)
            {
                L2Code.SetActive(true);
                L4Code.SetActive(true);
            }
            else
            {
                L2Code.SetActive(false);
                L4Code.SetActive(false);
            }

        }

  
        captureScreenshot = Input.GetMouseButtonDown(0) && isUsingCamera;
        if (captureScreenshot || captureVideo)
        {
            captureScreenshot = false;
            
            if(GetFlash&&lensManager.PickFlash){
                
                playableDirector.Play();
                Invoke("picfile", 0.2f);

            }
            else
            {
                picfile2();

            }


        }
    }
    void ToggleCameraUIState()
    {

        isCameraUIActive = !isCameraUIActive;
        cameraUI.gameObject.SetActive(isCameraUIActive);


    }
    void ShowPreviewPhoto()
    {
 
        photoPreview = Instantiate(photoPreviewPrefab, photoPreviewPanel.transform).GetComponent<Image>();
        RectTransform rectTransform = photoPreview.GetComponent<RectTransform>();

        Texture2D tex2D = toTexture2D(renderTexture);
        photoPreview.sprite = Sprite.Create(tex2D, new Rect(0.0f, 0.0f, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f), 100.0f);

        photoPreview.GetComponent<RectTransform>().anchoredPosition = new Vector2(photoPreview.GetComponent<RectTransform>().anchoredPosition.x, 0f);

        float y = rectTransform.sizeDelta.y - rectTransform.anchoredPosition.x;
        rectTransform.DOAnchorPosY(y, 2f).OnComplete(() => StartCoroutine(ClearPhoto(photoPreview.gameObject)));
    }

    IEnumerator ClearPhoto(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        Destroy(obj);


        if (optimizeForManyScreenshots == false)
        {
            Destroy(renderTexture);
            renderTexture = null;
            screenShot = null;
        }

    }


    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
   
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    void picfile()
    {
        CancelInvoke("picfile");
        L1Code.SetActive(true);
        string mask = string.Format("screen_{0}x{1}*.{2}", (int)rect.width, (int)rect.height, format.ToString().ToLower());

        if (Directory.GetFiles(folder, mask, SearchOption.TopDirectoryOnly).Length >= 9)
        {
          
            full.gameObject.SetActive(true);
            full.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200f);
            full.GetComponent<CanvasGroup>().alpha = 0f;

            full.GetComponent<RectTransform>().DOAnchorPosY(300f, 1.5f);
            full.GetComponent<CanvasGroup>().DOFade(1f, 1.5f).OnComplete(() =>
            full.GetComponent<CanvasGroup>().DOFade(0f, 1.5f).OnComplete(() =>
            full.gameObject.SetActive(false)
            ));
            L1Code.SetActive(false);
            return;
        }

      
        if (hideGameObject != null) hideGameObject.SetActive(false);

       
        if (renderTexture == null)
        {
           
            rect = new Rect(0, 0, captureWidth, captureHeight);
            renderTexture = new RenderTexture(captureWidth, captureHeight, 24);
            screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        }

 
        Camera camera = Camera.main;
        camera.targetTexture = renderTexture;
        camera.Render();

        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(rect, 0, 0);

        camera.targetTexture = null;
        RenderTexture.active = null;

        string filename = uniqueFilename((int)rect.width, (int)rect.height);

        byte[] fileHeader = null;
        byte[] fileData = null;
        if (format == Format.RAW)
        {
            fileData = screenShot.GetRawTextureData();
        }
        else if (format == Format.PNG)
        {
            fileData = screenShot.EncodeToPNG();
        }
        else if (format == Format.JPG)
        {
            fileData = screenShot.EncodeToJPG();
        }
        else 
        {

            string headerStr = string.Format("P6{0}{1}255", rect.width, rect.height);
            fileHeader = System.Text.Encoding.ASCII.GetBytes(headerStr);
            fileData = screenShot.GetRawTextureData();
        }

        new System.Threading.Thread(() =>
        {     
            var f = System.IO.File.Create(filename);
            if (fileHeader != null) f.Write(fileHeader, 0, fileHeader.Length);
            f.Write(fileData, 0, fileData.Length);
            f.Close();
            Debug.Log(string.Format("Wrote screenshot {0} of size {1}", filename, fileData.Length));
        }).Start();


        if (hideGameObject != null) hideGameObject.SetActive(true);
        L1Code.SetActive(false);
 
        ShowPreviewPhoto();
    }
    void picfile2()
    {
        shootPhoto.PlayOneShot(shootPhoto.clip);
        string mask = string.Format("screen_{0}x{1}*.{2}", (int)rect.width, (int)rect.height, format.ToString().ToLower());

        if (Directory.GetFiles(folder, mask, SearchOption.TopDirectoryOnly).Length >= 9)
        {
          
            full.gameObject.SetActive(true);
            full.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200f);
            full.GetComponent<CanvasGroup>().alpha = 0f;

            full.GetComponent<RectTransform>().DOAnchorPosY(300f, 1.5f);
            full.GetComponent<CanvasGroup>().DOFade(1f, 1.5f).OnComplete(() =>
            full.GetComponent<CanvasGroup>().DOFade(0f, 1.5f).OnComplete(() =>
            full.gameObject.SetActive(false)
            ));
            return;
        }

 
        if (hideGameObject != null) hideGameObject.SetActive(false);


        if (renderTexture == null)
        {
      
            rect = new Rect(0, 0, captureWidth, captureHeight);
            renderTexture = new RenderTexture(captureWidth, captureHeight, 24);
            screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        }


        Camera camera = Camera.main;
        camera.targetTexture = renderTexture;
        camera.Render();

        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(rect, 0, 0);

        camera.targetTexture = null;
        RenderTexture.active = null;

        string filename = uniqueFilename((int)rect.width, (int)rect.height);

        byte[] fileHeader = null;
        byte[] fileData = null;
        if (format == Format.RAW)
        {
            fileData = screenShot.GetRawTextureData();
        }
        else if (format == Format.PNG)
        {
            fileData = screenShot.EncodeToPNG();
        }
        else if (format == Format.JPG)
        {
            fileData = screenShot.EncodeToJPG();
        }
        else 
        {

            string headerStr = string.Format("P6{0}{1}255", rect.width, rect.height);
            fileHeader = System.Text.Encoding.ASCII.GetBytes(headerStr);
            fileData = screenShot.GetRawTextureData();
        }


        new System.Threading.Thread(() =>
        {

            var f = System.IO.File.Create(filename);
            if (fileHeader != null) f.Write(fileHeader, 0, fileHeader.Length);
            f.Write(fileData, 0, fileData.Length);
            f.Close();
            Debug.Log(string.Format("Wrote screenshot {0} of size {1}", filename, fileData.Length));
        }).Start();


        if (hideGameObject != null) hideGameObject.SetActive(true);

        ShowPreviewPhoto();
    }
}

    