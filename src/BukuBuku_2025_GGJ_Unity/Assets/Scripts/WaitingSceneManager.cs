using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class SelectDrinkSceneManager : MonoBehaviourPunCallbacks
{
    public GameObject playButton;
    public GameObject playerImage1;
    public GameObject playerImage2;
    public GameObject playerImage3;
    public GameObject playerImage4;
    public TextMeshProUGUI displayName;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        displayName.text = PhotonNetwork.NickName;

        if (PhotonNetwork.InRoom) // ルームに参加している場合
        {
            int currentPlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            switch (currentPlayerCount)
            {
                case 1:
                    playerImage1.SetActive(true);
                    playerImage2.SetActive(false);
                    playerImage3.SetActive(false);
                    playerImage4.SetActive(false);
                    break;
                case 2:
                    playerImage1.SetActive(true);
                    playerImage2.SetActive(true);
                    playerImage3.SetActive(false);
                    playerImage4.SetActive(false);
                    break;
                case 3:
                    playerImage1.SetActive(true);
                    playerImage2.SetActive(true);
                    playerImage3.SetActive(true);
                    playerImage4.SetActive(false);
                    break;
                case 4:
                    playerImage1.SetActive(true);
                    playerImage2.SetActive(true);
                    playerImage3.SetActive(true);
                    playerImage4.SetActive(true);
                    break;
                default:
                    playerImage1.SetActive(false);
                    playerImage2.SetActive(false);
                    playerImage3.SetActive(false);
                    playerImage4.SetActive(false);
                    break;
            }
        }
        else
        {
            Debug.Log("Not in a room.");
        }
    }
    
    private void Update() {
        if (PhotonNetwork.InRoom) // ルームに参加している場合
        {
            int currentPlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            switch (currentPlayerCount)
            {
                case 1:
                    playerImage1.SetActive(true);
                    playerImage2.SetActive(false);
                    playerImage3.SetActive(false);
                    playerImage4.SetActive(false);
                    break;
                case 2:
                    playerImage1.SetActive(true);
                    playerImage2.SetActive(true);
                    playerImage3.SetActive(false);
                    playerImage4.SetActive(false);
                    break;
                case 3:
                    playerImage1.SetActive(true);
                    playerImage2.SetActive(true);
                    playerImage3.SetActive(true);
                    playerImage4.SetActive(false);
                    break;
                case 4:
                    playerImage1.SetActive(true);
                    playerImage2.SetActive(true);
                    playerImage3.SetActive(true);
                    playerImage4.SetActive(true);
                    break;
                default:
                    playerImage1.SetActive(false);
                    playerImage2.SetActive(false);
                    playerImage3.SetActive(false);
                    playerImage4.SetActive(false);
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
