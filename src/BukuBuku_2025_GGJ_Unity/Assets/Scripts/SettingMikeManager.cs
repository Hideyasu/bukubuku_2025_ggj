using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;


public class SettingMikeManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image levelMeterImage;  // 音量レベルメーター用の Image
    [SerializeField] private TMP_Dropdown micDropdown;   // マイク選択用 Dropdown

    [Header("Audio Settings")]
    [SerializeField, Range(10, 100)]
    private float m_AmpGain = 10f; // 音量倍率（メーター拡大用）

    // 内部管理用
    private AudioClip micClip;     // 現在使用中のマイククリップ
    private int lastMicPos;        // 前回のサンプル位置
    private float micLevel;        // 現在のマイク音量レベル
    private string selectedMicName; // 選択したマイク名

    private void Start()
    {
        // --- マイクデバイス一覧を取得し、Dropdownに表示 ---
        micDropdown.ClearOptions();
        var devices = Microphone.devices.ToList();  // 取得したマイクデバイスのリスト
        micDropdown.AddOptions(devices);

        // --- 前回選択したマイク名を読み込み ---
        selectedMicName = PlayerPrefs.GetString("SelectedMic", "");
        int index = devices.IndexOf(selectedMicName);
        if (index < 0) index = 0; // 見つからなければ先頭を選択
        micDropdown.value = index;

        // --- マイク録音開始 (選択されたデバイス名) ---
        if (devices.Count > 0)
        {
            selectedMicName = devices[index];
            micClip = Microphone.Start(selectedMicName, true, 10, 48000);
        }

        // --- Dropdown操作時のリスナー設定 ---
        micDropdown.onValueChanged.AddListener((i) =>
        {
            selectedMicName = devices[i];
            // PlayerPrefsにマイク名を保存
            PlayerPrefs.SetString("SelectedMic", selectedMicName);
            PlayerPrefs.Save();

            // いったんマイク録音停止 → 新しく開始
            if (micClip) Microphone.End(null);
            micClip = Microphone.Start(selectedMicName, true, 10, 48000);
            lastMicPos = 0;
        });
    }

    private void Update()
    {
        // --- マイクレベルを取得し、レベルメーター (Image) の幅を伸縮 ---
        float[] waveData = GetUpdatedAudio();
        if (waveData.Length == 0) return;

        // 波形データの平均絶対値を音量レベルとする
        micLevel = waveData.Average(Mathf.Abs);

        // メーターを横方向にスケール
        // Image の RectTransform 幅を伸ばす例
        RectTransform rt = levelMeterImage.rectTransform;
        float newWidth = 100f * (1f + m_AmpGain * micLevel); // 基本幅100に、音量レベルを加味
        rt.sizeDelta = new Vector2(newWidth, rt.sizeDelta.y);
    }

    /// <summary>
    /// 新しく録音された音声データを取得して返す
    /// </summary>
    private float[] GetUpdatedAudio()
    {
        if (string.IsNullOrEmpty(selectedMicName) || micClip == null)
            return System.Array.Empty<float>();

        int nowPos = Microphone.GetPosition(selectedMicName);
        if (nowPos < 0 || !Microphone.IsRecording(selectedMicName))
            return System.Array.Empty<float>();

        float[] waveData = System.Array.Empty<float>();

        // 録音位置が前回より進んでいる場合
        if (lastMicPos < nowPos)
        {
            int audioCount = nowPos - lastMicPos;
            waveData = new float[audioCount];
            micClip.GetData(waveData, lastMicPos);
        }
        // ループして録音位置が巻き戻った場合
        else if (lastMicPos > nowPos)
        {
            int audioBuffer = micClip.samples * micClip.channels;
            int audioCount = audioBuffer - lastMicPos;

            float[] wave1 = new float[audioCount];
            micClip.GetData(wave1, lastMicPos);

            float[] wave2 = new float[nowPos];
            if (nowPos > 0)
            {
                micClip.GetData(wave2, 0);
            }

            waveData = new float[audioCount + nowPos];
            wave1.CopyTo(waveData, 0);
            wave2.CopyTo(waveData, audioCount);
        }

        lastMicPos = nowPos;
        return waveData;
    }
}
