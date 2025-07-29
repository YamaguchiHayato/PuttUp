using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);  // シーンをまたいでも残す
        PhotonNetwork.AutomaticallySyncScene = true;  // ルーム入室後に全員でシーン同期
    }

    private void Start()
    {
        PhotonNetwork.NickName = "Player_" + Random.Range(1000, 9999);

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(); // ロビーに入る
    }
}
