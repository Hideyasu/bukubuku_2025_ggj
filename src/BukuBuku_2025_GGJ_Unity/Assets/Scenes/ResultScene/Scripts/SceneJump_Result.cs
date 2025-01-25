using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump_Result : MonoBehaviour
{
	[SerializeField]
	string sceneName;    // ボタンが押されたら飛ぶシーンの名前
	public void OnJump()
	{
		SceneManager.LoadScene(sceneName);
	}
}
