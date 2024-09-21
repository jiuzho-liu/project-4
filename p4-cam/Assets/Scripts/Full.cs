using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Full : MonoBehaviour
{
    public Text screenshotLimitText;
    private ScreenRecorder ScreenRecorder;
    void Start()
    {

        ScreenRecorder = GetScreenshotManager();

        // 订阅事件  
        ScreenRecorder.OnScreenshotLimitReached += HandleScreenshotLimitReached;
        screenshotLimitText.text = "";
    }

    void OnDestroy()
    {
        // 取消订阅事件以避免内存泄漏  
        ScreenRecorder.OnScreenshotLimitReached -= HandleScreenshotLimitReached;
    }

    void HandleScreenshotLimitReached(string message)
    {
        // 更新UI元素以显示消息  
        screenshotLimitText.text = message;
    }

    // 这是一个假设的方法，用于获取ScreenshotManager的实例  
    // 你需要根据你的项目结构来实现它  
    private ScreenRecorder GetScreenshotManager()
    {
        // 返回ScreenshotManager的实例，可能是通过单例、查找或其他方式  
        throw new NotImplementedException();
    }
}
