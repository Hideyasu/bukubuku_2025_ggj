using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump_Result : MonoBehaviour
{
	[SerializeField]
	string sceneName;    // �{�^���������ꂽ���ԃV�[���̖��O
	public void OnJump()
	{
		SceneManager.LoadScene(sceneName);
	}
}
