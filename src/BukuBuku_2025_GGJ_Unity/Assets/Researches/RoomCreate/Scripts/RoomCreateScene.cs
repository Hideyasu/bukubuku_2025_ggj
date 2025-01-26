using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomCreateScene : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    //// マスターサーバーへの接続が成功した時に呼ばれるコールバック
    //public override void OnConnectedToMaster()
    //{
    //    // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
    //    // PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    //}

    //// ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    //public override void OnJoinedRoom()
    //{
    //    // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
    //    // var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
    //    // PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);
    //}
    public void OnCreateRoom()
    {
        PhotonNetwork.CreateRoom(null);  // null is auto create room
    }

    public void OnBackScene()
    {
        SceneManager.LoadScene("RoomSelect");
    }

    // ルームの作成が成功した時に呼ばれるコールバック
    public override void OnCreatedRoom()
    {
        Debug.Log("ルームの作成に成功しました");  //　今はログだが、そのうちプレイ待機にとぶ
    }

    // ルームの作成が失敗した時に呼ばれるコールバック
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"ルームの作成に失敗しました: {message}");
    }
}
