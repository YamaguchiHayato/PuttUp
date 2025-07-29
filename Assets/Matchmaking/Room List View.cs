using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class RoomListView : MonoBehaviourPunCallbacks
{
    private const int MaxElements = 20;

    [SerializeField]
    private RoomListViewElement elementPrefab = default;

    private RoomList roomList = new RoomList();
    private List<RoomListViewElement> elementList = new List<RoomListViewElement>(MaxElements);
    private ScrollRect scrollRect;

    public void Init(MatchmakingView parentView)
    {
        scrollRect = GetComponent<ScrollRect>();

        // ルームリスト要素（ルーム参加ボタン）を生成して初期化する
        for (int i = 0; i < MaxElements; i++)
        {
            var element = Instantiate(elementPrefab, scrollRect.content);
            element.Init(parentView);
            element.Hide();
            elementList.Add(element);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> changedRoomList)
    {
        roomList.Update(changedRoomList);

        // 安全のため List にコピーしておく
        List<RoomInfo> rooms = new List<RoomInfo>(roomList);

        int index = 0;

        // 最大数に制限しながら表示
        foreach (var roomInfo in rooms)
        {
            if (index >= elementList.Count)
            {
                Debug.LogWarning($"表示可能な上限({elementList.Count})を超えるルームが存在しています。");
                break;
            }

            elementList[index].Show(roomInfo);
            index++;
        }

        // 残りは非表示に
        for (int i = index; i < elementList.Count; i++)
        {
            elementList[i].Hide();
        }
    }
}
