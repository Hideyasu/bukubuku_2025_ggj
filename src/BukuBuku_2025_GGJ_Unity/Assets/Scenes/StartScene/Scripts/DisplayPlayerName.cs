using UnityEngine;
// using UnityEngine.UI;
using TMPro;

public class DisplayPlayerName : MonoBehaviour
{
    // public Text nameText;
    public TMP_Text nameText; // TMP版ならこちら

    void Start()
    {
        // シーンが読み込まれたら、GlobalParam に格納されている名前を表示
        nameText.text = GlobalParam.playerName;
    }
}
