
using UnityEngine;
using UnityEngine.UI; // Text���g���ꍇ

public class ShowTextOnCollision : MonoBehaviour
{
    public Text messageText; // Inspector��UI Text�����蓖�Ă�

    void Start()
    {
        if (messageText != null)
        {
            messageText.enabled = false; // �ŏ��͔�\��
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target")) // �^�O�Ŕ���
        {
            if (messageText != null)
            {
                messageText.text = "���������I";
                messageText.enabled = true;
            }
        }
    }
}
