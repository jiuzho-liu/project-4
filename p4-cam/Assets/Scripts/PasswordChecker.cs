using UnityEngine;
using UnityEngine.UI;

public class PasswordChecker : MonoBehaviour
{
    public InputField passwordInput;
    public Button submitButton;
    public Text messageText;

    private string correctPassword = "your_password_here"; // ���˴��滻Ϊ�������

    void Start()
    {
        // ��Ӱ�ť����¼�������
        submitButton.onClick.AddListener(CheckPassword);
    }

    void CheckPassword()
    {
        string enteredPassword = passwordInput.text;

        if (enteredPassword == correctPassword)
        {
            messageText.text = "������ȷ�����Ѵ򿪡�";
            // ��������Ӵ��ŵ��߼�
        }
        else
        {
            messageText.text = "������������ԡ�";
        }

        // ���������Ա��´�����
        passwordInput.text = "";
    }
}