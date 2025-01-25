using UnityEngine;
// using UnityEngine.UI;  // 旧式の InputField 用
using TMPro;        // TMP_InputField を使用する場合

public class NameInputManager : MonoBehaviour
{
    // public InputField nameInputField;
    public TMP_InputField nameInputField; // TextMeshPro 版を使うならこちら

    // 名前入力確定時に呼ばれるメソッド（ボタン押下などにひも付ける）
    public void OnNameConfirm()
    {
        // InputField に入力された文字列をグローバル変数に格納
        GlobalParam.playerName = nameInputField.text;

        // デバッグログで確認（Optional）
        Debug.Log("入力された名前: " + GlobalParam.playerName);

        // 次のシーンへ移動する場合はここで実行
        // SceneManager.LoadScene("GameScene");  // 例
    }
}
