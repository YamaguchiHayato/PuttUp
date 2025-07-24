
using UnityEngine;
using UnityEngine.UI; // Textを使う場合

public class ShowTextOnCollision : MonoBehaviour
{
    public Text messageText; // InspectorでUI Textを割り当てる

    void Start()
    {
        if (messageText != null)
        {
            messageText.enabled = false; // 最初は非表示
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target")) // タグで判定
        {
            if (messageText != null)
            {
                messageText.text = "当たった！";
                messageText.enabled = true;
            }
        }
    }
}
