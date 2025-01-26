using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ResultSceneManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI displayName;

    private void Start()
    {
        displayName.text = PhotonNetwork.NickName;
        PhotonNetwork.AutomaticallySyncScene = false;
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("LobbyScene");
        PhotonNetwork.LeaveRoom();
    }
}