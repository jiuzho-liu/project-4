using UnityEngine;
using UnityEngine.UI;

public class PasswordChecker : MonoBehaviour
{
    public InputField passwordInput; // 引用密码输入框
    public Button submitButton;     // 引用提交按钮
    public string correctPassword = "yourpassword"; // 设置正确的密码

    // Use this for initialization
    void Start()
    {
        // 添加按钮点击事件监听器
        submitButton.onClick.AddListener(CheckPassword);
    }

    // 检查输入的密码是否正确
    void CheckPassword()
    {
        string enteredPassword = passwordInput.text;
        if (enteredPassword == correctPassword)
        {
            Debug.Log("Password is correct!");
            // 在这里添加打开门的逻辑，比如显示门开的动画或触发门开的触发器
        }
        else
        {
            Debug.Log("Password is incorrect!");
            // 在这里添加错误提示，比如显示错误消息或震动效果
        }
    }
}