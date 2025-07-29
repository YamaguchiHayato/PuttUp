using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections.Generic;

public class PlayerListView : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerItem playerItemPrefab;
    [SerializeField] private Transform playerListRoot;

    private Dictionary<int, PlayerItem> items = new Dictionary<int, PlayerItem>();

    private void Start()
    {
        foreach (var p in PhotonNetwork.PlayerList)
        {
            AddPlayer(p);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayer(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (items.TryGetValue(otherPlayer.ActorNumber, out var item))
        {
            Destroy(item.gameObject);
            items.Remove(otherPlayer.ActorNumber);
        }
    }

    private void AddPlayer(Player player)
    {
        var item = Instantiate(playerItemPrefab, playerListRoot);
        item.Setup(player);
        items[player.ActorNumber] = item;
    }
}
