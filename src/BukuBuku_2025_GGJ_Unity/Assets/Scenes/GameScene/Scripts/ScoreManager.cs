using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class ScoreManager : MonoBehaviour
{
	int score = 0;						// �X�R�A��ݒ肷��ϐ�
	[SerializeField] Text scoreText;        // �c�莞�Ԃ�\������ϐ�

	[SerializeField] private string m_DeviceName;	// �f�o�C�X
	private AudioClip m_AudioClip;					// �I�[�f�B�I�p�̕ϐ�
	private int m_LastAudioPos;						// �}�C�N�̃f�o�C�X��ۑ����Ă����ϐ�
	private float m_AudioLevel;						// �I�[�f�B�I�̉���


	// [SerializeField] private GameObject m_Cube;							// �e�X�g�V�[���Ŏg�p�����L���[�u(�Q�[���V�[���ɂ͂Ȃ��̂ŃG���[���o��)
	[SerializeField, Range(10, 100)] private float m_AmpGain = 10;
	[SerializeField] public float threshold = 0.1f;     // �������l


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
        // m_Cube.transform.localScale = new Vector3(1, 1 + m_AmpGain * m_AudioLevel, 1); // �e�X�g�V�[���Œ����`��ό`�����Ă��镔��	

        ////////////////////////////////////////////////////////////////////
        // m_AmpGain �� m_AudioLevel ��p���ăX�R�A�̌v�Z�����镔�������� //
        ////////////////////////////////////////////////////////////////////

        if (m_AudioLevel > threshold)
        {
            score += (int)(m_AudioLevel * 100);
        }

        scoreText.text = $"{score}";			// ��ʂɕ\�����ꂽ�X�R�A���X�V���Ă�������
	}

	private float[] GetUpdatedAudio()
	{

		int nowAudioPos = Microphone.GetPosition(null);// null�Ńf�t�H���g�f�o�C�X

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
