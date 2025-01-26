using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class RandomSpeech : MonoBehaviour
{
	[SerializeField]
	[Header("�e�L�X�g�I�u�W�F�N�g")]
	Text speechText;

	[SerializeField]
	[Header("�ӂ������I�u�W�F�N�g")]
	GameObject speechPanel;

	[SerializeField]
	[Header("�Z���t����")]
	private string[] speeches;

	// ���ɐU������܂ł̑ҋ@����
	[SerializeField]
	[Header("���ɐU������܂ł̎���")]
	float nextWaitingTime = 10.0f;

	// �ӂ��������\������鎞��
	[SerializeField]
	[Header("�ӂ��������\������鎞��")]
	float speakingTime = 1.5f;

	// �^�C�}�[
	[SerializeField]
	[Header("��������")]
	private float limitTime = 60;       // �{���͐������Ԃ��Q�[���}�l�[�W���[����擾������

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
		while (limitTime > 0)                                        // �������Ԃ܂Ń��[�v����
		{
			yield return null;
			limitTime -= Time.deltaTime;                            // �o�ߎ��Ԃ̉��Z
			yield return StartCoroutine(NextRandomSpeak());			// ����
		}
	}

	IEnumerator NextRandomSpeak()
	{
		float timer = 0;
		while (nextWaitingTime > timer)
		{
			yield return null;               // �ҋ@
			timer += Time.deltaTime;         // �^�C�}�[�̉��Z
		}
		//speechPanel.SetActive(true);
		isWatching = true;
		speechText.text = speeches.ElementAt(Random.Range(0, speeches.Count()));    // �����_���ȃe�L�X�g�𔽉f
		timer = 0;
		while (speakingTime > timer)
		{
			yield return null;               // �ҋ@
			timer += Time.deltaTime;         // �^�C�}�[�̉��Z
		}
		//speechPanel.SetActive(false);
		isWatching = false;
	}
}
