using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump_Game : MonoBehaviour
{
	[SerializeField]
	string sceneName;	// ボタンが押されたら飛ぶシーンの名前
	public void OnJump()
	{
		SceneManager.LoadScene(sceneName);
	}
}
