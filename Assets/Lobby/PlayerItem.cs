using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private Button readyButton;

    private Player player;

    public void Setup(Player p)
    {
        player = p;
        playerNameText.text = p.NickName;

        // 自分以外の準備ボタンは押せない
        readyButton.interactable = p.IsLocal;

        readyButton.onClick.AddListener(() =>
        {
            Debug.Log($"{player.NickName} が準備しました！");
        });
    }
}