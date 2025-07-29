using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Matchmaking : Photon.Pun.MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        //SceneManager.LoadScene("Lobby");
        SceneManager.LoadSceneAsync("Lobby", LoadSceneMode.Single);
    }
}
