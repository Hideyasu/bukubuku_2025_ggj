using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	[SerializeField] int timeLimit;			// �c�莞�Ԃ�ݒ肷��ϐ�
	[SerializeField] Text timerText;		// �c�莞�Ԃ�\������ϐ�
	float time;								// �c�莞�Ԃ��v�Z����ϐ�

	[SerializeField]
	string sceneName;						// �������Ԃ�0�ɂȂ������ԃV�[����

	void Update()
	{
		time += Time.deltaTime;					// �t���[�����̌o�ߎ��Ԃ�time�ϐ��ɒǉ�
		int remaining = timeLimit - (int)time;	// time�ϐ���int�^�ɕϊ����������Ԃ������������limit�ϐ��ɑ��
		if ( remaining > 0 )
		{
			timerText.text = $"{remaining.ToString("D3")}";		// timerText���X�V���Ă���
		}
		else													// �c�莞��0�ȉ��ɂȂ����ꍇ
		{
			timerText.text = $"{0}";							// 0��\��
			SceneManager.LoadScene(sceneName); ;				// ��ʑJ�ڂ���
		}
	}
}