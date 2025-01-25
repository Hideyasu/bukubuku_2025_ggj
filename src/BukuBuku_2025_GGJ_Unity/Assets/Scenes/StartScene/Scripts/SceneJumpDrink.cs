using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJumpDrink : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("SelectRoomScene");
    }
}