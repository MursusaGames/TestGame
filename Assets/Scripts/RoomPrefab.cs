using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class RoomPrefab : MonoBehaviour
{
    [SerializeField] private Text roomName;
    [SerializeField] private Text userCount;

    public void SetInfo(RoomInfo info)
    {
        roomName.text = info.Name;
        userCount.text = info.PlayerCount + "/" + info.MaxPlayers;
    }

    public void JoinToListRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }
}
