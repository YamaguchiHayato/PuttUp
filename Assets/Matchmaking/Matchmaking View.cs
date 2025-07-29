using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchmakingView : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RoomListView roomListView = default;
    [SerializeField]
    private TMP_InputField roomNameInputField = default;
    [SerializeField]
    private Button createRoomButton = default;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        // Photonに接続
        PhotonNetwork.ConnectUsingSettings();

        // ロビーに参加するまでは、入力できないようにする
        canvasGroup.interactable = false;

        // ルームリスト表示を初期化する
        roomListView.Init(this);

        roomNameInputField.onValueChanged.AddListener(OnRoomNameInputFieldValueChanged);
        createRoomButton.onClick.AddListener(OnCreateRoomButtonClick);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        // ロビーに参加したら、入力できるようにする
        canvasGroup.interactable = true;

        // MatchmakingのUIを表示する（もし非表示になっていたら）
        gameObject.SetActive(true);
        Debug.Log("ロビーに入りました！");
    }

    private void OnRoomNameInputFieldValueChanged(string value)
    {
        // ルーム名が1文字以上入力されている時のみ、ルーム作成ボタンを押せるようにする
        createRoomButton.interactable = (value.Length > 0);
    }

    private void OnCreateRoomButtonClick()
    {
        // 入力できないようにする
        canvasGroup.interactable = false;

        // 状態チェック（超重要！）
        if (!PhotonNetwork.IsConnectedAndReady || PhotonNetwork.NetworkClientState != ClientState.JoinedLobby)
        {
            Debug.LogError("まだロビーに入っていないため、ルームを作成できません。");
            canvasGroup.interactable = true; // UIを戻す
            return;
        }

        // 入力フィールドに入力したルーム名のルームを作成する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
    }

    //private void OnCreateRoomButtonClick()
    //{
    //    // ルーム作成処理中は、入力できないようにする
    //    canvasGroup.interactable = false;

    //    if (!PhotonNetwork.IsConnectedAndReady)
    //    {
    //        Debug.LogWarning("まだ接続準備ができていません。");
    //        return;
    //    }

    //    // 入力フィールドに入力したルーム名のルームを作成する
    //    var roomOptions = new RoomOptions();
    //    roomOptions.MaxPlayers = 4;
    //    PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
    //}

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // ルームの作成が失敗したら、再び入力できるようにする
        roomNameInputField.text = string.Empty;
        canvasGroup.interactable = true;
    }

    public void OnJoiningRoom()
    {
        // ルーム参加処理中は、入力できないようにする
        canvasGroup.interactable = false;
    }

    public override void OnJoinedRoom()
    {
        // ルーム入室成功時にRoomSceneをロード
        PhotonNetwork.LoadLevel("Lobby");

        // ルームへの参加が成功したら、UIを非表示にする
        gameObject.SetActive(false);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // ルームへの参加が失敗したら、再び入力できるようにする
        canvasGroup.interactable = true;
    }
}