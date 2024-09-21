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

        // �����¼�  
        ScreenRecorder.OnScreenshotLimitReached += HandleScreenshotLimitReached;
        screenshotLimitText.text = "";
    }

    void OnDestroy()
    {
        // ȡ�������¼��Ա����ڴ�й©  
        ScreenRecorder.OnScreenshotLimitReached -= HandleScreenshotLimitReached;
    }

    void HandleScreenshotLimitReached(string message)
    {
        // ����UIԪ������ʾ��Ϣ  
        screenshotLimitText.text = message;
    }

    // ����һ������ķ��������ڻ�ȡScreenshotManager��ʵ��  
    // ����Ҫ���������Ŀ�ṹ��ʵ����  
    private ScreenRecorder GetScreenshotManager()
    {
        // ����ScreenshotManager��ʵ����������ͨ�����������һ�������ʽ  
        throw new NotImplementedException();
    }
}
