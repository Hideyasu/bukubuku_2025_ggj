using UnityEngine;
// using UnityEngine.UI;
using TMPro;

public class DisplayPlayerName : MonoBehaviour
{
    // public Text nameText;
    public TMP_Text nameText; // TMP�łȂ炱����

    void Start()
    {
        // �V�[�����ǂݍ��܂ꂽ��AGlobalParam �Ɋi�[����Ă��閼�O��\��
        nameText.text = GlobalParam.playerName;
    }
}
