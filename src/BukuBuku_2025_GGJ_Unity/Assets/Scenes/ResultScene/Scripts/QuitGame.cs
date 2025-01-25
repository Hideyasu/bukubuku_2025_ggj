using UnityEngine;

public class QuitGame : MonoBehaviour
{
	// ゲーム終了:ボタンから呼び出す
	public void EndGame()
	{
#if UNITY_EDITOR											// エディター上で実行されてる場合
		UnityEditor.EditorApplication.isPlaying = false;	// ゲームが終了された扱いにする
#else
    Application.Quit();//ゲームプレイ終了					// 実際にアプリケーションを閉じる
#endif
	}
}
