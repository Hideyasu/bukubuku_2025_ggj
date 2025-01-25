using Photon.Pun;
using UnityEngine;

public class SelectRoomManager : MonoBehaviourPunCallbacks
{
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
}
