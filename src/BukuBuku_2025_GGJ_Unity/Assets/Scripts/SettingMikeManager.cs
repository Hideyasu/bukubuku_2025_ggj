using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;


public class SettingMikeManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image levelMeterImage;  // ���ʃ��x�����[�^�[�p�� Image
    [SerializeField] private TMP_Dropdown micDropdown;   // �}�C�N�I��p Dropdown

    [Header("Audio Settings")]
    [SerializeField, Range(10, 100)]
    private float m_AmpGain = 10f; // ���ʔ{���i���[�^�[�g��p�j

    // �����Ǘ��p
    private AudioClip micClip;     // ���ݎg�p���̃}�C�N�N���b�v
    private int lastMicPos;        // �O��̃T���v���ʒu
    private float micLevel;        // ���݂̃}�C�N���ʃ��x��
    private string selectedMicName; // �I�������}�C�N��

    private void Start()
    {
        // --- �}�C�N�f�o�C�X�ꗗ���擾���ADropdown�ɕ\�� ---
        micDropdown.ClearOptions();
        var devices = Microphone.devices.ToList();  // �擾�����}�C�N�f�o�C�X�̃��X�g
        micDropdown.AddOptions(devices);

        // --- �O��I�������}�C�N����ǂݍ��� ---
        selectedMicName = PlayerPrefs.GetString("SelectedMic", "");
        int index = devices.IndexOf(selectedMicName);
        if (index < 0) index = 0; // ������Ȃ���ΐ擪��I��
        micDropdown.value = index;

        // --- �}�C�N�^���J�n (�I�����ꂽ�f�o�C�X��) ---
        if (devices.Count > 0)
        {
            selectedMicName = devices[index];
            micClip = Microphone.Start(selectedMicName, true, 10, 48000);
        }

        // --- Dropdown���쎞�̃��X�i�[�ݒ� ---
        micDropdown.onValueChanged.AddListener((i) =>
        {
            selectedMicName = devices[i];
            // PlayerPrefs�Ƀ}�C�N����ۑ�
            PlayerPrefs.SetString("SelectedMic", selectedMicName);
            PlayerPrefs.Save();

            // ��������}�C�N�^����~ �� �V�����J�n
            if (micClip) Microphone.End(null);
            micClip = Microphone.Start(selectedMicName, true, 10, 48000);
            lastMicPos = 0;
        });
    }

    private void Update()
    {
        // --- �}�C�N���x�����擾���A���x�����[�^�[ (Image) �̕���L�k ---
        float[] waveData = GetUpdatedAudio();
        if (waveData.Length == 0) return;

        // �g�`�f�[�^�̕��ϐ�Βl�����ʃ��x���Ƃ���
        micLevel = waveData.Average(Mathf.Abs);

        // ���[�^�[���������ɃX�P�[��
        // Image �� RectTransform ����L�΂���
        RectTransform rt = levelMeterImage.rectTransform;
        float newWidth = 100f * (1f + m_AmpGain * micLevel); // ��{��100�ɁA���ʃ��x��������
        rt.sizeDelta = new Vector2(newWidth, rt.sizeDelta.y);
    }

    /// <summary>
    /// �V�����^�����ꂽ�����f�[�^���擾���ĕԂ�
    /// </summary>
    private float[] GetUpdatedAudio()
    {
        if (string.IsNullOrEmpty(selectedMicName) || micClip == null)
            return System.Array.Empty<float>();

        int nowPos = Microphone.GetPosition(selectedMicName);
        if (nowPos < 0 || !Microphone.IsRecording(selectedMicName))
            return System.Array.Empty<float>();

        float[] waveData = System.Array.Empty<float>();

        // �^���ʒu���O����i��ł���ꍇ
        if (lastMicPos < nowPos)
        {
            int audioCount = nowPos - lastMicPos;
            waveData = new float[audioCount];
            micClip.GetData(waveData, lastMicPos);
        }
        // ���[�v���Ę^���ʒu�������߂����ꍇ
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
