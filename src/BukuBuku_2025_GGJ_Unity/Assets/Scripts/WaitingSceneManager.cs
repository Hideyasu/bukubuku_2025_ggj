using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SelectDrinkSceneManager : MonoBehaviourPunCallbacks
{
    public GameObject playButton;

    private void Start()
    {
        Debug.Log("this room's player is ");
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        Debug.Log("you are ");
        Debug.Log(PhotonNetwork.IsMasterClient);
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.IsMasterClient)
        {
            playButton.SetActive(true);
        }
    }

    public void OnClickPlay()
    {
        PhotonNetwork.LoadLevel("GameScene");
        Debug.Log("MasterClient is changing the scene to: GameScene");
    }
}
