using UnityEngine;
using UnityEngine.SceneManagement;

public class BackTitleScene : MonoBehaviour
{
    public void OnClickBack()
    {
        if (SceneManager.GetActiveScene().name == "SettingScene")
        {
            SceneManager.LoadScene("TitleScene");
        }
        else
        {
            SceneManager.LoadScene("SettingScene");
        }
    }
}
