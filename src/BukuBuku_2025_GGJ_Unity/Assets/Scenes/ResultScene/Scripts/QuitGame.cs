using UnityEngine;

public class QuitGame : MonoBehaviour
{
	// �Q�[���I��:�{�^������Ăяo��
	public void EndGame()
	{
#if UNITY_EDITOR											// �G�f�B�^�[��Ŏ��s����Ă�ꍇ
		UnityEditor.EditorApplication.isPlaying = false;	// �Q�[�����I�����ꂽ�����ɂ���
#else
    Application.Quit();//�Q�[���v���C�I��					// ���ۂɃA�v���P�[�V���������
#endif
	}
}
