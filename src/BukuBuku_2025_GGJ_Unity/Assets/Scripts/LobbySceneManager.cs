using Photon.Pun;
using UnityEngine;

public class LobbySceneManager : MonoBehaviourPunCallbacks
{
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
}
