using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	[SerializeField] int timeLimit;			// 残り時間を設定する変数
	[SerializeField] Text timerText;		// 残り時間を表示する変数
	float time;								// 残り時間を計算する変数

	[SerializeField]
	string sceneName;						// 制限時間が0になったら飛ぶシーン名

	void Update()
	{
		time += Time.deltaTime;					// フレーム毎の経過時間をtime変数に追加
		int remaining = timeLimit - (int)time;	// time変数をint型に変換し制限時間から引いた数をlimit変数に代入
		if ( remaining > 0 )
		{
			timerText.text = $"{remaining.ToString("D3")}";		// timerTextを更新していく
		}
		else													// 残り時間0以下になった場合
		{
			timerText.text = $"{0}";							// 0を表示
			SceneManager.LoadScene(sceneName); ;				// 画面遷移する
		}
	}
}