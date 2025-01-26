using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ResultSceneManager : MonoBehaviourPunCallbacks
{
    public void OnClickBack()
    {
        SceneManager.LoadScene("TitleScene");
    }
}