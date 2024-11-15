using UnityEngine;
using UnityEngine.UI;

public class PasswordChecker : MonoBehaviour
{
    public InputField passwordInput; // �������������
    public Button submitButton;     // �����ύ��ť
    public string correctPassword = "yourpassword"; // ������ȷ������

    // Use this for initialization
    void Start()
    {
        // ��Ӱ�ť����¼�������
        submitButton.onClick.AddListener(CheckPassword);
    }

    // �������������Ƿ���ȷ
    void CheckPassword()
    {
        string enteredPassword = passwordInput.text;
        if (enteredPassword == correctPassword)
        {
            Debug.Log("Password is correct!");
            // ��������Ӵ��ŵ��߼���������ʾ�ſ��Ķ����򴥷��ſ��Ĵ�����
        }
        else
        {
            Debug.Log("Password is incorrect!");
            // ��������Ӵ�����ʾ��������ʾ������Ϣ����Ч��
        }
    }
}