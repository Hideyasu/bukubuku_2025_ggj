using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField nameInputField;

    public void OnNameConfirm()
    {
        if (nameInputField.text == "") {
            return;
        }
        PhotonNetwork.NickName = nameInputField.text;
        PhotonNetwork.ConnectUsingSettings();
        SceneManager.LoadScene("LobbyScene");
    }
}
