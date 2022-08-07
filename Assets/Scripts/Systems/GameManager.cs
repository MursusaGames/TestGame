using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text lastMessage;
    [SerializeField] private InputField textMessage;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void OnSendButton()
    {
        photonView.RPC("Send_Data", RpcTarget.AllBuffered,PhotonNetwork.NickName, textMessage.text);
    }
    [PunRPC]
    public void Send_Data(string nick,string message)
    {
        lastMessage.text = nick + " : " + message;
    }

}
