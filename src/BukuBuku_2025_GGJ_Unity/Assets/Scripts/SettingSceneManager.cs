using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingSceneManager : MonoBehaviour
{
    [SerializeField] private Slider thresholdSlider;

    private void Start()
    {
        // スライダーの最小値と最大値を指定
        thresholdSlider.minValue = -10f;
        thresholdSlider.maxValue = 5f;

        // スライダーの初期値を、PlayerPrefsに保存されている値(-10f～5f)に合わせる
        float currentThreshold = PlayerPrefs.GetFloat("Threshold", 5f);
        // 読み込んだ値を -10～0 の範囲にClampしておくと安全
        currentThreshold = Mathf.Clamp(currentThreshold, -10f, 5f);

        thresholdSlider.value = currentThreshold;

        // スライダーが変更された時にPlayerPrefsへ保存
        thresholdSlider.onValueChanged.AddListener(OnThresholdSliderChanged);
    }

    private void OnThresholdSliderChanged(float newValue)
    {
        // -10～5 の範囲で値が変更される
        Debug.Log($"Threshold updated: {newValue}");

        // PlayerPrefsに書き込み
        PlayerPrefs.SetFloat("Threshold", newValue);
        // 必要に応じて強制保存（PCなどでは不要なことも多い）
        PlayerPrefs.Save();
    }
}
