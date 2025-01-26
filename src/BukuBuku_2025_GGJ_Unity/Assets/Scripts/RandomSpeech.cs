using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class RandomSpeech : MonoBehaviour
{
	[SerializeField]
	[Header("テキストオブジェクト")]
	Text speechText;

	[SerializeField]
	[Header("ふきだしオブジェクト")]
	GameObject speechPanel;

	[SerializeField]
	[Header("セリフたち")]
	private string[] speeches;

	// 次に振り向くまでの待機時間
	[SerializeField]
	[Header("次に振り向くまでの時間")]
	float nextWaitingTime = 10.0f;

	// ふきだしが表示される時間
	[SerializeField]
	[Header("ふきだしが表示される時間")]
	float speakingTime = 1.5f;

	// タイマー
	[SerializeField]
	[Header("制限時間")]
	private float limitTime = 60;       // 本来は制限時間をゲームマネージャーから取得したい

	bool isWatching = false;

	public bool IsWatching()
	{
		return isWatching;
	}

	void Start()
    {
		StartCoroutine(PlayRandomSpeak());
	}

	public IEnumerator PlayRandomSpeak()
	{
		while (limitTime > 0)                                        // 制限時間までループする
		{
			yield return null;
			limitTime -= Time.deltaTime;                            // 経過時間の加算
			yield return StartCoroutine(NextRandomSpeak());			// 喋る
		}
	}

	IEnumerator NextRandomSpeak()
	{
		float timer = 0;
		while (nextWaitingTime > timer)
		{
			yield return null;               // 待機
			timer += Time.deltaTime;         // タイマーの加算
		}
		//speechPanel.SetActive(true);
		isWatching = true;
		speechText.text = speeches.ElementAt(Random.Range(0, speeches.Count()));    // ランダムなテキストを反映
		timer = 0;
		while (speakingTime > timer)
		{
			yield return null;               // 待機
			timer += Time.deltaTime;         // タイマーの加算
		}
		//speechPanel.SetActive(false);
		isWatching = false;
	}
}
