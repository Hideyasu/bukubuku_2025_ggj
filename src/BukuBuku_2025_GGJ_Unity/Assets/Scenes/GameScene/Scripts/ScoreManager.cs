using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class ScoreManager : MonoBehaviour
{
	int score = 0;						// スコアを設定する変数
	[SerializeField] Text scoreText;        // 残り時間を表示する変数

	[SerializeField] private string m_DeviceName;	// デバイス
	private AudioClip m_AudioClip;					// オーディオ用の変数
	private int m_LastAudioPos;						// マイクのデバイスを保存しておく変数
	private float m_AudioLevel;						// オーディオの音量


	// [SerializeField] private GameObject m_Cube;							// テストシーンで使用したキューブ(ゲームシーンにはないのでエラーが出る)
	[SerializeField, Range(10, 100)] private float m_AmpGain = 10;
	[SerializeField] public float threshold = 0.1f;     // しきい値


    void Start()
	{
		string targetDevice = "";

		foreach (var device in Microphone.devices)
		{
			Debug.Log($"Device Name: {device}");
			if (device.Contains(m_DeviceName))
			{
				targetDevice = device;
			}
		}

		Debug.Log($"=== Device Set: {targetDevice} ===");
		m_AudioClip = Microphone.Start(targetDevice, true, 10, 48000);
	}

	void Update()
	{
		float[] waveData = GetUpdatedAudio();
		if (waveData.Length == 0) return;


        var firstValues = waveData.Take(10).Select(v => v.ToString("F3"));

        m_AudioLevel = waveData.Average(Mathf.Abs);
        // m_Cube.transform.localScale = new Vector3(1, 1 + m_AmpGain * m_AudioLevel, 1); // テストシーンで長方形を変形させている部分	

        ////////////////////////////////////////////////////////////////////
        // m_AmpGain や m_AudioLevel を用いてスコアの計算をする部分を実装 //
        ////////////////////////////////////////////////////////////////////

        if (m_AudioLevel > threshold)
        {
            score += (int)(m_AudioLevel * 100);
        }

        scoreText.text = $"{score}";			// 画面に表示されたスコアを更新していく処理
	}

	private float[] GetUpdatedAudio()
	{

		int nowAudioPos = Microphone.GetPosition(null);// nullでデフォルトデバイス

		float[] waveData = Array.Empty<float>();

		if (m_LastAudioPos < nowAudioPos)
		{
			int audioCount = nowAudioPos - m_LastAudioPos;
			waveData = new float[audioCount];
			m_AudioClip.GetData(waveData, m_LastAudioPos);
		}
		else if (m_LastAudioPos > nowAudioPos)
		{
			int audioBuffer = m_AudioClip.samples * m_AudioClip.channels;
			int audioCount = audioBuffer - m_LastAudioPos;

			float[] wave1 = new float[audioCount];
			m_AudioClip.GetData(wave1, m_LastAudioPos);

			float[] wave2 = new float[nowAudioPos];
			if (nowAudioPos != 0)
			{
				m_AudioClip.GetData(wave2, 0);
			}

			waveData = new float[audioCount + nowAudioPos];
			wave1.CopyTo(waveData, 0);
			wave2.CopyTo(waveData, audioCount);
		}

		m_LastAudioPos = nowAudioPos;

		return waveData;
	}
}
