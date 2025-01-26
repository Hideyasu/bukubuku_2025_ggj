using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SelectDrinkSceneManager : MonoBehaviourPunCallbacks
{
    public GameObject playButton;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.InRoom) // ルームに参加している場合
        {
            int currentPlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            switch (currentPlayerCount)
            {
                case 1:
                    Debug.Log("1");
                    break;
                case 2:
                    Debug.Log("2");
                    break;
                case 3:
                    Debug.Log("3");
                    break;
                case 4:
                    Debug.Log("4");
                    break;
                default:
                    Debug.Log("x");
                    break;
            }
        }
        else
        {
            Debug.Log("Not in a room.");
        }
    }

    public void OnClickPlay()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameScene");
            Debug.Log("MasterClient is changing the scene to: GameScene");
        }
        else {
            Debug.LogWarning("Only the MasterClient can change the scene.");
        }
    }
}
