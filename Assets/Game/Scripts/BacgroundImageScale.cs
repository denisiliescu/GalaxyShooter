using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacgroundImageScale : MonoBehaviour
{   
    [SerializeField]
    private GameObject bgImage;
    [SerializeField]
    private Camera mainCam;

    private ScreenManager _screenManager;


    // Start is called before the first frame update
    void Start()
    {
        _screenManager = GameObject.Find("ScreenManager").GetComponent<ScreenManager>();
        BgImageScale();
    }


    private void BgImageScale()
    {
        // Step1: Get Device Screen Aspect Ratio
        Vector2 deviceScreenResolution = new Vector2(Screen.width, Screen.height);
        Debug.Log(deviceScreenResolution);

        float screenHeight = Screen.height;
        float screenWidth = Screen.width;

        float DEVICE_SCREEN_ASPECT = screenWidth / screenHeight;
        Debug.Log("Device screen aspect: " + DEVICE_SCREEN_ASPECT);

        // Step2: Set Main Camera's aspect = Device's Aspect

        mainCam.aspect = DEVICE_SCREEN_ASPECT;



        // Step3: Scale Bg Image to fit camera size

        float camHeight = 100.0f * mainCam.orthographicSize * 2.0f;
        float camWidth = camHeight *DEVICE_SCREEN_ASPECT;
        Debug.Log("height" + camHeight.ToString());
        Debug.Log("width" + camWidth.ToString());


        // Get Background Image Size
        SpriteRenderer bgImageSR = bgImage.GetComponent<SpriteRenderer>();
        float bgImgH = bgImageSR.sprite.rect.height;
        float bgImgW = bgImageSR.sprite.rect.width;
        Debug.Log("height1 " + bgImgH.ToString());
        Debug.Log("width1 " + bgImgW.ToString());


        //Calculate Ratio for Scaling
        float bgImg_scale_ratio_Height = camHeight / bgImgH;
        float bgImg_scale_ratio_Width = camWidth / bgImgW;
        bgImage.transform.localScale = new Vector3(bgImg_scale_ratio_Width, bgImg_scale_ratio_Height, 1);



        Debug.Log("screen height" + _screenManager.getScreenHeight());
        Debug.Log("screen width" + _screenManager.getScreenWidth());
        


    }
}
