using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingSceneManager : MonoBehaviour
{
    [SerializeField] private Slider thresholdSlider;

    private void Start()
    {
        // �X���C�_�[�̍ŏ��l�ƍő�l���w��
        thresholdSlider.minValue = -10f;
        thresholdSlider.maxValue = 5f;

        // �X���C�_�[�̏����l���APlayerPrefs�ɕۑ�����Ă���l(-10f�`5f)�ɍ��킹��
        float currentThreshold = PlayerPrefs.GetFloat("Threshold", 5f);
        // �ǂݍ��񂾒l�� -10�`0 �͈̔͂�Clamp���Ă����ƈ��S
        currentThreshold = Mathf.Clamp(currentThreshold, -10f, 5f);

        thresholdSlider.value = currentThreshold;

        // �X���C�_�[���ύX���ꂽ����PlayerPrefs�֕ۑ�
        thresholdSlider.onValueChanged.AddListener(OnThresholdSliderChanged);
    }

    private void OnThresholdSliderChanged(float newValue)
    {
        // -10�`5 �͈̔͂Œl���ύX�����
        Debug.Log($"Threshold updated: {newValue}");

        // PlayerPrefs�ɏ�������
        PlayerPrefs.SetFloat("Threshold", newValue);
        // �K�v�ɉ����ċ����ۑ��iPC�Ȃǂł͕s�v�Ȃ��Ƃ������j
        PlayerPrefs.Save();
    }
}
