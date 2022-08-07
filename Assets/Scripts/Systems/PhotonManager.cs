using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private List<Transform> spawnPos;
    [SerializeField] private string region;
    [SerializeField] private string nickName;
    [SerializeField] private InputField roomName;
    [SerializeField] private RoomPrefab roomPrefab;
    [SerializeField] private Transform content;
    List<RoomInfo> allRooms = new List<RoomInfo>();
    private int index;
    private GameObject player;
    [SerializeField] private GameObject playerPrefab;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(region);
        if(SceneManager.GetActiveScene().name == "game_scene")
        {
            player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos[Random.Range(0, spawnPos.Count)].position, Quaternion.identity);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Вы подключились к " + PhotonNetwork.CloudRegion);
        if (string.IsNullOrEmpty(nickName)) PhotonNetwork.NickName = "User";
        else PhotonNetwork.NickName = nickName;
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Вы отключились от сервера");
    }
    public void CreateRoomButton()
    {
        if (!PhotonNetwork.IsConnected) return;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(roomName.text, roomOptions, TypedLobby.Default);
        
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Создана комната " + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Не удалось создать комнату");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList)
        {
            for(int i =0; i < allRooms.Count; i++)
            {
                if (allRooms[i].masterClientId == info.masterClientId)
                    return;
            }
            RoomPrefab prefab = Instantiate(roomPrefab, content);
            if (null != prefab)
            {
                prefab.SetInfo(info);
                allRooms.Add(info);
            }
                
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("game_scene");
    }

    public void JoinRandomRoomBtn()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinButton()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Destroy(player.gameObject);
        PhotonNetwork.LoadLevel("main");
    }
}

