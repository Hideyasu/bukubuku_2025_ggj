using Photon.Pun;
using TMPro;
using UnityEngine;

public class LobbySceneManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI displayName;

    private void Start()
    {
        displayName.text = PhotonNetwork.NickName;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
}
