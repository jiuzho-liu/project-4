using UnityEngine;
using UnityEngine.UI;

public class PasswordChecker : MonoBehaviour
{
    public InputField passwordInput;
    public Button submitButton;
    public Text messageText;

    private string correctPassword = "your_password_here"; // 将此处替换为你的密码

    void Start()
    {
        // 添加按钮点击事件监听器
        submitButton.onClick.AddListener(CheckPassword);
    }

    void CheckPassword()
    {
        string enteredPassword = passwordInput.text;

        if (enteredPassword == correctPassword)
        {
            messageText.text = "密码正确！门已打开。";
            // 在这里添加打开门的逻辑
        }
        else
        {
            messageText.text = "密码错误，请重试。";
        }

        // 清空输入框以便下次输入
        passwordInput.text = "";
    }
}